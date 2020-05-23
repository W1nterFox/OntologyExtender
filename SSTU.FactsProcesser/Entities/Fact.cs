using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTU.FactsProcesser.Entities
{
	public class Fact
	{
		public string Name { get; set; }
		public List<FactField> Fields { get; set; } = new List<FactField>();
	}
}
