using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer_mpp
{
	interface ISerializer
	{
		string Serialize(TraceResult traceResult);
	}
}
