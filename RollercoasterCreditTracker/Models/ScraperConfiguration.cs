using RollercoasterCreditTracker.Interfaces;

namespace RollercoasterCreditTracker.Models
{
    public class ScraperConfiguration : IScraperConfiguration
    {
        public string ScraperApiUrl { get; set; } = string.Empty;
    }
}
