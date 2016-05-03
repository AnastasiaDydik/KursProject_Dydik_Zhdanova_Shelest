using Kurs.Admin.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kurs.Admin.Models
{
    public class MakerViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Title { get; set; }

        public MakerViewModel()
        {

        }

        public MakerViewModel(Maker maker)
        {
            if (maker == null)
                throw new ArgumentNullException(nameof(maker));

            Id = maker.Id;
            Title = maker.Title;
        }
    }
}