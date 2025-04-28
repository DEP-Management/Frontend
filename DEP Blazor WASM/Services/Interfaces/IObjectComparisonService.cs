namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IObjectComparisonService
    {
        bool AreSimplePropertiesDifferent<T>(T obj1, T obj2);
    }
}
