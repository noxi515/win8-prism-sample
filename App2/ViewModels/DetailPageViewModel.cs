using System.Collections.Generic;
using App2.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Windows.UI.Xaml.Navigation;
using App2.Repository;
using System;

namespace App2.ViewModels
{
    public class DetailPageViewModel : NavigatableViewModelBase
    {
        #region Fields

        private readonly IMemoRepository _repository;

        private string _text;
        private int _id;
        private DelegateCommand _deleteCommand;

        #endregion

        #region Properties

        /// <summary>
        /// テキストを取得または設定します。
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                SetProperty(ref _text, value);
            }
        }

        /// <summary>
        /// 削除ボタンのコマンドを取得します。
        /// </summary>
        public DelegateCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand != null)
                {
                    return _deleteCommand;
                }

                return _deleteCommand = new DelegateCommand(
                    async () =>
                    {
                        // メモ削除
                        await _repository.DeleteAsync(_id);

                        // 画面戻る
                        NavigationService.GoBack();
                    });
            }
        }

        #endregion

        #region Constructors

        public DetailPageViewModel(INavigationService navigationService, IMemoRepository repository)
            : base(navigationService)
        {
            _repository = repository;
            PageTitle = "Detail";
        }

        #endregion

        #region Methods

        public override async void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            // 表示するメモのIDを取得
            if (!(navigationParameter is int))
            {
                throw new InvalidOperationException();
            }

            _id = (int)navigationParameter;

            // メモを取得
            var memo = await _repository.GetAsync(_id);
            Text = memo?.Text;
        }

        #endregion
    }
}
