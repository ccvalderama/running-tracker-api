using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningTracker.Domain.ViewModel
{
    public class RunningActivityViewModel
    {
        public required string Location { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double Distance { get; set; }
        public required int UserProfileId { get; set; }
    }
}
