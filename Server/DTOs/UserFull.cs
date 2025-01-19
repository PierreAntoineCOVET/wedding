using Server.Models;
using System.ComponentModel.DataAnnotations;

namespace Server.DTOs
{
    public class UserFull: UserLight
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        // JSON constructor
        public UserFull()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
            : base()
        {
        }

        public UserFull(User user)
            : base(user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
