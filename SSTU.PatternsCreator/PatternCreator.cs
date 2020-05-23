using SSTU.PatternsCreator.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;

namespace SSTU.PatternsCreator
{
	public class PatternsCreator
	{
		public readonly string Var1 = "Var1";
		public readonly string Var2 = "Var2";

		public List<OlsplPattern> CreateOlsplsFromOntology(string owlPath)
		{
			if (string.IsNullOrEmpty(owlPath))
			{
				return new List<OlsplPattern>();
			}

			var owlParser = new OwlParser(owlPath);

			var properties = owlParser.GetProperties();
			var classes = owlParser.GetClasses();

			var ontologyInfo = new OntologyInfo
			{
				OwlObjectProperties = properties,
				OwlClasses = classes,
			};

			SetGlobalOntologyUri(ontologyInfo);

			return GetOlspls(ontologyInfo);
		}

		public List<OlsplPattern> ReadOlsplsFromFile(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				return new List<OlsplPattern>();
			}

			var lines = File.ReadAllLines(filePath).Select(x =>x.Trim()).ToList();
			lines.RemoveAll(x => x == Environment.NewLine || x == string.Empty || x == "{" || x == "@olspl");

			var rowPatternBlocks = new List<List<string>>();
			var tempList = new List<string>();
			foreach(var line in lines)
			{
				if (line == "}")
				{
					rowPatternBlocks.Add(tempList);
					tempList = new List<string>();
				}
				else
				{
					tempList.Add(line);
				}
			}

			var result = new List<OlsplPattern>();
			foreach(var block in rowPatternBlocks)
			{
				var pattern = GetOlsplPatternFromBlock(block);
				result.Add(pattern);
			}

			return result;
		}
		
		private OlsplPattern GetOlsplPatternFromBlock(List<string> block)
		{
			if (block.Count != 4 && block.Count != 7)
			{
				var message = $"Неверный формат OLSPL-паттерна.{Environment.NewLine}" +
					$"Заголовок OLSPL-паттерна должен содержать комментарий и правило. Тело паттерна должно содержать 2 или 5 триплетов";
				throw new FormatException(message);
			}
			
			var comment = block[0];
			var rule = block[1];

			var entity1Type = GetEntityType(block[2], block[3]);
			var entity1Term = entity1Type.GetLocalName();

			var pattern = new OlsplPattern
			{
				Comment = comment,
				Rule = rule,
				Entity1 = new RdfEntity
				{
					EntityIndex = 1,
					Label = Var1,
					//Type = entity1Type,
					TypeTerm = entity1Term,
				},
			};

			if (block.Count == 7)
			{
				var entity2Type = GetEntityType(block[4], block[5]);
				var entity2Term = entity2Type.GetLocalName();

				pattern.Entity2 = new RdfEntity
				{
					EntityIndex = 2,
					Label = Var2,
					//Type = entity2Type,
					TypeTerm = entity2Term,
				};

				var tripletElems = block[6].Split(' ');
				pattern.TotalTriplet = new Triplet
				{
					RdfSubject = $"{Constants.OwntologyUri}#{Constants.NewEntity}1",
					RdfPredicate = tripletElems[1],
					RdfObject = $"{Constants.OwntologyUri}#{Constants.NewEntity}2",
				};
			}

			return pattern;
		}

		private string GetEntityType(string str1, string str2)
		{
			if (str1.Contains("rdf:type"))
			{
				return str1.Split(' ').Last();
			}
			if (str2.Contains("rdf:type"))
			{
				return str2.Split(' ').Last();
			}

			return null;
		}

		private void SetGlobalOntologyUri(OntologyInfo ontologyInfo)
		{
			var graphUri = ontologyInfo.OwlClasses.Select(x => x.ObjectClassURI.GraphUri).FirstOrDefault();

			if (graphUri == null)
			{
				graphUri = ontologyInfo.OwlObjectProperties.Select(x => x.ObjectPropertyURI.GraphUri).FirstOrDefault();
			}

			Constants.OwntologyUri = graphUri.ToString();
		}

		private List<OlsplPattern> GetOlspls(OntologyInfo ontologyInfo)
		{
			var patterns = new List<OlsplPattern>();

			patterns.AddRange(GetOlsplPatternsViaProperties(ontologyInfo.OwlObjectProperties));
			patterns.AddRange(GetOlsplPatternsViaClassesRestrictions(ontologyInfo.OwlClasses));
			patterns.AddRange(GetOlsplPatternsViaClassLabels(ontologyInfo.OwlClasses));

			return patterns;
		}

		private List<OlsplPattern> GetOlsplPatternsViaProperties(List<OwlObjectProperty> properties)
		{
			var patterns = new List<OlsplPattern>();

			foreach (var property in properties)
			{
				if (property.Domain == null || property.Range == null)
				{
					continue;
				}

				foreach (var label in property.Labels)
				{
					var pattern = GetPatternByLabelOfProperty(property, label);
					patterns.Add(pattern);
				}
			}

			return patterns;
		}
		private List<OlsplPattern> GetOlsplPatternsViaClassesRestrictions(List<OwlClass> classes)
		{
			var patterns = new List<OlsplPattern>();
			foreach (var owlClass in classes)
			{
				foreach (var restriction in owlClass.Restrictions)
				{
					if (restriction.ParentClassUri == null || (restriction.OwlOnPropertyUri == null))
					{
						continue;
					}

					foreach (var label in restriction.LabelsOfProperty)
					{
						var pattern = GetPatternByLabelOfRestriction(restriction, label);
						patterns.Add(pattern);
					}

				}
			}

			return patterns;
		}
		private List<OlsplPattern> GetOlsplPatternsViaClassLabels(List<OwlClass> classes)
		{
			var patterns = new List<OlsplPattern>();
			foreach (var owlClass in classes)
			{
				foreach (var label in owlClass.Labels)
				{
					var classUri = owlClass.ObjectClassURI;
					var classLocalName = classUri.GetLocalName();
					var rule1 = $"{classLocalName} - {label}";

					var entity = new RdfEntity
					{
						//Type = classUri.ToString(),
						TypeTerm = classLocalName,
						Label = Var1,
						EntityIndex = 1,
					};

					var comment = "Generated via class labels";

					var pattern = new OlsplPattern
					{
						Rule = rule1,
						Entity1 = entity,
						Comment = comment,
					};

					patterns.Add(pattern);
				}
			}

			return patterns;
		}

		private OlsplPattern GetPatternByLabelOfProperty(OwlObjectProperty property, INode label)
		{
			var domain = property.Domain;
			var range = property.Range;
			var domainLocalName = domain.GetLocalName();
			var rangeLocalName = range.GetLocalName();

			var rule = $"{domainLocalName} {label} {rangeLocalName}";
			var entity1 = new RdfEntity
			{
				//Type = domain.ToString(),
				TypeTerm = domainLocalName,
				Label = Var1,
				EntityIndex = 1,
			};

			var entity2 = new RdfEntity
			{
				//Type = range.ToString(),
				TypeTerm = rangeLocalName,
				Label = Var2,
				EntityIndex = 2,
			};

			var totalTriplet = new Triplet(domain, property, range, 1, 2);

			return new OlsplPattern
			{
				Rule = rule,
				Entity1 = entity1,
				Entity2 = entity2,
				TotalTriplet = totalTriplet,
				Comment = "Generated via domain and ranges",
			};
		}
		private OlsplPattern GetPatternByLabelOfRestriction(Restriction restriction, INode label)
		{
			var parentClass = restriction.ParentClassUri;
			var onClass = restriction.OwlOnClass;
			var parentClassLocalName = parentClass.GetLocalName();
			var onClassLocalName = onClass.GetLocalName();

			var rule = $"{parentClassLocalName} {label} {onClassLocalName}";

			var entity1 = new RdfEntity
			{
				//Type = parentClass.ToString(),
				TypeTerm = parentClassLocalName,
				Label = Var1,
				EntityIndex = 1,
			};

			var entity2 = new RdfEntity
			{
				//Type = onClass.ToString(),
				TypeTerm = onClassLocalName,
				Label = Var2,
				EntityIndex = 2,
			};

			var totalTriplet = new Triplet(parentClass, restriction, onClass, 1, 2);

			string comment = null;
			if (restriction.Cardinality == null)
			{
				comment = $"Generated via restriction - {restriction.RestrictionType}: {onClass}";
			}
			else
			{
				comment = $"Generated via restriction - {restriction.RestrictionType}: {restriction.Cardinality} for {onClass}";
			}


			return new OlsplPattern
			{
				Rule = rule,
				Entity1 = entity1,
				Entity2 = entity2,
				TotalTriplet = totalTriplet,
				Comment = comment,
			};
		}

	}

	public static class ExtClass
	{
		public static string GetLocalName(this INode node)
		{
			return node.ToString().Split('#').Last();
		}

		public static string GetLocalName(this string node)
		{
			return node.Split('#').Last();
		}
	}
}
