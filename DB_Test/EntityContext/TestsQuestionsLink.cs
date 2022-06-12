using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class TestsQuestionsLink
    {
        public int Id { get; set; }
        [Required]
        public Guid QuestionId { get; set; }
        [Required]
        public Guid TestId { get; set; }
        public Questions question { get; set; }
        public Tests test { get; set; }
    }
}
