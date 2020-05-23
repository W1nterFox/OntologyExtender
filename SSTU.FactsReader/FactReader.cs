using SSTU.FactsReader.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSTU.FactsReader
{
    public class FactsReader
    {
		public List<Fact> GetFactsFromFile(string filePath)
		{
			return GetFactsFromXml(filePath);
		}

		private List<Fact> GetFactsFromXml(string filePath)
		{
			var xmlDoc = XDocument.Load(filePath);
			var rowFacts = xmlDoc.Descendants("facts").Elements();

			var result = new List<Fact>();
			foreach (var rowFact in rowFacts)
			{
				var fact = GetFactsFromRowFact(rowFact);
				result.Add(fact);
			}

			return result;
		}

		private Fact GetFactsFromRowFact(XElement rowFact)
		{
			var factName = rowFact.Name.LocalName;
			var factFields = new List<FactField>();

			foreach (var subNode in rowFact.Elements())
			{
				var fieldField = new FactField
				{
					Name = subNode.Name.LocalName,
					Value = subNode.Attribute("val").Value,
				};

				factFields.Add(fieldField);
			}

			return new Fact
			{
				Name = factName,
				Fields = factFields,
			};
		}
	}
}
