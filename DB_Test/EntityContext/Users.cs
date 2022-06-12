using System.ComponentModel.DataAnnotations;

namespace DB_Test.EntityContext
{
    public class Users
    {
        public  int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string PwHash { get; set; }
        public string Role { get; set; }
    }
}
