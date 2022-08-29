using System.Text.Json.Serialization;

namespace Application.Models
{
    public class StatsDto
    {
        [JsonPropertyName("count_mutant_dna")]
        public int CountMutantDna { get; set; }
        [JsonPropertyName("count_human_dna")]
        public int CountHumanDna { get; set; }
        [JsonPropertyName("ratio")]
        public double Ratio { get; set; }
    }
}
