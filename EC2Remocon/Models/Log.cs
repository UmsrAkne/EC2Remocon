using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC2Remocon.Models {
    public class Log {

        public DateTime DateTime { get; private set; } = DateTime.Now;

        public String Status { get; private set; } = "";

        public EC2InstanceOperation Operation { get; private set; }

        public Log(EC2InstanceOperation operation, string status) {
            Status = status;
            Operation = operation;
        }

        public enum EC2InstanceOperation {
            start,
            stop,
            statusCheck
        }
    }
}
