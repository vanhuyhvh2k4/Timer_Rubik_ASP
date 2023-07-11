namespace Timer_Rubik.WebApp.Models
{
    public class Rule
    {
        public Guid Id { get; set; }

        public string RoleName{ get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
