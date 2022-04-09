namespace DB_Test.EntityContext
{
    public class Groups
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public int DisciplineId { get; set; }
        public Disciplines Discipline { get; set; }
    }
}
