using App1.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;

namespace App1.ViewModels
{
    public class SubPageViewModel : NavigatableViewModelBase
    {
        /// <summary>
        /// テキストを取得または設定します。
        /// </summary>
        public string Text { get; set; }

        public SubPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            PageTitle = "Sub";
        }

        public override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            // Called when navigation [MainPage -> SubPage]
            Text = navigationParameter as string ?? "Error";
        }
    }
}
