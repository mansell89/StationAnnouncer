using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationAnnouncer
{
    class Train
    {
        public List<Station> stopSequence;
        public List<ExpressSection> expressSections;
        public int lastStoppingStationIndex;
        public int numberOfStops;

        public Train()
        {
            this.stopSequence = new List<Station>();
            this.expressSections = new List<ExpressSection>();
            this.lastStoppingStationIndex = 1;
            this.numberOfStops = 0;
        }
    }
}
