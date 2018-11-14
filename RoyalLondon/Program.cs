using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RoyalLondon.Model;

namespace RoyalLondon
{
    class Program
    {
        public static string filePath = string.Empty;

        static void Main(string[] args)
        {

            var currentDirectory = Directory.GetCurrentDirectory();
            currentDirectory = currentDirectory.Replace(@"bin\Debug", "");

            //I have placed the maturity data csv in a folder called Data which sits
            //within the project folder
            filePath = Path.Combine(currentDirectory, "Data", "MaturityData.csv");

            var fileIO = new FileIO();

            //the fileIO here only contains one method at present but could be 
            //expanded to include stream readers and writes.  The readAllLines 
            //functionality does what is needed and reads all the lines of data
            //in the csv
            string[] rawMaturityData = fileIO.ReadAllLines(filePath);

            var dataProcessor = new DataProcessor();

            //The raw data that is read from the file is processed and converted to a list of
            //MaturityDataViewModel
            var maturityData = dataProcessor.ProcessRawData(rawMaturityData);

            //The maturity data is then written to an xml file
            dataProcessor.WriteXMLToFile(Path.Combine(currentDirectory, "Data", "MaturityData.xml"), maturityData);


        }
    }
}
