using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationAnnouncer
{
    /// <summary>
    /// The Station class represents a train station with a name and a flag indicating whether the train stops at the station or not.
    /// </summary>
    class Station
    {
        public string name;
        public bool isStopping;
        /// <summary>
        /// Initializes a new instance of the Station class with default values.
        /// </summary>
        public Station() {
            this.name = "";
            this.isStopping = true;
        }
        /// <summary>
        /// Initializes a new instance of the Station class with the specified station name and sets the isStopping flag to false.
        /// </summary>
        /// <param name="name">The name of the station.</param>
        public Station(string name)
        {
            this.name = name;
            this.isStopping = false;
        }
        /// <summary>
        /// Initializes a new instance of the Station class with the specified station name and isStopping flag.
        /// </summary>
        /// <param name="name">The name of the station.</param>
        /// <param name="isStopping">Indicates whether the train stops at the station or not.</param>
        public Station(string name, bool isStopping) : this(name)
        {
            this.name=name;
            this.isStopping = isStopping;
        }
    }
}
