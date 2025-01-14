namespace Server.DTOs
{
    public class UserFull: UserLight
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }
    }
}
