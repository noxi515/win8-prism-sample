using System.Collections.Generic;
using App2.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Windows.UI.Xaml.Navigation;
using App2.Repository;
using System.Threading.Tasks;
using App2.Data;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace App2.ViewModels
{
    public class ListPageViewModel : NavigatableViewModelBase
    {
        #region Fields

        private readonly IMemoRepository _repository;

        private string _text;
        private DelegateCommand _addCommand;
        private DelegateCommand<MemoItemViewModel> _memoItemClickCommand;

        #endregion

        #region Properties

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
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// 追加ボタンのコマンドを取得します。
        /// </summary>
        public DelegateCommand AddCommand
        {
            get
            {
                if (_addCommand != null)
                {
                    return _addCommand;
                }

                return _addCommand = new DelegateCommand(
                    async () =>
                    {
                        var text = Text;
                        // 表示文字を消す
                        Text = "";

                        // メモ追加
                        await AddMemoAsync(text);
                        // 表示リスト更新
                        await UpdateListAsync();
                    },
                    () => !string.IsNullOrEmpty(Text));
            }
        }

        /// <summary>
        /// メモリストのアイテムをクリックしたときのコマンドを取得します。
        /// </summary>
        public DelegateCommand<MemoItemViewModel> MemoItemClickCommand
        {
            get
            {
                if (_memoItemClickCommand != null)
                {
                    return null;
                }

                _memoItemClickCommand = new DelegateCommand<MemoItemViewModel>(vm => NavigationService.Navigate("Detail", vm.Id));
                return _memoItemClickCommand;
            }
        }

        /// <summary>
        /// メモ一覧を取得します。
        /// </summary>
        public ObservableCollection<MemoItemViewModel> Memos { get; } = new ObservableCollection<MemoItemViewModel>();

        #endregion

        #region Constructors

        public ListPageViewModel(INavigationService navigationService, IMemoRepository repository)
            : base(navigationService)
        {
            _repository = repository;
            PageTitle = "Memo";
        }

        #endregion

        #region Methods

        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);

            // 状態の保存
            viewModelState["text"] = Text;
        }

        public override async void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            // 状態の復元
            if (viewModelState != null && viewModelState.ContainsKey("text"))
            {
                Text = viewModelState["text"] as string;
            }

            // 表示更新
            await UpdateListAsync();
        }

        /// <summary>
        /// メモを追加します。
        /// </summary>
        /// <param name="text">メモの本文</param>
        private async Task AddMemoAsync(string text)
        {
            var memo = new MemoItem { Text = text };
            await _repository.AddAsync(memo);
        }

        /// <summary>
        /// 表示しているメモリストを更新します。
        /// </summary>
        private async Task UpdateListAsync()
        {
            // メモをDBから逆順で取得
            var memos = from memo in await _repository.GetAllAsync()
                        where memo.Id != null
                        orderby memo.Id.Value descending
                        select new MemoItemViewModel
                        {
                            Id = memo.Id.Value,
                            Text = memo.Text
                        };

            // 空にしてから全件投入
            Memos.Clear();

            foreach (var memo in memos)
            {
                Memos.Add(memo);
            }
        }

        #endregion
    }

    /// <summary>
    /// リストに表示するメモのViewModel
    /// </summary>
    public class MemoItemViewModel : ViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}
