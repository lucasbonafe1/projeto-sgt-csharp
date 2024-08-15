using SGT.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGT.Application.DTOs.Users
{
    public class UserRequestDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "Número de caracteres excedido.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [RegularExpression(@"\(\d{2}\) \d{5}-\d{4}", ErrorMessage = "Formato de telefone inválido")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 50 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?=.*\d).{8,50}$", ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um caractere especial e um número.")]
        public string Password { get; set; }


        public UserRequestDTO() { }

        public UserRequestDTO(UserEntity userEntity)
        {
            Name = userEntity.Name;
            PhoneNumber = userEntity.PhoneNumber;
            Email = userEntity.Email;
            Password = userEntity.Password;
        }
    }
}
