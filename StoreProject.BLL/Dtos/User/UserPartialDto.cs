﻿namespace StoreProject.BLL.Dtos.User
{
    public class UserPartialDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public decimal Balance { get; set; }
    }
}
