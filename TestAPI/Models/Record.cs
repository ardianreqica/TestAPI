using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    public class Record
    {
        public string Id { get; set; }
        public Fields Fields { get; set; }
        public DateTime CreatedTime { get; set; }
    }

}
