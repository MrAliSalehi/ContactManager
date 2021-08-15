using System.Collections.Generic;

namespace ClientManager.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
    }

    public static class UserItems
    {
        private static UserModel us = new();
        public static List<string> ItemNames => new() { nameof(us.ID), "FirstName","LastName", nameof(us.Phone), nameof(us.Email), nameof(us.Note) };
    }
}
