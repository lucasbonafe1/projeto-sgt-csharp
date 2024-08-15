using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGT.Application.DTOs.Users
{
    public class UserUpdateDTO
    {
        [StringLength(50, ErrorMessage = "Número de caracteres excedido.")]
        public string? Name { get; set; }

        [RegularExpression(@"\(\d{2}\) \d{5}-\d{4}", ErrorMessage = "Formato de telefone inválido")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        public string? Email { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 50 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?=.*\d).{8,50}$", ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um caractere especial e um número.")]
        public string? Password { get; set; }

    }
}
