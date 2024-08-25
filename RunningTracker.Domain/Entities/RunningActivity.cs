using System.ComponentModel.DataAnnotations.Schema;

namespace RunningTracker.Domain.Entities
{
    public class RunningActivity
    {
        public int Id { get; set; }
        public required int UserProfileId {  get; set; }
        public required string Location { get; set; }
        public DateTime StartDateTime { get; set; } = DateTime.Now;
        public DateTime EndDateTime { get; set; } = DateTime.MinValue;
        public double Distance { get; set; }
        public TimeSpan Duration => EndDateTime - StartDateTime;
        public double AveragePace => Duration.TotalMinutes / Distance;

        [NotMapped]
        public virtual UserProfile? UserProfile { get; set; }

    }
}
