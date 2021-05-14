using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSMR_Front.Models
{
    public class QueryModel
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public int QueryCount { get; set; }
        public DateTime AddDate { get; set; }
        public string FileBite { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
    }
    public class QueryRequestData
    {
        public int AgentId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
