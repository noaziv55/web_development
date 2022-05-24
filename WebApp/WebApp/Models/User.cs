using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class User
    {
        [Key]
        [JsonPropertyName("username")]
        public string UsernameOfUser { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [JsonPropertyName("server")]
        public string ServerAddress { get; set; }

        public User(string UsernameOfUser, string Password, string ServerAddress)
        {
            this.UsernameOfUser = UsernameOfUser;
            this.Password = Password;
            this.ServerAddress = ServerAddress;
        }
    }
}