using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;

namespace SSTU.PatternsCreator.Entities
{
	public class OwlObjectProperty
	{
		public INode ObjectPropertyURI { get; set; }
		public INode Domain { get; set; }
		public INode Range { get; set; }
		public List<INode> Labels { get; set; }
	}
}
