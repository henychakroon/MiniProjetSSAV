using Microsoft.AspNetCore.Identity;

namespace MiniProject.MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName {  get; set; }
        public string Adress { get; set; }
    }
}
