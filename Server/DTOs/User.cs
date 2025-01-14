using Server.Models;

namespace Server.DTOs
{
    public class User
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public AccessGroup AccessGroup { get; set; }
    }
}
