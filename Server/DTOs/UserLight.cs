using Server.Models;

namespace Server.DTOs
{
    public class UserLight
    {
        public int Id { get; set; }

        public required string UserName { get; set; }

        public AccessGroup AccessGroup { get; set; }
    }
}
