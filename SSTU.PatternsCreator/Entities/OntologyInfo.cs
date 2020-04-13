using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTU.PatternsCreator.Entities
{
	public class OntologyInfo
	{
		public List<OwlObjectProperty> OwlObjectProperties { get; set; }
		public List<OwlClass> OwlClasses { get; set; }
	}
}
