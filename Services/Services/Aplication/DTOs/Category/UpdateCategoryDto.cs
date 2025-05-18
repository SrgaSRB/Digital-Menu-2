namespace Services.Aplication.DTOs.Category
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public List<Guid> SelectedSubCategoryIds { get; set; } = new();
    }
}
