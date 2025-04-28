namespace DEP_Blazor_WASM.Services.States
{
    public class AppState
    {
        public event Action? OnLogout;

        public string? LogoutReason { get; set; }

        public void TriggerLogout(string reason)
        {
            LogoutReason = reason;
            OnLogout?.Invoke();
        }
    }
}
