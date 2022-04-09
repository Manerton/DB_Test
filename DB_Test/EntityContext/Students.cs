namespace DB_Test.EntityContext
{
    public class Students
    {
        public int Id { get; set; }
        public string? StudentName { get; set;}
        public int GroupId { get; set; }
        public Groups Group { get; set; }
    }
}
