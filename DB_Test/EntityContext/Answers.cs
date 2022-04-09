namespace DB_Test.EntityContext
{
    public class Answers
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int QuestionId { get; set; }
        public Questions Questions { get; set; }
    }
}
