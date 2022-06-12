using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class Tests
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CountQuestions { get; set; }
        [Required]
        public int DisciplineId { get; set; }
        public Disciplines Discipline { get; set; }
    }
}
