namespace DEP_Blazor_WASM.Entities.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Amount { get; set; }

        public Module? Module { get; set; }
    }
}
