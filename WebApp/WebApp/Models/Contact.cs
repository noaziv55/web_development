using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class Contact
    {
        [Key]
        [JsonPropertyName("id")]
        public string UsernameOfContact { get; set; }

        [Key]
        [JsonIgnore]
        public string ContactOfUser { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string NicknameOfContact { get; set; }

        [Required]
        public string server { get; set; }

        [JsonPropertyName("last")]
        public string lastMessage { get; set; }

        public DateTime? lastdate { get; set; }

        public Contact(string UsernameOfContact, string ContactOfUser,string NicknameOfContact, string server)
        {
            this.UsernameOfContact = UsernameOfContact;
            this.ContactOfUser = ContactOfUser;
            this.NicknameOfContact = NicknameOfContact;
            this.server = server;
            lastMessage = null;
            lastdate = null;
        }
    }
}