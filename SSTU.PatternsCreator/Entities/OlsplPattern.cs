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
		public string EntityType1 { get; set; }
		public string EntityType2 { get; set; }
		public string EntityLabel2 { get; set; }
		public string EntityLabel1 { get; set; }
		public string TotalTriplet { get; set; }
		public string Comment { get; set; }
	}
}
