using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class Students
    {
        public int Id { get; set; }
        [Required]
        public string StudentName { get; set;}
        [Required]
        public int GroupId { get; set; }
        public Groups Group { get; set; }
    }
}
