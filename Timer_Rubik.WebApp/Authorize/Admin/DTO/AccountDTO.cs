﻿namespace Timer_Rubik.WebApp.Authorize.Admin.DTO
{
    public class GetAccountDTO_Admin
    {
        public Guid Id { get; set; }

        public Guid RuleId { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
    
    public class CreateAccountDTO_Admin
    {
        public Guid RuleId { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class UpdateAccountDTO_Admin
    {
        public Guid Id { get; set; }

        public Guid RuleId { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
