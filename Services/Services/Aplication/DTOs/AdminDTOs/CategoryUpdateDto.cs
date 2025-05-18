namespace Services.Aplication.DTOs.AdminDTOs
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public List<Guid> SelectedSubCategoryIds { get; set; } = new();
    }

}
