namespace Server.DTOs
{
    public class Admin
    {
        public IEnumerable<User> UnAnswered { get; set; }
        public IEnumerable<User> Comming { get; set; }
        public IEnumerable<User> NotComming { get; set; }
    }
}
