﻿namespace Services.Aplication.DTOs.Subcategory
{
    public class SubcategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

}