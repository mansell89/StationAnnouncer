using StationAnnouncer;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

internal class Program
{   

    private static void Main(string[] args)
    {
        Train train = new Train();
        //get raw text from file
        string rawInput = "";
        try
        {
            // Open the text file using a stream reader.
            using (var sr = new StreamReader("C:\\Users\\SPUD\\Documents\\Job Apps\\Queensland Rail\\Code\\Test1.txt"))
            {
                // Read the stream as a string, and write the string to the console.
                Console.WriteLine("Raw Input");
                rawInput = sr.ReadToEnd().Trim();
                Console.WriteLine(rawInput);
                Console.WriteLine("------------------");
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        //Split up csv into stations
        string[] lines = rawInput.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] splitText = lines[i].Split(",");
            Station station = new Station();
            station.name = splitText[0];
            station.isStopping = bool.Parse(splitText[1].Trim());
            train.stopSequence.Add(station);
            // go through list of stations and find the the last stop
            if (station.isStopping)
            {
                train.lastStoppingStationIndex = i;
                train.numberOfStops++;
            }
        }
        //get express sections
        findExpressSections(train);
        // generate description
        // output description
    
        Console.WriteLine(generateAnnouncement(train));
    }

    private static void findExpressSections(Train train)
    {

        ExpressSection currentSection = new ExpressSection();
        bool expressSectionStarted = false;

        for (int i = 0; i <= train.lastStoppingStationIndex;i++)
        {
            if (!train.stopSequence[i].isStopping)
            {
                currentSection.stationList.Add(train.stopSequence[i]);
                if (!expressSectionStarted)
                {
                    currentSection.StartStation= train.stopSequence[i-1];
                    expressSectionStarted = true;
                }
            }
            else
            {
                //look for intermediate station
                if (expressSectionStarted && i < train.lastStoppingStationIndex - 1)
                {   
                    //if the next train is express again. this means the current station is an intermediate station
                    if (!train.stopSequence[i+1].isStopping)
                    {
                        currentSection.intermediateStation= train.stopSequence[i];
                        currentSection.hasIntermediateStation= true;
                    }
                    else
                    {
                        currentSection.EndStation = train.stopSequence[i];
                        train.expressSections.Add(currentSection);
                        currentSection.hasIntermediateStation = false;
                        currentSection.stationList.Clear();
                        expressSectionStarted = false;
                    }
                }
                
                else if (expressSectionStarted)
                {
                    currentSection.EndStation = train.stopSequence[i];
                    expressSectionStarted = false;
                    train.expressSections.Add(currentSection);
                }
            }
        }
    }
    private static string generateAnnouncement(Train train)
    {
        if (train.expressSections.Count == 0)
        {
            if (train.numberOfStops == 2)
            {
                return $"This train stops at {train.stopSequence[0].name} and {train.stopSequence[1].name} Only";
            }
            return "This train stops at all stations";
        }
        if (train.expressSections.Count == 1)
        {
            string returnString = "";

            if (train.expressSections[0].stationList.Count == 1)
            {
                return $"This train stops at all stations except {train.expressSections[0].stationList[0].name}";
            }

            returnString = $"This train runs express from {train.expressSections[0].StartStation.name} to {train.expressSections[0].EndStation.name}";
            if (train.expressSections[0].hasIntermediateStation)
            {
                returnString += $", stopping only at {train.expressSections[0].intermediateStation.name}";
            }
            return returnString;
        }
        if (train.expressSections.Count > 1)
        {
            string returnString = "This train";
            for (int i = 0; i < train.expressSections.Count; i++)
            {
                if (i == 0)
                {
                    returnString += $" runs express from";
                }
                else
                {
                    returnString += $" then runs express from";
                }
                returnString += $" {train.expressSections[i].StartStation.name} to {train.expressSections[i].EndStation.name}";
                if (train.expressSections[i].hasIntermediateStation)
                {
                    returnString += $", stopping only at {train.expressSections[i].intermediateStation.name}";
                }

            }
            return returnString;
        }
        return "";
    }
}
