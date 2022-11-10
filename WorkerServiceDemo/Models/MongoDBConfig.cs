using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerServiceDemo.Models
{
    internal class MongoDBConfig
    {
        public string? ClientDatabase { get; set; }
        public string? HistoryCollection { get; set; }
        public string? LiveCollection { get; set; }
        public string? ConnectionString { get; set; }
        public int NoOfDays { get; set; }
    }
}
