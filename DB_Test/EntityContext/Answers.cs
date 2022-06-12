using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class Answers
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public Guid QuestionsId { get; set; }
        public Questions Questions { get; set; }
    }
}
