using DB_Test.EntityContext;

namespace DB_Test.DTO
{
    public class QuestionsDTO
    {
        public string? TextQuestions { get; set; }
        public string Theme { get; set; }
        public string? TrueAnswer { get; set; }
        public int Scores { get; set; }
        public List<string> Answers {get;set;}

    }
}
