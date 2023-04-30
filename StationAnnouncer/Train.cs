using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationAnnouncer
{
    /// <summary>
    /// Represents a train with a stop sequence and express sections. This class provides methods to generate the stop sequence,
    /// identify express sections, and generate train announcements based on the stopping pattern.
    /// </summary>
    class Train
    {
        public List<Station> stopSequence;
        public List<ExpressSection> expressSections;
        public int lastStopIndex;
        public int numberOfStops;

        /// <summary>
        /// Constructor for the Train class. Initializes the Train object with default values.
        /// </summary>
        public Train()
        {
            this.stopSequence = new List<Station>();
            this.expressSections = new List<ExpressSection>();
            this.lastStopIndex = 1;
            this.numberOfStops = 0;
        }
        /// <summary>
        /// Generates the stop sequence by parsing the input raw text, creating Station objects, and adding them to the stopSequence list.
        /// </summary>
        /// <param name="rawText">The raw text input representing the train's stop sequence.</param>
        public void generateStopSequence(string rawText)
        {
            string[] lines = rawText.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string[] splitText = lines[i].Split(",");
                Station station = new Station(splitText[0], 
                                              bool.Parse(splitText[1].Trim()));
                // find last stop
                if (station.isStopping)
                {
                    this.lastStopIndex = i;
                    this.numberOfStops++;
                }
                this.stopSequence.Add(station);
            }
        }
        /// <summary>
        /// Finds and creates ExpressSection objects based on the stop sequence. This method iterates through the stopSequence list and identifies sections where the train is running express.
        /// </summary>
        public void findExpressSections()
        {
            ExpressSection currentSection = new ExpressSection();
            bool expressSectionStarted = false;

            for (int i = 0; i <= this.lastStopIndex; i++)
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
                    if (expressSectionStarted && i < this.lastStopIndex - 1)
                    {
                        //if the next station is express. this means the current station is an intermediate station
                        if (!this.stopSequence[i + 1].isStopping)
                        {
                            currentSection.intermediateStation = this.stopSequence[i];
                            currentSection.hasIntermediateStation = true;
                        }
                        // else express section has finished
                        else
                        {
                            currentSection.EndStation = this.stopSequence[i];
                            this.expressSections.Add(new ExpressSection(currentSection.StartStation,
                                currentSection.EndStation,
                                currentSection.intermediateStation,
                                currentSection.hasIntermediateStation,
                                currentSection.stationList));
                            currentSection.hasIntermediateStation = false;
                            currentSection.stationList.Clear();
                            expressSectionStarted = false;
                        }
                    }
                    //captures end of list
                    else if (expressSectionStarted)
                    {
                        currentSection.EndStation = this.stopSequence[i];
                        this.expressSections.Add(new ExpressSection(currentSection.StartStation,
                            currentSection.EndStation,
                            currentSection.intermediateStation,
                            currentSection.hasIntermediateStation,
                            currentSection.stationList));
                    }
                }
            }
        }
        /// <summary>
        /// Generates a train announcement based on the expressSections list. This method creates a human-readable string describing the train's stopping pattern, considering all stations and express sections.
        /// </summary>
        /// <returns>A string representing the train announcement.</returns>

        public string generateAnnouncement()
        {
            if (this.expressSections.Count == 0)
            {
                if (this.numberOfStops == 2)
                {
                    return $"This train stops at {this.stopSequence[0].name} and {this.stopSequence[1].name} only";
                }
                return "This train stops at all stations";
            }
            if (this.expressSections.Count == 1)
            {
                string returnString = "";

                if (this.expressSections[0].stationList.Count == 1)
                {
                    return $"This train stops at all stations except {this.expressSections[0].stationList[0].name}";
                }

                returnString = $"This train runs express from {this.expressSections[0].StartStation.name} to {this.expressSections[0].EndStation.name}";
                if (this.expressSections[0].hasIntermediateStation)
                {
                    returnString += $", stopping only at {this.expressSections[0].intermediateStation.name}";
                }
                return returnString;
            }
            if (this.expressSections.Count > 1)
            {
                string returnString = "This train";
                for (int i = 0; i < this.expressSections.Count; i++)
                {
                    if (i == 0)
                    {
                        returnString += $" runs express from";
                    }
                    else
                    {
                        returnString += $" then runs express from";
                    }
                    returnString += $" {this.expressSections[i].StartStation.name} to {this.expressSections[i].EndStation.name}";
                    if (this.expressSections[i].hasIntermediateStation)
                    {
                        returnString += $", stopping only at {this.expressSections[i].intermediateStation.name}";
                    }

                }
                return returnString;
            }
            return "";
        }
    }
}
