namespace Server.Models
{
    public enum Roles
    {
        User,
        Admin
    }

    [Flags]
    public enum Days
    {
        None = 0,
        Monday = 16,
        Tuesday = 8,
        Wednesday = 4,
        Thursday = 2,
        Friday = 1
    }

    public class User
    {
        public int Id { get; set; }

        /// <summary>
        /// Familly name
        /// </summary>
        public required string LastName { get; set; }

        public required string FirstName { get; set; }

        public required string UserName { get; set; }

        public required Roles Role { get; set; }

        public required Days Invitation {  get; set; }

        public Form? Form { get; set; }
    }
}
