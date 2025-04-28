using DEP_Blazor_WASM.Entities.Enums;

namespace DEP_Blazor_WASM.Entities.Models
{
    public class FileTagUserRole
    {
        public int FileTagId { get; set; }
        public FileTag FileTag { get; set; }

        public UserRole Role { get; set; }
    }
}
