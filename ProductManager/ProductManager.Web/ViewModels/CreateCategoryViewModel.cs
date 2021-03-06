﻿using System.ComponentModel.DataAnnotations;

namespace ProductManager.Web.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
