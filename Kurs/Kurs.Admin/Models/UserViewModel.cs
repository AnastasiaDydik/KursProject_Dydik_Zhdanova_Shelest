using System.ComponentModel.DataAnnotations;

namespace Kurs.Admin.Models
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Логин")]
        public string Name { get; set; }

        [Display(Name = "Роль")]
        public string Role { get; set; }
    }

    public class UserFormViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Логин")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Роль")]
        public int RoleId { get; set; }
    }
}