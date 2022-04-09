namespace DB_Test.EntityContext
{
    public class Tests
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DisciplineId { get; set; }
        public Disciplines Discipline { get; set; }
    }
}
