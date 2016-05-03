using System;
using System.ComponentModel.DataAnnotations;

namespace Kurs.Admin.Models
{
    public class OperatingSystemViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Title { get; set; }

        public OperatingSystemViewModel()
        {

        }

        public OperatingSystemViewModel(Repository.OperatingSystem os)
        {
            if (os == null)
                throw new ArgumentNullException(nameof(os));

            Id = os.Id;
            Title = os.Title;
        }
    }
}