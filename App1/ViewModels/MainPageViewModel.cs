using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;

namespace App1.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private INavigationService NavigationService { get; }

        private string _text;
        private DelegateCommand _command;

        public string Text
        {
            get { return _text; }
            set
            {
                SetProperty(ref _text, value);
                Command.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand Command
        {
            get
            {
                if (_command != null)
                {
                    return _command;
                }

                _command = new DelegateCommand(
                    () => NavigationService.Navigate("Sub", Text),  // Button action (Executed when button clicked)
                    () => !string.IsNullOrEmpty(Text)               // Button is enabled or disabled
                );

                return _command;
            }
        }

        public MainPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }


    }
}
