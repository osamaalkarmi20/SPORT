﻿namespace SPORT.Models
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IList<string> Role { get; set; }
    }
}
