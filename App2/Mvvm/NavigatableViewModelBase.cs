using App2.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using System.Windows.Input;

namespace App2.Mvvm
{
    public abstract class NavigatableViewModelBase : ViewModel
    {
        /// <summary>
        /// ページナビゲーションに関するサービスを取得します。
        /// </summary>
        public INavigationService NavigationService { get; }

        /// <summary>
        /// ページタイトルを取得または設定します。
        /// </summary>
        public virtual string PageTitle { get; set; }

        /// <summary>
        /// 戻るボタンのコマンドを取得します。
        /// </summary>
        public ICommand GoBackCommand { get; }


        protected NavigatableViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            GoBackCommand = CommandHelper.CreateGoBackNavigationCommand(navigationService);
        }
    }
}
