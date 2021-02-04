using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public class LoginViewModel
    {
        [DisplayName("Usuário")]
        [Required(ErrorMessage = "O login do usuário deve ser informado.")]
        [StringLength(100, MinimumLength = 15, ErrorMessage = "O login do usuário deve ter no mínimo 15 e no máximo 100 caractéres.")]
        public string Login { get; set; }
        [DisplayName("Senha")]
        [Required(ErrorMessage = "A senha do usuário deve ser informada.")]
        [StringLength(100, MinimumLength = 15, ErrorMessage = "A senha do usuário deve ter no mínimo 8 e no máximo 20 caractéres.")]
        public string Password { get; set; }
    }
}
