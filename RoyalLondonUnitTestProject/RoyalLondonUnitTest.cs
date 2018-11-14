using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoyalLondon.Model;

namespace RoyalLondonUnitTestProject
{
    [TestClass]
    public class RoyalLondonUnitTest
    {
        public string filePath = string.Empty;

        [TestMethod]
        public void BasicTest()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            currentDirectory = currentDirectory.Replace(@"bin\Debug", "");

            //I have placed the maturity data csv in a folder called Data which sits
            //within the project folder
            filePath = Path.Combine(currentDirectory, "Data", "MaturityData.csv");

            var fileIO = new RoyalLondon.Model.FileIO();

            //the fileIO here only contains one method at present but could be 
            //expanded to include stream readers and writes.  The readAllLines 
            //functionality does what is needed and reads all the lines of data
            //in the csv
            string[] rawMaturityData = fileIO.ReadAllLines(filePath);

            var dataProcessor = new DataProcessor();

            //The raw data that is read from the file is processed and converted to a list of
            //MaturityDataViewModel
            var maturityData = dataProcessor.ProcessRawData(rawMaturityData);

            Assert.AreEqual(maturityData.Count, 9);

            //The maturity data is then written to an xml file
            dataProcessor.WriteXMLToFile(Path.Combine(currentDirectory, "Data", "MaturityData.xml"), maturityData);

            Assert.IsTrue(File.Exists(Path.Combine(currentDirectory, "Data", "MaturityData.xml")));
        }
    }
}
