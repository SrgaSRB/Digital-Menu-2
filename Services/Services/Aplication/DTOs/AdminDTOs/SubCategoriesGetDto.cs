namespace Services.Aplication.DTOs.AdminDTOs
{
    public class SubCategoriesGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SubCategoryList> AllSubCategoryLists { get; set; }
        public List<SubCategoryList> SelectedSubCategoryLists { get; set; }

    }
}
