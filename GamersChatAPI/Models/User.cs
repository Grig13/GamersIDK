﻿namespace GamersChatAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
