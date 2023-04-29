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
        Console.WriteLine(train.generateAnnouncement());
    }

}
