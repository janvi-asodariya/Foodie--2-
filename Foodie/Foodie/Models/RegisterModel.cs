using System.ComponentModel.DataAnnotations;

namespace Foodie.Models
{
    public class RegisterModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage ="Username required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name required")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Email required")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile required")]

        public string Modile { get; set; }

        [Required(ErrorMessage = "Address required")]

        public string Address { get; set; }

        [Required(ErrorMessage = "Postcode required")]

        public string Postcode { get; set; }


        public string imageUrl { get; set; }
        [Required(ErrorMessage = "Username required")]

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Password required")]

        public string Password { get; set; }    

    }
}
