namespace Server.Models
{
    public enum AccessGroup
    {
        Tuesday,
        Wednesday,
        TuesdayToThursday,
        Full,
        Admin
    }

    public class User
    {
        public int Id { get; set; }

        /// <summary>
        /// Familly name
        /// </summary>
        public required string LastName { get; set; }

        public required string FirstName { get; set; }

        public string UserName { get; set; }

        public AccessGroup AccessGroup { get; set; }
    }
}
