using Prism.Mvvm;
using System;
using EC2Remocon.Models;
using Prism.Commands;
using System.Collections.ObjectModel;

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

        public MainWindowViewModel() {
            Log.Add(new Log(Models.Log.EC2InstanceOperation.other, "init"));
        }


        public DelegateCommand StartEC2InstanceCommand {
            #region
            get => startEC2InstanceCommand ?? (startEC2InstanceCommand = new DelegateCommand(() => {
                var stdOut = CLICommand.startEC2Instance();
                Output += Environment.NewLine + stdOut;
                Log.Add(new Log(Models.Log.EC2InstanceOperation.start, stdOut));
            }));
        }
        private DelegateCommand startEC2InstanceCommand;
        #endregion


        public DelegateCommand StopEC2InstanceCommand {
            #region
            get => stopEC2InstanceCommand ?? (stopEC2InstanceCommand = new DelegateCommand(() => {
                var stdOut = CLICommand.stopEC2Instance();
                Output += Environment.NewLine + stdOut;
                Log.Add(new Log(Models.Log.EC2InstanceOperation.stop, stdOut));
            }));
        }
        private DelegateCommand stopEC2InstanceCommand;
        #endregion


        public DelegateCommand GetEC2InstanceStatusCommand {
            #region
            get => getEC2InstanceStatusCommand ?? (getEC2InstanceStatusCommand = new DelegateCommand(() => {
                var stdOut = CLICommand.getEC2InstanceStatus();
                Output += Environment.NewLine + stdOut;
                Log.Add(new Log(Models.Log.EC2InstanceOperation.statusCheck, stdOut));
            }));
        }
        private DelegateCommand getEC2InstanceStatusCommand;
        #endregion


    }
}
