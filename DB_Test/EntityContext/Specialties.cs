namespace DB_Test.EntityContext
{
    public class Specialties
    {
        //Id специальности
        public int Id { get; set; }
        //Название специальности
        public string? SpecialtiesName { get; set; }
        //Id интистута к которому относится данная специальность
        public int InstituteId { get; set; }
        public Institutes institute { get; set; }  

    }
}
