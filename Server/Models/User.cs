﻿namespace Server.Models
{
    public enum Roles
    {
        User,
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

        public required string UserName { get; set; }

        public required Roles Role { get; set; }

        public required string Invitation {  get; set; }
    }
}
