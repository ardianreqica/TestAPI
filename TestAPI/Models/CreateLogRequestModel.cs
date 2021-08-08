using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    public class CreateLogRequestModel
    {
        public List<CreateLogRequestModelRecord> records { get; set; }
    }

    public class CreateLogRequestModelRecord
    {
        public Fields fields { get; set; }
    }

}
