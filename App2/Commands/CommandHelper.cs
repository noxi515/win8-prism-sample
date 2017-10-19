using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using System.Windows.Input;

namespace App2.Commands
{
    public static class CommandHelper
    {
        /// <summary>
        /// 戻るボタンのコマンドを生成します。
        /// </summary>
        public static ICommand CreateGoBackNavigationCommand(INavigationService navigationService)
        {
            return new DelegateCommand(
                () => navigationService.GoBack(),
                () => navigationService.CanGoBack());
        }
    }
}
