﻿using System.Text.Json.Serialization;

namespace Recipe.Parser.Models
{
    public class WebSnippet
    {
        [JsonPropertyName("displayLink")]
        public string? DisplayLink { get; set; }

        [JsonPropertyName("formattedUrl")]
        public string? FormattedUrl { get; set; }

        [JsonPropertyName("htmlFormattedUrl")]
        public string? HtmlFormattedUrl { get; set; }

        [JsonPropertyName("htmlSnippet")]
        public string? HtmlSnippet { get; set; }

        [JsonPropertyName("htmlTitle")]
        public string? HtmlTitle { get; set; }

        [JsonPropertyName("link")]
        public string? Link { get; set; }

        [JsonPropertyName("snippet")]
        public string? Snippet { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}