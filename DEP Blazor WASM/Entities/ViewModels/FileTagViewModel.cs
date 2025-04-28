namespace DEP_Blazor_WASM.Entities.ViewModels
{
    public class FileTagViewModel
    {
        public int FileTagId { get; set; }
        public string TagName { get; set; } = string.Empty;
        public List<FileTagUserRoleViewModel> FileTagUserRoles { get; set; } = new();
    }
}
