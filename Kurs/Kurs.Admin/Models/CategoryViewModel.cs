using Kurs.Admin.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kurs.Admin.Models
{
    public class CategoryViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Заголовок")]
        [Required]
        public string Title { get; set; }

        public CategoryViewModel()
        {

        }

        public CategoryViewModel(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            Id = category.Id;
            Title = category.Title;
        }
    }
}