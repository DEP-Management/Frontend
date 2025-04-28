using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Modal.Services;
using DEP_Blazor_WASM.Entities.Enums;
using DEP_Blazor_WASM.Entities.RequestModels;
using DEP_Blazor_WASM.Services.Interfaces;
using DEP_Blazor_WASM.Services.States;
using DEP_Blazor_WASM.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;

namespace DEP_Blazor_WASM.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _factory;
        private readonly ILocalStorageService _localStorageService;
        private readonly CustomAuthStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;
        private readonly AppState _appState;

        private const string JWT_KEY = nameof(JWT_KEY);
        private const string REFRESH_KEY = nameof(REFRESH_KEY);

        [CascadingParameter] IModalService Modal { get; set; } = default!;

        public AuthService(
            IHttpClientFactory factory,
            ILocalStorageService localStorageService,
            CustomAuthStateProvider authStateProvider,
            NavigationManager navigationManager,
            AppState appState)
        {
            _factory = factory;
            _localStorageService = localStorageService;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
            _appState = appState;
        }

        public async ValueTask<string?> GetJwtAsync()
        {
            return await _localStorageService.GetItemAsync<string>(JWT_KEY);
        }

        public async Task<bool> LoginAsync(LoginRequest loginModel)
        {
            var response = await _factory.CreateClient("ServerApi").PostAsJsonAsync("auth/login", loginModel);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var content = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();

            if (content == null)
            {
                throw new InvalidDataException();
            }

            // Store JWT and Refresh tokens in localStorage
            await _localStorageService.SetItemAsync(JWT_KEY, content.AccessToken);
            await _localStorageService.SetItemAsync(REFRESH_KEY, content.RefreshToken);

            await _authStateProvider.GetAuthenticationStateAsync();

            return true;  // return true on successful login

        }

        public async Task LogoutAsync()
        {
            // Remove tokens from local storage
            await _localStorageService.RemoveItemAsync(JWT_KEY);
            await _localStorageService.RemoveItemAsync(REFRESH_KEY);

            // Notify the application of the state change
            _authStateProvider.NotifyStateChanged();
        }


        public async Task<bool> RefreshAsync()
        {
            // Get the authentication state
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            // Extract the userId from claims
            var userIdClaim = user.FindFirst(c => c.Type == "userId")?.Value;

            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return false;  // userId claim not found or invalid format
            }

            try
            {
                var model = new AuthenticatedResponse
                {
                    AccessToken = await _localStorageService.GetItemAsync<string>(JWT_KEY) ?? "",
                    RefreshToken = await _localStorageService.GetItemAsync<string>(REFRESH_KEY) ?? "",
                    UserId = userId
                };

                var response = await _factory.CreateClient("ServerApi").PostAsJsonAsync("auth/refresh", model);

                if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.BadRequest)
                {
                    // Get the message from the backend
                    var message = await response.Content.ReadAsStringAsync();

                    // Prepare for logout
                    await LogoutAsync();

                    // Trigger Logout so the component subscribed to logouts can open the logout modal
                    _appState.TriggerLogout(message);
                    return false;
                }

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();
                    await _localStorageService.SetItemAsync(JWT_KEY, content?.AccessToken);
                    await _localStorageService.SetItemAsync(REFRESH_KEY, content?.RefreshToken);
                    return true;
                }

                // If not successful, handle other cases (e.g., bad response)
                await LogoutAsync();
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await LogoutAsync();
                return false;
            }
        }

        public async Task<int?> GetCurrentUserId()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            var roleIdClaim = user.FindFirst(c => c.Type == "userId")?.Value;

            if (roleIdClaim == null || !int.TryParse(roleIdClaim, out int userId))
            {
                return null;
            }

            return userId;
        }

        public async Task<UserRole?> GetCurrentUserRole()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            var roleClaim = user.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(roleClaim) || !Enum.TryParse(roleClaim, out UserRole userRole))
            {
                return null;
            }

            return userRole;
        }


    }
}
