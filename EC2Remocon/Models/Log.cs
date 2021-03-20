using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EC2Remocon.Models {
    public class Log {

        public DateTime DateTime { get; private set; } = DateTime.Now;

        public String Status { get; private set; } = "";

        public EC2InstanceOperation Operation { get; private set; }

        public Log(EC2InstanceOperation operation, string status) {
            Operation = operation;

            var regex = new Regex("\"State\": \"(.*)\"", RegexOptions.IgnoreCase);
            var match = regex.Match(status);
            Status = match.Groups[1].Value;
        }

        public enum EC2InstanceOperation {
            start,
            stop,
            statusCheck,
            autoStatusCheck,
            other
        }
    }
}
