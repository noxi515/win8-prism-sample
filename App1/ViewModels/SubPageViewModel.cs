using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;

namespace App1.ViewModels
{
    public class SubPageViewModel : ViewModel
    {
        public string Text { get; set; }

        public override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            // Called when navigation [MainPage -> SubPage]
            Text = navigationParameter as string ?? "Error";
        }

    }
}
