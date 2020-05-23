using MoreLinq;
using SSTU.FactsProcesser.Entities;
using SSTU.PatternsCreator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SSTU.FactsProcesser
{
	public class FactsProcesser
	{
		private int numOflastEntity = 1;
		public readonly string var1 = "Var1";
		public readonly string var2 = "Var2";
		public readonly string commonEntityName1 = "NewEntity1";
		public readonly string commonEntityName2 = "NewEntity2";

		public List<Fact> GetFactsFromFile(string filePath)
		{
			return GetFactsFromXml(filePath);
		}

		private List<Fact> GetFactsFromXml(string filePath)
		{
			var xmlDoc = XDocument.Load(filePath);
			var rowDocuments = xmlDoc.Descendants("fdo_objects").Elements();

			var result = new List<Fact>();
			foreach (var document in rowDocuments)
			{
				var factsFromDocument = GetFactsFromDocument(document);
				result.AddRange(factsFromDocument);
			}

			return result;
		}

		private List<Fact> GetFactsFromDocument(XElement document)
		{
			var rowFactsXml = document.Descendants("facts").Elements().ToList();
			var rowFacts = GetRowFacts(rowFactsXml);

			var factsGroupedByType = GetFactsGropedByType(rowFacts);

			var allPossibleCombinations = GetPossibleCombinationsFields(factsGroupedByType);

			var result = new List<Fact>();
			foreach (var combination in allPossibleCombinations)
			{
				var fact = GetMergedFact(combination);
				result.Add(fact);
			}

			return result;
		}

		private List<List<Fact>> GetFactsGropedByType(List<Fact> facts)
		{
			var lookup = facts.ToLookup(x => string.Join(" ", x.Fields.Select(y => y.Name).OrderBy(y => y)));
			var result = new List<List<Fact>>();
			foreach(var coll in lookup)
			{
				result.Add(coll.ToList());
			}

			return result;
		}

		private List<List<Fact>> GetPossibleCombinationsFields(List<List<Fact>> factsGroupedByType)
		{
			return GetPermutations<Fact>(factsGroupedByType).Select(x => x.ToList()).ToList();
		}

		public IEnumerable<IEnumerable<T>> GetPermutations<T>(
			IEnumerable<IEnumerable<T>> listOfLists)
		{
			return listOfLists.Skip(1).Aggregate(listOfLists.First()
			.Select(c => new List<T>() { c }), (previous, next) => previous
			.SelectMany(p => next.Select(d => new List<T>(p) { d })));
		}

		private List<Fact> GetRowFacts(List<XElement> rowFactsXml)
		{
			var result = new List<Fact>();
			foreach (var rowFact in rowFactsXml)
			{
				var fact = GetFactFromRowFactXml(rowFact);
				result.Add(fact);
			}

			return result;
		}

		private Fact GetFactFromRowFactXml(XElement rowFact)
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

		private Fact GetMergedFact(List<Fact> facts)
		{
			var initialFields = facts.SelectMany(x => x.Fields).ToList();

			var scientistFields = initialFields.Where(x => x.Name.StartsWith("Scientist"));
			var resultFields = initialFields.Except(scientistFields).ToList();
			if (scientistFields.Any())
			{
				var newFieldsValue = string.Join(" ", scientistFields.Where(x => x.Name != "Scientist_SurnameIsDictionary" && x.Name != "Scientist_SurnameIsLemma")
				.Select(x => x.Value));
				var newScientistField = new FactField
				{
					Name = "Scientist",
					Value = newFieldsValue,
				};
				resultFields.Add(newScientistField);
			}

			var resultFact = new Fact
			{
				Name = "Fact",
				Fields = resultFields,
			};

			return resultFact;
		}

		public List<OlsplPattern> GetTripletsFromFacts(List<OlsplPattern> patterns, List<Fact> facts)
		{
			CheckThatFactsUseSameTerms(patterns, facts);

			var distinctedPatterns = DistinctPatterns(patterns);

			var totalFilledPatterns = new List<OlsplPattern>();
			foreach (var fact in facts)
			{
				foreach (var pattern in distinctedPatterns)
				{
					var filledPatterns = GetFilledPattern(fact, pattern);
					if (filledPatterns != null)
					{
						totalFilledPatterns.Add(filledPatterns);
					}
				}
			}

			RecalculateNameOfEntities(totalFilledPatterns);

			return totalFilledPatterns;
		}

		private void CheckThatFactsUseSameTerms(List<OlsplPattern> patterns, List<Fact> facts)
		{
			var patternsTerms = patterns.Select(x => x.Entity1.TypeTerm)
				.Union(patterns.Where(x => x.Entity2 != null).Select(x => x.Entity2.TypeTerm));
			var factsTerms = facts.SelectMany(x => x.Fields.Select(y => y.Name)).Distinct();

			var extraTermsInFacts = factsTerms.Except(patternsTerms);

			if (extraTermsInFacts.Count() != 0)
			{
				var extraTermsStr = string.Join(", ", extraTermsInFacts);
				throw new FormatException(message: $"Факты содержат термины, которые отсутствуют в онтологии: {extraTermsStr}");
			}
		}

		public List<OlsplPattern> DistinctPatterns(List<OlsplPattern> patterns)
		{
			return patterns.DistinctBy(x => new
			{
				a = x.Entity1.Type,
				b = x.Entity1.Label,
				c = x.Entity2 != null ? x.Entity2.Type : "",
				d = x.Entity2 != null ? x.Entity2.Label : "",
				e = x.TotalTriplet != null ? x.TotalTriplet.TotalTriplet : "",
			}).ToList();
		}

		private OlsplPattern GetFilledPattern(Fact fact, OlsplPattern pattern)
		{
			var newPattern = CreateClone(pattern);
			var label1 = GetLabelFromFact(fact, newPattern.Entity1.TypeTerm);

			if (label1 == null)
			{
				return null;
			}

			newPattern.Entity1.Label = label1;

			if (newPattern.Entity2 != null)
			{
				var label2 = GetLabelFromFact(fact, newPattern.Entity2.TypeTerm);
				if (label2 == null)
				{
					return null;
				}

				newPattern.Entity2.Label = label2;
			}

			return newPattern;
		}

		private OlsplPattern CreateClone(OlsplPattern pattern)
		{
			var newPattern = new OlsplPattern();
			newPattern.Comment = pattern.Comment;
			newPattern.Rule = pattern.Rule;
			newPattern.Entity1 = new RdfEntity
			{
				EntityIndex = pattern.Entity1.EntityIndex,
				Label = pattern.Entity1.Label,
				//Type = pattern.Entity1.Type,
				TypeTerm = pattern.Entity1.TypeTerm,
			};
			
			if (pattern.TotalTriplet != null)
			{
				newPattern.Entity2 = new RdfEntity
				{
					EntityIndex = pattern.Entity2.EntityIndex,
					Label = pattern.Entity2.Label,
					//Type = pattern.Entity2.Type,
					TypeTerm = pattern.Entity2.TypeTerm,
				};
				newPattern.TotalTriplet = new Triplet
				{
					RdfSubject = pattern.TotalTriplet.RdfSubject,
					RdfPredicate = pattern.TotalTriplet.RdfPredicate,
					RdfObject = pattern.TotalTriplet.RdfObject,
				};
			}

			return newPattern;
		}

		private string GetLabelFromFact(Fact fact, string term)
		{
			if (term == null)
			{
				return null;
			}

			foreach (var field in fact.Fields)
			{
				if (term == field.Name)
				{
					return field.Value;
				}
			}

			return null;
		}

		private void RecalculateNameOfEntities(List<OlsplPattern> patterns)
		{
			SetNewEntityNamesForEntities(patterns);

			foreach (var pattern in patterns)
			{
				if (pattern.TotalTriplet != null)
				{
					var oldEntityName1 = (Constants.NewEntity + 1).ToString();
					var newEntityName1 = (Constants.NewEntity + pattern.Entity1.EntityIndex).ToString();
					pattern.TotalTriplet.RdfSubject = pattern.TotalTriplet.RdfSubject.Replace(oldEntityName1, newEntityName1);
					//pattern.TotalTriplet.EntityIndex1 = pattern.Entity1.EntityIndex;

					var oldEntityName2 = (Constants.NewEntity + 2).ToString();
					var newEntityName2 = (Constants.NewEntity + pattern.Entity2.EntityIndex).ToString();
					pattern.TotalTriplet.RdfObject = pattern.TotalTriplet.RdfObject.Replace(oldEntityName2, newEntityName2);
					//pattern.TotalTriplet.EntityIndex2 = pattern.Entity2.EntityIndex;
				}
			}
		}
		
		private void SetNewEntityNamesForEntities(IEnumerable<OlsplPattern> patterns)
		{
			var entitesTotal = patterns.Select(x => x.Entity1).Union(patterns.Select(x => x.Entity2)).Where(x => x != null);

			var tempList = new List<RdfEntity>();
			foreach (var entity in entitesTotal)
			{
				var entityWithSameTypeAndLabel = GetEntityWithSameTypeAndLabel(entity, tempList);
				if (entityWithSameTypeAndLabel.Count > 0)
				{
					entity.EntityIndex = entityWithSameTypeAndLabel.First().EntityIndex;
				}
				else
				{
					entity.EntityIndex = tempList.Count == 0 ? 1 : tempList.Max(x => x.EntityIndex) + 1;
				}

				tempList.Add(entity);
			}
		}

		private List<RdfEntity> GetEntityWithSameTypeAndLabel(RdfEntity entity, IEnumerable<RdfEntity> entities)
		{
			return entities.Where(x => x.TypeTerm == entity.TypeTerm && x.Label == entity.Label).ToList();
		}

	}
}
