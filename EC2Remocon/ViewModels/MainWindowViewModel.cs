using Prism.Mvvm;
using System;
using EC2Remocon.Models;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace EC2Remocon.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string output = "";
        public string Output {
            get => output;
            set => SetProperty(ref output, value);
        }

        public ObservableCollection<Log> Log { get; private set; } = new ObservableCollection<Log>();

        private CLICommand CLICommand { get; } = new CLICommand();

        private int autoStatusCheckCounter = 0;
        private TimeSpan autoStatusCheckInterval = new TimeSpan(0, 30, 0);
        private DispatcherTimer timer = new DispatcherTimer();

        public MainWindowViewModel() {
            Log.Add(new Log(Models.Log.EC2InstanceOperation.other, "init"));
            GetEC2InstanceStatusCommand.Execute();

            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Tick += (sender, e) => {
                autoStatusCheckCounter++;
                if(autoStatusCheckCounter >= autoStatusCheckInterval.Minutes) {
                    autoStatusCheckCounter = 0;
                    addLog(Models.Log.EC2InstanceOperation.autoStatusCheck, CLICommand.getEC2InstanceStatus());
                }
            };

            timer.Start();
        }

        private void addLog(Log.EC2InstanceOperation operation, String standardOutput) {
            Output += Environment.NewLine + standardOutput;
            Log.Add(new Log(operation, standardOutput));
        }

        public DelegateCommand StartEC2InstanceCommand {
            #region
            get => startEC2InstanceCommand ?? (startEC2InstanceCommand = new DelegateCommand(async() => {
                addLog(Models.Log.EC2InstanceOperation.start, await CLICommand.startEC2InstanceAsync());
            }));
        }
        private DelegateCommand startEC2InstanceCommand;
        #endregion


        public DelegateCommand StopEC2InstanceCommand {
            #region
            get => stopEC2InstanceCommand ?? (stopEC2InstanceCommand = new DelegateCommand(async() => {
                addLog(Models.Log.EC2InstanceOperation.stop, await CLICommand.stopEC2InstanceAsync());
            }));
        }
        private DelegateCommand stopEC2InstanceCommand;
        #endregion


        public DelegateCommand GetEC2InstanceStatusCommand {
            #region
            get => getEC2InstanceStatusCommand ?? (getEC2InstanceStatusCommand = new DelegateCommand(async() => {
                addLog(Models.Log.EC2InstanceOperation.statusCheck, await CLICommand.getEC2InstanceStatusAsync());
            }));
        }
        private DelegateCommand getEC2InstanceStatusCommand;
        #endregion

    }
}
