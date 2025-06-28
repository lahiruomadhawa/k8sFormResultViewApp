namespace k8sFormResultViewApp.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string FormattedDate => CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
