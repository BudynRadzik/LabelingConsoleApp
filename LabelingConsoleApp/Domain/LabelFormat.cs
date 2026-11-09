using System.ComponentModel.DataAnnotations;

namespace LabelingConsole.Domain
{
    public class LabelFormat
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string TemplatePath { get; set; } = string.Empty;
    }
}
