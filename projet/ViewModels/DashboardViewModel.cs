using System.Security.Claims;

namespace projet.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalClaims { get; set; }
        public int CompletedInterventions { get; set; }
        public int PendingInterventions { get; set; }
        public List<Claim> RecentClaims { get; set; }
    }
}
