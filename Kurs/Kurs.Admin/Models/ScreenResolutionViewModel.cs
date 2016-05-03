using Kurs.Admin.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kurs.Admin.Models
{
    public class ScreenResolutionViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Длина")]
        [Required]
        public double Width { get; set; }

        [Display(Name = "Высота")]
        [Required]
        public double Height { get; set; }

        [Display(Name = "Диагональ")]
        [Required]
        public float Diagonal { get; set; }

        public ScreenResolutionViewModel()
        {

        }

        public ScreenResolutionViewModel(ScreenResolution screenResolution)
        {
            if (screenResolution == null)
                throw new ArgumentNullException(nameof(screenResolution));

            Width = screenResolution.Width;
            Height = screenResolution.Height;
            Diagonal = screenResolution.Diagonal;
            Id = screenResolution.Id;
        }
    }
}