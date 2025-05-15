using DEP_Blazor_WASM.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net;

namespace DEP_Blazor_WASM.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private static readonly SemaphoreSlim _refreshLock = new SemaphoreSlim(1, 1);

        public AuthenticationHandler(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await _authService.GetJwtAsync();
            var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;
            var isRefreshRequest = request.RequestUri?.AbsoluteUri.Contains("auth/refresh") ?? false;

            if (isToServer && !string.IsNullOrEmpty(jwt) && !isRefreshRequest)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized && !string.IsNullOrEmpty(jwt) && !isRefreshRequest)
            {
                await _refreshLock.WaitAsync(cancellationToken);
                try
                {
                    var newJwt = await _authService.GetJwtAsync();
                    if (!string.IsNullOrEmpty(newJwt) && newJwt != jwt)
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newJwt);
                        response = await base.SendAsync(request, cancellationToken);
                    }
                    else if (await _authService.RefreshAsync())
                    {
                        newJwt = await _authService.GetJwtAsync();
                        if (isToServer && !string.IsNullOrEmpty(newJwt))
                        {
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newJwt);
                            response = await base.SendAsync(request, cancellationToken);
                        }
                    }
                }
                finally
                {
                    _refreshLock.Release();
                }
            }

            return response;
        }
    }
}
