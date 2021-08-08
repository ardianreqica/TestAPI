using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{



    public class ListLogResponseModel
    {
        public List<Record> Records { get; set; }
        public string Offset { get; set; }
    }
}
