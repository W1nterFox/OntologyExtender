using SSTU.PatternsCreator.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Writing;

namespace SSTU.PatternsCreator
{
	public class OntologyFiller
	{
		public string GetExtendedOntologyByPatterns(string owlPath, List<OlsplPattern> patterns)
		{
			var totalIndividuals = new List<string>();
			foreach (var pattern in patterns)
			{
				if (pattern.TotalTriplet != null)
				{
					var individuals = CreateInvividualsForComplexPattern(pattern);
					totalIndividuals.AddRange(individuals);
				}
				else
				{
					var individuals = CreateInvividualsForSimplePattern(pattern);
					totalIndividuals.AddRange(individuals);
				}
			}
			return GetOntologyWithNewTriplets(owlPath, totalIndividuals);
		}

		public List<string> CreateInvividualsForComplexPattern(OlsplPattern pattern)
		{
			var result = new List<string>();
			var individualBlock1 = $"   <owl:NamedIndividual rdf:about=\"{pattern.TotalTriplet.RdfSubject}\">\n"
								+ $"      <rdf:type rdf:resource = \"{pattern.Entity1.Type}\" />\n"
								+ $"      <rdfs:label>{pattern.Entity1.Label}</rdfs:label>\n"
								+ $"      <{pattern.TotalTriplet.RdfPredicate} rdf:resource = \"{pattern.TotalTriplet.RdfObject}\"/>\n"
								+ "   </owl:NamedIndividual>\n\n";

			var individualBlock2 = $"   <owl:NamedIndividual rdf:about=\"{pattern.TotalTriplet.RdfObject}\">\n"
								+ $"      <rdf:type rdf:resource = \"{pattern.Entity2.Type}\" />\n"
								+ $"      <rdfs:label>{pattern.Entity2.Label}</rdfs:label>\n"
								+ "   </owl:NamedIndividual>\n\n";

			result.Add(individualBlock1);
			result.Add(individualBlock2);
			
			return result;
		}

		public List<string> CreateInvividualsForSimplePattern(OlsplPattern pattern)
		{
			var result = new List<string>();
			var individualBlock = $"   <owl:NamedIndividual rdf:about=\"{pattern.Entity1.Name}\">\n"
									+ $"      <rdf:type rdf:resource = \"{pattern.Entity1.Type}\" />\n"
									+ $"      <rdfs:label>{pattern.Entity1.Label}</rdfs:label>\n"
									+ "   </owl:NamedIndividual>\n\n";


			result.Add(individualBlock);
			return result;
		}

		public string GetOntologyWithNewTriplets(string owlFileName, List<string> individuals)
		{
			var blockForInsert = string.Join("", individuals);
			var endRdf = @"</rdf:RDF>";
			var owlText = File.ReadAllText(owlFileName);
			owlText = owlText.Replace(endRdf, "");

			owlText += blockForInsert;

			owlText += endRdf;

			return owlText;
		}
	}
}
