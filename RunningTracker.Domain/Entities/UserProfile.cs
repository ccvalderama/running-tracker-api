﻿namespace RunningTracker.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public double Weight {  get; set; }
        public double Height {  get; set; }
        public DateTime BirthDate { get; set; }
        public int Age => DateTime.Now.Year - BirthDate.Year;
        public double BMI => Weight / Math.Pow(Height / 100, 2);

        public virtual ICollection<RunningActivity> RunningActivities { get; set; } = new List<RunningActivity>();
    }
}
