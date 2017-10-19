using App2.Config;
using App2.Data;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App2.Repository
{
    /// <summary>
    /// メモを保存するためのレポジトリを提供するインターフェース
    /// </summary>
    public interface IMemoRepository
    {
        /// <summary>
        /// 保存されているメモを全件取得します。
        /// </summary>
        Task<List<MemoItem>> GetAllAsync();

        /// <summary>
        /// 保存されているメモを一件取得します。
        /// </summary>
        /// <param name="id">メモのID</param>
        Task<MemoItem> GetAsync(int id);

        /// <summary>
        /// メモを追加します。
        /// </summary>
        /// <param name="item">追加するメモ</param>
        Task AddAsync(MemoItem item);

        /// <summary>
        /// 保存されているメモを削除します。
        /// </summary>
        /// <param name="id">削除するメモのID</param>
        Task DeleteAsync(int id);
    }

    public class MemoRepository : IMemoRepository
    {
        private readonly IGlobalConfig _config;
        private SQLiteAsyncConnection _database;

        /// <summary>
        /// データベース接続を取得します。
        /// </summary>
        private SQLiteAsyncConnection Database
        {
            get
            {
                lock (this)
                {
                    if (_database != null)
                    {
                        return _database;
                    }

                    // memo: ビルドターゲットをAnyCPUからx86へ変更し
                    //       参照に Windows 8.1 > 拡張 からMicrosoft Visual C++ 2013 Runtime Packageを追加する
                    _database = new SQLiteAsyncConnection(_config.DatabaseFileName);
                    _database.CreateTableAsync<MemoItem>().Wait();
                    return _database;
                }
            }
        }

        public MemoRepository(IGlobalConfig config)
        {
            _config = config;
        }

        public Task AddAsync(MemoItem item)
        {
            return Database.InsertOrReplaceAsync(item);
        }

        public Task DeleteAsync(int id)
        {
            // TODO textがどうなるか見ていない。消えてるかも、、？
            return Database.UpdateAsync(new MemoItem { Id = id, Deleted = true });
        }

        public Task<List<MemoItem>> GetAllAsync()
        {
            return Database.Table<MemoItem>()
                .Where(memo => !memo.Deleted)
                .ToListAsync();
        }

        public Task<MemoItem> GetAsync(int id)
        {
            return Database.Table<MemoItem>()
                .Where(memo => memo.Id == id && !memo.Deleted)
                .FirstOrDefaultAsync();
        }
    }
}
