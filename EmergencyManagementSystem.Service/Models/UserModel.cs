
using System.ComponentModel.DataAnnotations;

namespace EmergencyManagementSystem.Service.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Favor informar o usuário de login.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "O usuário deve ter no mínimo 8 e no máximo 100 caracteres.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Favor informar a senha do usuário.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha deve ter no mínimo 8 e no máximo 20 caracteres.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Favor informar o Id do funcionário.")]
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
