using Kurs.Admin.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kurs.Admin.Models
{
    public class ColorViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Заголовок")]
        [Required]
        public string Title { get; set; }

        public ColorViewModel()
        {

        }

        public ColorViewModel(Color color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            Id = color.Id;
            Title = color.Title;
        }
    }
}