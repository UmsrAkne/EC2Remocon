using Prism.Mvvm;
using System;
using EC2Remocon.Models;
using Prism.Commands;

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

        private CLICommand CLICommand { get; } = new CLICommand();

        public MainWindowViewModel() {

        }


        public DelegateCommand StartEC2InstanceCommand {
            #region
            get => startEC2InstanceCommand ?? (startEC2InstanceCommand = new DelegateCommand(() => {
                Output += Environment.NewLine + CLICommand.startEC2Instance();
            }));
        }
        private DelegateCommand startEC2InstanceCommand;
        #endregion


        public DelegateCommand StopEC2InstanceCommand {
            #region
            get => stopEC2InstanceCommand ?? (stopEC2InstanceCommand = new DelegateCommand(() => {
                Output += Environment.NewLine + CLICommand.stopEC2Instance();
            }));
        }
        private DelegateCommand stopEC2InstanceCommand;
        #endregion


        public DelegateCommand GetEC2InstanceStatusCommand {
            #region
            get => getEC2InstanceStatusCommand ?? (getEC2InstanceStatusCommand = new DelegateCommand(() => {
                Output += Environment.NewLine + CLICommand.getEC2InstanceStatus();
            }));
        }
        private DelegateCommand getEC2InstanceStatusCommand;
        #endregion


    }
}
