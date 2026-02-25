namespace BizSecureDemo_22180022.Data
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public int? FailedLogins { get; set; }
        public DateTime? LockoutUntilUtc { get; set; }
    }
}
