using SSTU.PatternsCreator.Entities;
using System;
using System.Collections.Generic;
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

		public List<OlsplPattern> CreateOlspls(string owlPath)
		{
			var owlParser = new OwlParser(owlPath);

			var properties = owlParser.GetProperties();
			var classes = owlParser.GetClasses();

			var ontologyInfo = new OntologyInfo
			{
				OwlObjectProperties = properties,
				OwlClasses = classes,
			};

			return GetOlspls(ontologyInfo);
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
					var rule1 = $"{Var1} - {label}";
					var rule2 = $"{Var1} - это {label}";

					var classUri = owlClass.ObjectClassURI;
					var firstEntityTypeTriplet = $"{classUri.GraphUri}NewEntity <rdf:type> {classUri}";
					var firstEntityLabelTriplet = $"{classUri.GraphUri}NewEntity <rdfs:label> {Var1}";
					var comment = "Generated via class labels";

					var pattern1 = new OlsplPattern
					{
						Rule = rule1,
						EntityType1 = firstEntityTypeTriplet,
						EntityLabel1 = firstEntityLabelTriplet,
						//PatternType = PatternType.SimplePattern,
						Comment = comment,
					};
					var pattern2 = new OlsplPattern
					{
						Rule = rule2,
						EntityType1 = firstEntityTypeTriplet,
						EntityLabel1 = firstEntityLabelTriplet,
						//PatternType = PatternType.SimplePattern,
						Comment = comment,
					};

					patterns.Add(pattern1);
					patterns.Add(pattern2);
				}
			}

			return patterns;
		}

		private OlsplPattern GetPatternByLabelOfProperty(OwlObjectProperty property, INode label)
		{
			var rule = $"{Var1} {label} {Var2}";
			var domain = property.Domain;
			var range = property.Range;

			var entityType1 = $"{domain.GraphUri}NewEntity1 <rdf:type> {domain}";
			var entityLabel1 = $"{domain.GraphUri}NewEntity1 <rdfs:label> {Var1}";
			var entityType2 = $"{range.GraphUri}NewEntity2 <rdf:type> {range}";
			var entityLabel2 = $"{range.GraphUri}NewEntity2 <rdfs:label> {range}";
			var totalTriplet = $"{domain.GraphUri}NewEntity1 {property.ObjectPropertyURI} {range.GraphUri}NewEntity2";

			return new OlsplPattern
			{
				Rule = rule,
				EntityType1 = entityType1,
				EntityType2 = entityType2,
				EntityLabel1 = entityLabel1,
				EntityLabel2 = entityLabel2,
				TotalTriplet = totalTriplet,
				//PatternType = PatternType.FullPattern,
				Comment = "Generated via domain and ranges",
			};
		}
		private OlsplPattern GetPatternByLabelOfRestriction(Restriction restriction, INode label)
		{
			var rule = $"{Var1} {label} {Var2}";
			var parentClass = restriction.ParentClassUri;
			var onClass = restriction.OwlOnClass;

			var entityType1 = $"{parentClass.GraphUri}NewEntity1 <rdf:type> {parentClass}";
			var entityLabel1 = $"{parentClass.GraphUri}NewEntity1 <rdfs:label> {Var1}";
			var entityType2 = $"{onClass.GraphUri}NewEntity2 <rdf:type> {onClass}";
			var entityLabel2 = $"{onClass.GraphUri}NewEntity2 <rdfs:label> {Var2}";
			var totalTriplet = $"{parentClass.GraphUri}NewEntity1 {restriction.OwlOnPropertyUri} {onClass.GraphUri}NewEntity2";

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
				EntityType1 = entityType1,
				EntityLabel1 = entityLabel1,
				EntityType2 = entityType2,
				EntityLabel2 = entityLabel2,
				TotalTriplet = totalTriplet,
				//PatternType = PatternType.FullPattern,
				Comment = comment,
			};
		}

	}
}
