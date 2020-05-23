using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTU.PatternsCreator.Entities
{
	public class OlsplPattern
	{
		public string Rule { get; set; }
		public RdfEntity Entity1 { get; set; }
		public RdfEntity Entity2 { get; set; }
		public Triplet TotalTriplet { get; set; }
		public string Comment { get; set; }
	}
}
