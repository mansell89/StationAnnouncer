using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationAnnouncer
{
    class ExpressSection
    {
        public Station StartStation;
        public Station EndStation;
        public Station intermediateStation;
        public bool hasIntermediateStation;
        public List<Station> stationList;
        public ExpressSection() { 
            this.StartStation = new Station();
            this.EndStation = new Station();
            this.intermediateStation = new Station();
            this.stationList = new List<Station>();
            this.hasIntermediateStation = false;
        }
        public ExpressSection(Station startStation, Station endStation, Station intermediateStation, bool hasIntermediateStation, List<Station> stationList)
        {
            this.StartStation = startStation;
            this.EndStation = endStation;
            this.intermediateStation = intermediateStation;
            this.hasIntermediateStation = hasIntermediateStation;
            this.stationList = stationList;
        }
    }
}
