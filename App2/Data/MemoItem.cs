using SQLite;

namespace App2.Data
{
    [Table("Memo")]
    public class MemoItem
    {
        /// <summary>
        /// IDを取得または設定します。
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }

        /// <summary>
        /// メモテキストを取得または設定します。
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 削除フラグを取得または設定します。
        /// </summary>
        public bool Deleted { get; set; }
    }
}
