using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using RoyalLondon.ViewModel;

namespace RoyalLondon.Model
{
    /// <summary>
    /// DataProcessor has methods which process the raw data and also writes the
    /// List<MaturityDataViewModel> to an xml file
    /// </summary>
    public class DataProcessor
    {
        List<MaturityDataViewModel> _maturityData;

        public DataProcessor()
        {
            _maturityData = new List<MaturityDataViewModel>();
        }

        public List<MaturityDataViewModel> ProcessRawData(string[] rawData)
        {
            _maturityData.Clear();

            for (var i = 1; i < rawData.Length; i++)
            {
                var splitData = rawData[i].Split(',').ToList();

                var membership = splitData[3].ToUpper() == "Y" ? Enums.YesNo.Yes : Enums.YesNo.No;
                var policyStartdate = DateTime.Parse(splitData[1]);
                double managementFeeMultiplier = 0.0;
                string policyType = "";

                //policyType has been set here just to make it easy to debug problems
                //The calculation is also done here, but maintainability could be a problem
                //if the number of rules grows.  possible solutions might be to call a rules engine,
                //or pass in a set of rules which would be matched to policy type
                if (policyStartdate < new DateTime(1990, 1, 1, 0, 0, 0))
                {
                    policyType = "A";
                    managementFeeMultiplier = 0.03;
                }
                else if (policyStartdate >= new DateTime(1990, 1, 1, 0, 0, 0) && membership == Enums.YesNo.Yes)
                {
                    policyType = "C";
                    managementFeeMultiplier = 0.07;
                }
                else if (membership == Enums.YesNo.Yes)
                {
                    policyType = "B";
                    managementFeeMultiplier = 0.05;
                }

                var item = new MaturityDataViewModel()
                {
                    PolicyNumber = splitData[0],
                    PolicyStartDate = DateTime.Parse(splitData[1]),
                    Premiums = double.Parse(splitData[2]),
                    Membership = membership,
                    DiscretionaryBonus = double.Parse(splitData[4]),
                    UpliftPercentage = double.Parse(splitData[5]),
                    PolicyType = policyType,
                    ManagementFeeMultiplier = managementFeeMultiplier
                };

                item.MaturityValue = ((item.Premiums - item.ManagementFeeMultiplier * item.Premiums) + item.DiscretionaryBonus) * (1 + (item.UpliftPercentage / 100));
                _maturityData.Add(item);
            }

            return _maturityData;
        }

        /// <summary>
        /// WriteXMLToFile could have gone into a separate class dealing with xml, but I have
        /// left it here for the momement.  If the program grows the code can be refactored
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public void WriteXMLToFile(string fileName, List<MaturityDataViewModel> data)
        {
            _maturityData = data;
            string result = string.Empty;

            //I have assumed that the file name and path are correct and have not
            //put any error handling here.
            using (XmlWriter writer = XmlWriter.Create(fileName))
            {
                writer.WriteStartElement("policies");

                foreach (var item in _maturityData)
                {
                    writer.WriteStartElement("policy");
                    writer.WriteElementString("policy_number", item.PolicyNumber);
                    writer.WriteElementString("maturity_value", item.MaturityValue.ToString());
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.Flush();
            }
        }

    }
}
