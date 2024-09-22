using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBeat.Domain.Entities
{
    public class ApiLog
    {
        public int Id { get; set; }
        public string RequestPath { get; set; }
        public string RequestMethod { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
