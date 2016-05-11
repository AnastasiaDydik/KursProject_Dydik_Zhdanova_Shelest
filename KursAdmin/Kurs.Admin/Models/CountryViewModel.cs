using Kurs.Admin.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kurs.Admin.Models
{
    public class CountryViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Title { get; set; }

        public CountryViewModel()
        {

        }

        public CountryViewModel(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));

            Id = country.Id;
            Title = country.Title;
        }
    }
}