using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class Disciplines
    {
        public int Id { get; set; }
        [Required]
        public string DisciplineName { get; set; }
    }
}
