﻿namespace Services.Aplication.DTOs.Subcategory
{
    public class CreateSubcategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
