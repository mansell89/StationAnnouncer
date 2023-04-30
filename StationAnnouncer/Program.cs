using StationAnnouncer;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

internal class Program
{   
    private static void Main(string[] args)
    {
#if DEBUG
        args = new[] { "C:\\Users\\SPUD\\Documents\\Job Apps\\Queensland Rail\\Code\\Test6.txt" };
#endif
        if (args.Length == 0)
        {
            Console.WriteLine("Program requires full path to text file as argument");
        return;
        }
        try
        {   string fileName = args[0];
            Train train = new Train();
            train.generateStopSequence(getTextFromFile(fileName));
            train.findExpressSections();
            Console.WriteLine(train.generateAnnouncement());
        }
        catch (Exception e)
        {
            Console.WriteLine("There was an error");
            Console.WriteLine(e.Message);
        }  
    }
    private static string getTextFromFile(string fileName)
    {
        using (var sr = new StreamReader(fileName))
        {
            return sr.ReadToEnd().Trim();
        }
    }
}
