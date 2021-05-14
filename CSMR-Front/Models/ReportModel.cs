using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSMR_Front.Models
{
    public class ReportModel
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string IIN { get; set; }
        public decimal Result { get; set; }
        public DateTime SendDate { get; set; }
    }
    public class ReportData
    {
        public int AgentId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
