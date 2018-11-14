using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RoyalLondon.Model
{
    /// <summary>
    /// This class deals with the common file io for this application.
    /// At the moment it only has one method called ReadAllLines
    /// </summary>
    public class FileIO
    {
        public FileIO()
        {

        }

        public string[] ReadAllLines(string fullPathName)
        {
            if (string.IsNullOrEmpty(fullPathName))
            {
                throw new Exception("File name has not bee supplied");
            }
            else if (!File.Exists(fullPathName))
            {
                throw new Exception("File does not exist");
            }

            return File.ReadAllLines(fullPathName);
        }
    }
}
