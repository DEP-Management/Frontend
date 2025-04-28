using System.Xml.Linq;

namespace DEP_Blazor_WASM.Entities.Models
{
    public class FileTag
    {
        public int FileTagId { get; set; }
        public string TagName { get; set; } = string.Empty;

        public List<FileTagUserRole> FileTagUserRoles { get; set; } = new List<FileTagUserRole>();

        // Used for MudSelects to fix issues displaying names for selected value.
        public override string ToString()
        {
            return TagName;
        }
    }
}
