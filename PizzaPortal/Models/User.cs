using System.ComponentModel.DataAnnotations;

namespace PizzaPortal.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
    }
}
