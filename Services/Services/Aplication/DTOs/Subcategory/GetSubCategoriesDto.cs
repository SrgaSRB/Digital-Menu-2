namespace Services.Aplication.DTOs.Subcategory
{
    public class SubCategoriesGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<SubCategoryList> SelectedSubCategoryLists { get; set; } = new();
        public List<SubCategoryList> AllSubCategoryLists { get; set; } = new();
    }
}
