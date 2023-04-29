using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationAnnouncer
{
    class Station
    {
        public string name;
        public bool isStopping;
        public Station() {
            this.name = "";
            this.isStopping = true;
        }
        public Station(string name)
        {
            this.name = name;
            this.isStopping = false;
        }
        public Station(string name, bool isStopping) : this(name)
        {
            this.name=name;
            this.isStopping = isStopping;
        }
    }
}
