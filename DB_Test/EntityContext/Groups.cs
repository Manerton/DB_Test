using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class Groups
    {
        public int Id { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 5)]
        public string GroupName { get; set; }
        [Required]
        public int SpecialtieId { get; set; }
        public Specialties specialtie { get; set; }
    }
}
