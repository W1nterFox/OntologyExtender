using SSTU.PatternsCreator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace SSTU.PatternsCreator
{
	public class OwlParser
	{
		private readonly string prefixes = "PREFIX owl: <http://www.w3.org/2002/07/owl#> PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>";
		private readonly TripleStore store;

		public OwlParser(string owlFilePath)
		{
			this.store = LoadStore(owlFilePath);
		}

		public TripleStore LoadStore(string owlFilePath)
		{
			var store = new TripleStore();
			var g = new Graph();
			FileLoader.Load(g, owlFilePath);
			store.Add(g);
			return store;
		}

		#region ForProperties
		public List<OwlObjectProperty> GetProperties()
		{
			var owlObjectProperties = new List<OwlObjectProperty>();
			var query = "SELECT * WHERE { { GRAPH ?g { ?propertyUri rdf:type owl:ObjectProperty. } } }";

			var properties = ExecuteQuery(query);

			foreach (var property in properties)
			{
				var owlPropertyUri = property["propertyUri"];
				var domain = GetDomainOfProperty(owlPropertyUri.ToString());
				var range = GetRangeOfProperty(owlPropertyUri.ToString());
				var labels = GetLabelsOfProperty(owlPropertyUri.ToString());

				owlObjectProperties.Add(new OwlObjectProperty
				{
					ObjectPropertyURI = owlPropertyUri,
					Domain = domain,
					Range = range,
					Labels = labels
				});
			}

			return owlObjectProperties;
		}

		private List<INode> GetLabelsOfProperty(string propertyUri)
		{
			var queryString = new SparqlParameterizedString();
			queryString.Namespaces.AddNamespace("rdfs", new Uri("http://www.w3.org/2000/01/rdf-schema#"));
			queryString.CommandText = "SELECT * WHERE { { GRAPH ?g { <" + propertyUri + "> rdfs:label ?label. } } }";

			var labelsOfProperty = ExecuteQuery(queryString.ToString());

			var result = labelsOfProperty.Select(x => x["label"]);

			return result.ToList();
		}

		private INode GetDomainOfProperty(string propertyUri)
		{
			var query = "SELECT * WHERE { { GRAPH ?g { <" + propertyUri + "> rdfs:domain ?domain. } } }";

			var domains = ExecuteQuery(query);

			return domains.Select(x => x["domain"]).FirstOrDefault();
		}

		private INode GetRangeOfProperty(string propertyUri)
		{
			var query = "SELECT * WHERE { { GRAPH ?g { <" + propertyUri + "> rdfs:range ?range. } } }";

			var ranges = ExecuteQuery(query);

			return ranges.Select(x => x["range"]).FirstOrDefault();
		}
		#endregion

		#region ForClasses
		public List<OwlClass> GetClasses()
		{
			var owlClasses = new List<OwlClass>();
			var query = "SELECT * WHERE { { GRAPH ?g { ?owlClassUri rdf:type owl:Class. } } }";
			var classes = ExecuteQuery(query);

			foreach (var cl in classes)
			{
				var owlClassUri = cl["owlClassUri"];

				var owlClass = ParseClassFromSparql(cl["owlClassUri"]);
				owlClasses.Add(owlClass);
			}

			return owlClasses;
		}

		private OwlClass ParseClassFromSparql(INode classUri)
		{
			var restrictions = GetRestrictions(classUri);
			var labels = GetClassLabels(classUri);

			return new OwlClass
			{
				Labels = labels,
				Restrictions = restrictions,
				ObjectClassURI = classUri,
			};
		}

		private List<Restriction> GetRestrictions(INode classUri)
		{
			var restrictions = new List<Restriction>();
			restrictions.AddRange(GetRestrictions(classUri, RestrictionType.AllValuesFrom));
			restrictions.AddRange(GetRestrictions(classUri, RestrictionType.SomeValuesFrom));
			restrictions.AddRange(GetRestrictions(classUri, RestrictionType.MaxQualifiedCardinality));
			restrictions.AddRange(GetRestrictions(classUri, RestrictionType.QualifiedCardinality));
			restrictions.AddRange(GetRestrictions(classUri, RestrictionType.MinQualifiedCardinality));
			return restrictions;
		}

		private List<Restriction> GetRestrictions(INode classUri, RestrictionType restrictionType)
		{
			var query = GetQueryForClassRestrictionsByType(classUri, restrictionType);
			var restrictionsSparqlSet = ExecuteQuery(query);
			var listOfRestrictions = new List<Restriction>();

			foreach (var restrictionSparql in restrictionsSparqlSet)
			{
				var restriction = new Restriction
				{
					RestrictionType = restrictionType,
					OwlOnPropertyUri = restrictionSparql["onPropertyUri"],
					OwlOnClass = restrictionSparql["onClass"],
					ParentClassUri = classUri,
					LabelsOfProperty = GetLabelsOfProperty(restrictionSparql["onPropertyUri"].ToString()),
				};

				if (restrictionType == RestrictionType.MaxQualifiedCardinality 
					|| restrictionType == RestrictionType.QualifiedCardinality
					|| restrictionType == RestrictionType.MinQualifiedCardinality)
				{
					restriction.Cardinality = restrictionSparql["cardinality"];
				}

				listOfRestrictions.Add(restriction);
			}

			return listOfRestrictions;
		}

		private string GetQueryForClassRestrictionsByType(INode classUri, RestrictionType restrictionType)
		{
			var queryAllValuesFrom = "SELECT * WHERE { { GRAPH ?g { ?restriction owl:onProperty ?onPropertyUri. ?restriction owl:allValuesFrom ?onClass. <" + classUri + "> rdfs:subClassOf ?restriction.} } }";
			var querySomeValuesFrom = "SELECT * WHERE { { GRAPH ?g { ?restriction owl:onProperty ?onPropertyUri. ?restriction owl:someValuesFrom ?onClass. <" + classUri + "> rdfs:subClassOf ?restriction.} } }";
			var queryMinCardinality = "SELECT * WHERE { { GRAPH ?g { ?restriction owl:onProperty ?onPropertyUri. ?restriction owl:minQualifiedCardinality ?cardinality. ?restriction owl:onClass ?onClass. <" + classUri + "> rdfs:subClassOf ?restriction.} } }";
			var queryCardinality = "SELECT * WHERE { { GRAPH ?g { ?restriction owl:onProperty ?onPropertyUri. ?restriction owl:qualifiedCardinality ?cardinality. ?restriction owl:onClass ?onClass. <" + classUri + "> rdfs:subClassOf ?restriction.} } }";
			var queryMaxCardinality = "SELECT * WHERE { { GRAPH ?g { ?restriction owl:onProperty ?onPropertyUri. ?restriction owl:maxQualifiedCardinality ?cardinality. ?restriction owl:onClass ?onClass. <" + classUri + "> rdfs:subClassOf ?restriction.} } }";

			switch (restrictionType)
			{
				case RestrictionType.AllValuesFrom:
					{
						return queryAllValuesFrom;
					}
				case RestrictionType.SomeValuesFrom:
					{
						return querySomeValuesFrom;
					}
				case RestrictionType.MaxQualifiedCardinality:
					{
						return queryMaxCardinality;
					}
				case RestrictionType.QualifiedCardinality:
					{
						return queryCardinality;
					}
				case RestrictionType.MinQualifiedCardinality:
					{
						return queryMinCardinality;
					}
				default:
					{
						throw new ArgumentOutOfRangeException("Can`t find appropriate restrictionType");
					}
			}
		}

		private List<INode> GetClassLabels(INode classUri)
		{
			var query = "SELECT * WHERE { { GRAPH ?g { <" + classUri + "> rdfs:label ?label } } }";
			var labels = ExecuteQuery(query);

			return labels.Select(x => x["label"]).ToList();
		}

		#endregion

		private SparqlResultSet ExecuteQuery(string query)
		{
			var result = store.ExecuteQuery(prefixes + query);

			return (SparqlResultSet)result;
		}
	}
}
