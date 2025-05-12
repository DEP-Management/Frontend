using Blazored.LocalStorage;
using DEP_Blazor_WASM.Entities.Enums;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace DEP_Blazor_WASM.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly UserSessionService _userSessionService;

        private const string JWT_KEY = nameof(JWT_KEY);

        public CustomAuthStateProvider(ILocalStorageService localStorage, UserSessionService userSessionService)
        {
            _localStorage = localStorage;
            _userSessionService = userSessionService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string? token = await _localStorage.GetItemAsStringAsync(JWT_KEY);

            Console.WriteLine($"GetAuthenticationStateAsync: Token is {(string.IsNullOrEmpty(token) ? "null or empty" : "available")}");

            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(token))
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), JWT_KEY);
                var user = new ClaimsPrincipal(identity);

                // Parse and store session data
                var roleClaim = user.FindFirst(ClaimTypes.Role)?.Value;
                if (Enum.TryParse(roleClaim, out UserRole parsedRole))
                {
                    _userSessionService.CurrentUserRole = parsedRole;
                }

                _userSessionService.UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _userSessionService.UserName = user.Identity?.Name;
            }
            else
            {
                // Clear session info if token is missing
                _userSessionService.CurrentUserRole = null;
                _userSessionService.UserId = null;
                _userSessionService.UserName = null;
            }

            var userPrincipal = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(userPrincipal);

            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return keyValuePairs?
                .Select(kvp => new Claim(kvp.Key, kvp.Value?.ToString() ?? string.Empty))
                ?? Enumerable.Empty<Claim>();
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public void NotifyStateChanged()
        {
            _userSessionService.CurrentUserRole = null;
            _userSessionService.UserId = null;
            _userSessionService.UserName = null;

            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }
}
