namespace Server.Model
{
    public enum AccessGroup
    {
        Partial,
        Full
    }

    public class User
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public AccessGroup AccessGroup { get; set; }
    }
}
