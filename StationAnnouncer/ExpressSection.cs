using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationAnnouncer
{
    /// <summary>
    /// The ExpressSection class represents a section of a train route that consists of starting and ending stations,
    /// and optionally an intermediate station. It also includes a list of all the stations in the section.
    /// </summary>
    class ExpressSection
    {
        public Station StartStation;
        public Station EndStation;
        public Station intermediateStation;
        public bool hasIntermediateStation;
        public List<Station> stationList;
        /// <summary>
        /// Initializes a new instance of the ExpressSection class with default values.
        /// </summary>
        public ExpressSection() { 
            this.StartStation = new Station();
            this.EndStation = new Station();
            this.intermediateStation = new Station();
            this.stationList = new List<Station>();
            this.hasIntermediateStation = false;
        }
        /// <summary>
        /// Initializes a new instance of the ExpressSection class with the specified start station, end station,
        /// intermediate station, whether the intermediate station exists, and a list of stations in the section.
        /// </summary>
        /// <param name="startStation">The station before the express section.</param>
        /// <param name="endStation">The station after the express section.</param>
        /// <param name="intermediateStation">The intermediate station in the express section, if it exists.</param>
        /// <param name="hasIntermediateStation">Indicates whether the express section has an intermediate station or not.</param>
        /// <param name="stationList">The list of stations in the express section.</param>
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
