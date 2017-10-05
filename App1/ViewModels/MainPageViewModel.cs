using System.Collections.Generic;
using System.Linq;
using App1.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Windows.UI.Xaml.Navigation;

namespace App1.ViewModels
{
    public class MainPageViewModel : NavigatableViewModelBase
    {
        private string _text;
        private DelegateCommand _command;

        /// <summary>
        /// テキストを取得または設定します。
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                // Viewに対してプロパティに変更があったことを通知する。
                SetProperty(ref _text, value);

                // ボタンコマンドに対して、依存プロパティに変更があったことを通知する。
                Command.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// ボタンコマンドを取得します。
        /// </summary>
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
            : base(navigationService)
        {
            PageTitle = "Main";
        }

        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            // この画面からナビゲーションで旅立つときに呼ばれるメソッド

            base.OnNavigatedFrom(viewModelState, suspending);

            // Save text to ViewModelState
            viewModelState["Text"] = Text;
        }

        public override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            // ナビゲーションでこの画面に来たときに呼ばれるメソッド

            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            // Restore text from ViewModelState
            if (viewModelState?.ContainsKey("Text") == true)
            {
                Text = viewModelState["Text"] as string ?? "";
            }
        }
    }
}
