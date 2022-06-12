using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class Institutes
    {
        public int Id { get; set; }
        [Required]
        public string InstitutesName { get; set; }
    }
}
