namespace projet.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    namespace projet.Models
    {
        public class RegisterViewModel
        {
            [Required]
            [Display(Name = "Nom d'utilisateur")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Adresse e-mail")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
            [Display(Name = "Confirmer le mot de passe")]
            public string ConfirmPassword { get; set; }
        }
    }


}
