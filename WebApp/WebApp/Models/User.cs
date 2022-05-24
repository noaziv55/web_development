using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class User
    {
        [Key]
        public string UsernameOfUser { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string ServerAddress { get; set; }

        public User(string UsernameOfUser, string Password, string ServerAddress)
        {
            this.UsernameOfUser = UsernameOfUser;
            this.Password = Password;
            this.ServerAddress = ServerAddress;
        }
    }
}