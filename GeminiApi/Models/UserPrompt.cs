using System.ComponentModel.DataAnnotations;

namespace GeminiApi.Models
{
    public class UserPrompt
    {
        [Required]
        public string Question { get; set; }
    }
}
