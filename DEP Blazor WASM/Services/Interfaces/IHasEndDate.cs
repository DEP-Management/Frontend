using DEP_Blazor_WASM.Entities.Models;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface IHasEndDate
    {
        DateTime HiringDate { get; }
        DateTime EndDate { get; }
        string EndDateStyle { get; set; }
    }
}
