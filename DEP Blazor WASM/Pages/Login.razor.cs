using DEP_Blazor_WASM.Entities.RequestModels;
using DEP_Blazor_WASM.Services;
using DEP_Blazor_WASM.Services.Interfaces;
using DEP_Blazor_WASM.Shared.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DEP_Blazor_WASM.Pages
{
    public partial class Login
    {
        private LoginRequest loginModel = new LoginRequest();
        private bool invalidLogin = false;

        // Injected services
        [Inject] private IAuthService AuthService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private CustomAuthStateProvider AuthStateProvider { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            //Navigate to default page if user is authenticated and enters the login page.
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            if (authState?.User?.Identity?.IsAuthenticated == true)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        private async Task SubmitLogin()
        {
            // Use AuthService to handle login
            var loginResult = await AuthService.LoginAsync(loginModel);

            if (loginResult)
            {
                NavigationManager.NavigateTo("/"); // Redirect after login
                StateHasChanged();
            }
            else
            {
                // Handle login failure
                invalidLogin = true;
                StateHasChanged();  // Trigger a re-render to display the error message
                Console.WriteLine("Login failed.");
            }
        }
    }
}
