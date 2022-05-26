using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class Message
    {
        [Key]
        public int id { get; set; }

        [Required]
        [JsonIgnore]
        public string to { get; set; }

        [Required]
        [JsonIgnore]
        public string from { get; set; }
        public string content { get; set; }
        public DateTime created { get; set; }

        public Message(string from, string to, string content)
        {
            this.from = from;
            this.to = to;
            this.content = content;

            created = DateTime.Now;

        }
    }
}