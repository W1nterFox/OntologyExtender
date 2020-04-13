using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;

namespace SSTU.PatternsCreator.Entities
{
	public class OwlClass
	{
		public INode ObjectClassURI { get; set; }
		public List<Restriction> Restrictions { get; set; }
		public List<INode> Labels { get; set; }
	}
}
