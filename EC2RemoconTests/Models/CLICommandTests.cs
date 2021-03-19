using Microsoft.VisualStudio.TestTools.UnitTesting;
using EC2Remocon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC2Remocon.Models.Tests {
    [TestClass()]
    public class CLICommandTests {
        [TestMethod()]
        public void getEC2InstanceIdTest() {
            var cliCommad = new CLICommand();
            System.Diagnostics.Debug.WriteLine(cliCommad.getEC2InstanceId());
        }

        [TestMethod()]
        public void startEC2InstanceTest() {
            var cliCommand = new CLICommand();
            System.Diagnostics.Debug.WriteLine(cliCommand.startEC2Instance());
        }

        [TestMethod()]
        public void stopEC2InstanceTest() {
            System.Diagnostics.Debug.WriteLine(new CLICommand().stopEC2Instance());
        }

        [TestMethod()]
        public void describeEC2InstanceTest() {
            System.Diagnostics.Debug.WriteLine(new CLICommand().describeEC2Instance());
        }
    }
}