using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public void generateStopSequence(string rawText)
        {
            string[] lines = rawText.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string[] splitText = lines[i].Split(",");
                Station station = new Station();
                station.name = splitText[0];
                station.isStopping = bool.Parse(splitText[1].Trim());
                this.stopSequence.Add(station);
                // go through list of stations and find the the last stop
                if (station.isStopping)
                {
                    this.lastStoppingStationIndex = i;
                    this.numberOfStops++;
                }
            }
        }
        public void findExpressSections()
        {

            ExpressSection currentSection = new ExpressSection();
            bool expressSectionStarted = false;

            for (int i = 0; i <= this.lastStoppingStationIndex; i++)
            {
                if (!this.stopSequence[i].isStopping)
                {
                    currentSection.stationList.Add(this.stopSequence[i]);
                    if (!expressSectionStarted)
                    {
                        currentSection.StartStation = this.stopSequence[i - 1];
                        expressSectionStarted = true;
                    }
                }
                else
                {
                    //look for intermediate station
                    if (expressSectionStarted && i < this.lastStoppingStationIndex - 1)
                    {
                        //if the next train is express again. this means the current station is an intermediate station
                        if (!this.stopSequence[i + 1].isStopping)
                        {
                            currentSection.intermediateStation = this.stopSequence[i];
                            currentSection.hasIntermediateStation = true;
                        }
                        else
                        {
                            currentSection.EndStation = this.stopSequence[i];
                            this.expressSections.Add(currentSection);
                            currentSection.hasIntermediateStation = false;
                            currentSection.stationList.Clear();
                            expressSectionStarted = false;
                        }
                    }

                    else if (expressSectionStarted)
                    {
                        currentSection.EndStation = this.stopSequence[i];
                        expressSectionStarted = false;
                        this.expressSections.Add(currentSection);
                    }
                }
            }
        }
    }
}
