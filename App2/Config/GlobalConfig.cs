using System.IO;
using Windows.Storage;

namespace App2.Config
{
    /// <summary>
    /// アプリ設定を提供するインターフェース
    /// </summary>
    public interface IGlobalConfig
    {
        /// <summary>
        /// データを保存するファイル名を取得します。
        /// </summary>
        string DatabaseFileName { get; }
    }

    public class GlobalConfig : IGlobalConfig
    {
        public string DatabaseFileName { get; private set; }

        public GlobalConfig()
        {
            // アプリローカルフォルダのパスを取得する
            DatabaseFileName = Path.Combine(ApplicationData.Current.LocalFolder.Path, "memo.db");
        }
    }
}
