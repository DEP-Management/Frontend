using DEP_Blazor_WASM.Pages;

namespace DEP_Blazor_WASM.Services.States
{
    public class AppState
    {
        public event Func<Task>? OnLogout;
        public event Func<Task>? OnLogin;  // Using Func<Task> to support async calls

        public string? LogoutReason { get; set; }

        public async Task TriggerLogout(string reason)
        {
            if (OnLogout != null)
            {
                LogoutReason = reason;
                await OnLogout?.Invoke();
            }
        }

        public async Task TriggerLogin()
        {
            if (OnLogin != null)
            {
                //await Task.Delay(50);
                await OnLogin.Invoke();
            }
        }
    }
}
