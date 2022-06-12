using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class Specialties
    {
        //Id специальности
        public int Id { get; set; }
        //Название специальности
        [Required]
        public string SpecialtiesName { get; set; }
        //Id интистута к которому относится данная специальность
        [Required]
        public int InstituteId { get; set; }
        public Institutes institute { get; set; }  
    }
}
