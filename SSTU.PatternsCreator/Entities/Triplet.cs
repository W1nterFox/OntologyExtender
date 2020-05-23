using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;

namespace SSTU.PatternsCreator.Entities
{
	public class Triplet
	{
		public string RdfSubject { get; set; }
		public string RdfPredicate { get; set; }
		public string RdfObject { get; set; }
		public string TotalTriplet => $"{RdfSubject} {RdfPredicate} {RdfObject}";

		public Triplet()
		{

		}

		public Triplet(INode parentClass, Restriction restriction, INode onClass, int entityIndex1, int entityIndex2)
		{

			RdfSubject = $"{parentClass.GraphUri}#{Constants.NewEntity + entityIndex1}";
			RdfPredicate = restriction.OwlOnPropertyUri.GetLocalName();
			RdfObject = $"{onClass.GraphUri}#{Constants.NewEntity + entityIndex2}";
		}

		public Triplet(INode domain, OwlObjectProperty property, INode range, int entityIndex1, int entityIndex2)
		{
			RdfSubject = $"{domain.GraphUri}#{Constants.NewEntity + entityIndex1}";
			RdfPredicate = property.ObjectPropertyURI.GetLocalName();
			RdfObject = $"{range.GraphUri}#{Constants.NewEntity + entityIndex2}";
		}

		
	}
}
