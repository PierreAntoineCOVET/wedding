using Server.Models;
using System.ComponentModel.DataAnnotations;

namespace Server.DTOs
{
    public class UserLight
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        // JSON constructor
        public UserLight()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }

        public UserLight(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            AccessGroup = user.AccessGroup;
        }

        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public AccessGroup AccessGroup { get; set; }
    }
}
