using Kurs.Admin.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kurs.Admin.Models
{
    public class ReviewViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Отзыв")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "Устройство")]
        [Required]
        public int DeviceId { get; set; }

        public ReviewViewModel()
        {

        }

        public ReviewViewModel(Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            Id = review.Id;
            Content = review.Content;
            DeviceId = review.DeviceId;
        }
    }
}