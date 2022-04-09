namespace DB_Test.EntityContext
{
    public class Disciplines
    {
        public int Id { get; set; }
        public string? DisciplineName { get; set; }
        public int SpecialtieId { get; set; }
        public Specialties Specialtie { get; set;}
    }
}
