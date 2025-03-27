using Server.Models;
using UserModel = Server.Models.User;

namespace Server.DTOs
{
    public class User
    {

        public int? Id { get; set; }

        public required string UserName { get; set; }

        public required Roles Role { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Invitation { get; set; }

        public static User FromModel(UserModel userModel)
        {
            return new User
            {
                Id = userModel.Id,
                UserName = userModel.UserName,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Role = userModel.Role,
                Invitation = Convert.ToString((int)userModel.Invitation, 2).PadLeft(6, '0')
            };
        }

        public static UserModel ToModel(User user)
        {
            return new UserModel
            {
                Id = user.Id ?? 0,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Role = user.Role,
                Invitation = (Models.Days)Convert.ToInt32(user.Invitation, 2),
            };
        }
    }
}
