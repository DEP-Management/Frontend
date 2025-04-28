using DEP_Blazor_WASM.Entities.Models;

namespace DEP_Blazor_WASM.Services.Interfaces
{
    public interface ILocationService
    {
        Task<bool> AddLocationAsync(Location location);
        Task<List<Location>> GetLocationsAsync();
        Task<bool> UpdateLocationAsync(Location model);
        Task<bool> DeleteLocationAsync(int locationId);
    }
}
