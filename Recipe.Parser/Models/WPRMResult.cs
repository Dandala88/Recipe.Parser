using System.Text.Json.Serialization;

namespace Recipe.Parser.Models
{
    public class WPRMResult
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("html")]
        public string Html { get; set; }
    }
}
