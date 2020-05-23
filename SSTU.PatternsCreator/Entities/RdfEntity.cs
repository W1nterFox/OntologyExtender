using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTU.PatternsCreator.Entities
{
	public class RdfEntity
	{
		public string Type => $"{Constants.OwntologyUri}#{TypeTerm}";
		public string TypeTerm { get; set; }
		public string Label { get; set; }
		public string Name  => $"{Constants.OwntologyUri}#{Constants.NewEntity}{EntityIndex}";
		public int EntityIndex { get; set; }
	}
}
