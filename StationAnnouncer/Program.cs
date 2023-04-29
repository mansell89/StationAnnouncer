using StationAnnouncer;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

internal class Program
{   

    private static void Main(string[] args)
    {
        Train train = new Train();
        //get raw text from file
        string rawText = "";
        try
        {
            // Open the text file using a stream reader.
            using (var sr = new StreamReader("C:\\Users\\SPUD\\Documents\\Job Apps\\Queensland Rail\\Code\\Test1.txt"))
            {
                // Read the stream as a string, and write the string to the console.
                Console.WriteLine("Raw Input");
                rawText = sr.ReadToEnd().Trim();
                Console.WriteLine(rawText);
                Console.WriteLine("------------------");
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        //Split up csv into stations
        train.generateStopSequence(rawText);
        train.findExpressSections();
        //get express sections
        // generate description
        // output description
    
        Console.WriteLine(generateAnnouncement(train));
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
