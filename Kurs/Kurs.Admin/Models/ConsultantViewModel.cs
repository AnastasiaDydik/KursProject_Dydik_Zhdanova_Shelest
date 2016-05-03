using Kurs.Admin.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kurs.Admin.Models
{
    public class ConsultantViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Номер телефона")]
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "Электронная почта")]
        [Required]
        public string Email { get; set; }

        public ConsultantViewModel()
        {

        }

        public ConsultantViewModel(Consultant consultant)
        {
            if (consultant == null)
                throw new ArgumentNullException(nameof(consultant));

            Id = consultant.Id;
            Name = consultant.Name;
            PhoneNumber = consultant.PhoneNumber;
            Email = consultant.Email;
        }
    }
}