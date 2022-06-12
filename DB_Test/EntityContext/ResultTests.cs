using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class ResultTests
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public Guid TestId { get; set; }
        [Required]
        public int SumScore { get; set; }
        public Students Student { get; set; }
        public Tests Test { get; set; }

    }

}
