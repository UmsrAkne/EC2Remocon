using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EC2Remocon.Models {
    public class CLICommand {

        private Process process = new Process() {
            StartInfo = new ProcessStartInfo() {
                FileName = System.Environment.GetEnvironmentVariable("ComSpec"),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = false,
                CreateNoWindow = true
            }
        };

        /// <summary>
        /// EC2インスタンスの InstanceId を標準出力から取得します。
        /// このメソッドは、複数の EC2 インスタンスをもつ AMI で実行した場合を想定しておらず、正しく動作しない可能性があります。
        /// </summary>
        /// <returns>EC2の InstanceId が返却されます。</returns>
        public string getEC2InstanceId() {
            string text = getEC2InstanceStatus();
            var regex = new Regex("\"InstanceId\": \"(.*)\"", RegexOptions.IgnoreCase);
            var matches = regex.Matches(text);
            return matches[0].Groups[1].Value;
        }

        /// <summary>
        /// 指定した id のEC2インスタンスを起動します。
        /// 注意 : インスタンス作成ではなく、停止済みのインスタンスの起動。
        /// </summary>
        /// <param name="instanceName"></param>
        /// <returns>aws ec2 start-instances 実行の際の標準出力を返します。</returns>
        public string startEC2Instance(string instanceName) {
            process.StartInfo.Arguments = $"/c aws ec2 start-instances --instance-ids {instanceName}";
            process.Start();
            return process.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// getEC2InstanceId() で取得される id を引数にして、このメソッドのオーバーロードを実行します。
        /// </summary>
        /// <returns>aws ec2 start-instances 実行時の標準出力を返します。</returns>
        public string startEC2Instance() {
            return startEC2Instance(getEC2InstanceId());
        }

        public Task<string> startEC2InstanceAsync() {
            return Task.Run(() => startEC2Instance());
        }

        /// <summary>
        /// 指定した id の EC2インスタンスを停止します。
        /// 注意 : インスタンス終了（削除）ではなく一時停止コマンドを実行します。
        /// </summary>
        /// <param name="instanceName"></param>
        /// <returns>aws ec2 stop-instances 実行時の標準出力を返します。</returns>
        public string stopEC2Instance(string instanceName) {
            process.StartInfo.Arguments = $"/c aws ec2 stop-instances --instance-ids {instanceName}";
            process.Start();
            return process.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// getEC2InstanceId() で取得される id を引数にして、このメソッドのオーバーロードを実行します。
        /// </summary>
        /// <returns>aws ec2 stop-instances 実行時の標準出力を返します。</returns>
        public string stopEC2Instance() {
            return stopEC2Instance(getEC2InstanceId());
        }

        public Task<string> stopEC2InstanceAsync() {
            return Task.Run(() => stopEC2Instance());
        }

        public string getEC2InstanceStatus() {
            return describeEC2Instance();
        }

        public string describeEC2Instance() {
            process.StartInfo.Arguments = @"/c aws ec2 describe-instances";
            process.Start();
            return process.StandardOutput.ReadToEnd();
        }

        public Task<string> getEC2InstanceStatusAsync() {
            return Task.Run(() => getEC2InstanceStatus());
        }
    }
}
