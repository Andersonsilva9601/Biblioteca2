using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    #region TB_USUARIO
    public class TB_USUARIO
    {
        public int Id { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "O usuário deve ser informado!")]
        public string Usuario { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A senha deve ser informada!")]
        public string Senha { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome deve ser informado!")]
        public string Nome { get; set; }

        [Display(Name = "Acesso")]
        [Required(ErrorMessage = "O nível de acesso deve ser informado!")]
        public int Acesso { get; set; }
    }
    #endregion

    #region Security
    public class Security
    {
        public static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
    #endregion
}