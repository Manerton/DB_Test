using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class Questions
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 5)]
        public string TextQuestions { get; set; }
        public string? Theme { get; set; }
        [Required]
        public string TrueAnswer { get; set; }
        [Required]
        public int Score { get; set; }

    }
}
