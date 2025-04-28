using DEP_Blazor_WASM.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net;

namespace DEP_Blazor_WASM.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private bool _refreshing;

        public AuthenticationHandler(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await _authService.GetJwtAsync();
            var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;

            if (isToServer && !string.IsNullOrEmpty(jwt))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (!_refreshing && !string.IsNullOrEmpty(jwt) && response.StatusCode == HttpStatusCode.Unauthorized)
            {
                try
                {
                    _refreshing = true;

                    if (await _authService.RefreshAsync())
                    {
                        jwt = await _authService.GetJwtAsync();

                        if (isToServer && !string.IsNullOrEmpty(jwt))
                        {
                            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", jwt);
                        }

                        response = await base.SendAsync(request, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    _refreshing = false;
                }
            }

            return response;
        }
    }
}
