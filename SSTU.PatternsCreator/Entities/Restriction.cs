using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;

namespace SSTU.PatternsCreator.Entities
{
	public class Restriction
	{
		public INode OwlOnPropertyUri { get; set; }
		public List<INode> LabelsOfProperty { get; set; }
		public RestrictionType RestrictionType { get; set; }
		public INode ParentClassUri { get; set; }
		public INode OwlOnClass { get; set; }
		public INode Cardinality { get; set; }
	}
}
