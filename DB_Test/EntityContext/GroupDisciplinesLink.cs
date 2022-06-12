
using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class GroupDisciplinesLink
    {
        public int Id { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int DisciplineId { get; set; }
        public Groups group{ get; set; }
        public Disciplines discipline { get; set; }
    }
}
