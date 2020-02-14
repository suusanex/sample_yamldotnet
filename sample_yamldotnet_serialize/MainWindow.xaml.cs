using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YamlDotNet.Serialization;

namespace sample_yamldotnet
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnBtnTest1(object sender, RoutedEventArgs e)
        {
            //バージョンアップでクラスに変更が入った場合、古いバージョンの文字列を新しいバージョンのクラスへデシリアライズできるかどうかの実験。
            //YamlDotNet Ver.6.1.2 + .NET Framework 4.7.2を使用。
            //変数の追加：デシリアライズ成功。追加された変数は、new時のデフォルト値となった
            //変数の削除：デシリアライズ失敗。同名の変数が必ず存在している必要があるため、不要になった変数であっても、過去バージョンとの互換のために残しておく必要がある。プレフィックスobsolete_を付けた変数名を用意し、YamlMemberのAliasで過去バージョンの名前を指定することで、不要な変数を分かりやすく分類することはできた。
            //変数の削除 応用：IgnoreUnmatchedProperties()を使用する事で、削除された（存在しない）値を無視する動作にすることができた。互換が取りやすくなる。ドキュメントは無いが、ソースコードおよびWebの情報から存在を確認した。
            //  コード上のコメント：>Determines if an exception or null should be returned if name can't be found in type
            //  Web情報：https://stackoverflow.com/questions/44470352/yamldotnet-need-deserializer-to-ignore-extra
            // リストの読み込み：読み込み対象のリストにデフォルト値がある場合、上書きされる（デフォルト値は消える）。直観通りの動作。（ちなみにJSON.NETの場合、デフォルト値に加えて読み込んだ値が追加されるという動きになる）

            var v1 = new Datav1
            {
                child = new Datav1Child()
                {
                    dataChild1 = 2
                },
                data1 = 3,
                data2 = new List<string> { "1", "2" }
            };

            var serializer = new YamlDotNet.Serialization.Serializer();

            var v1str = serializer.Serialize(v1);

            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            

            var v2 = deserializer.Deserialize<Datav2>(v1str);

            MessageBox.Show($"{nameof(v2.data3)}={v2.data3},{nameof(v2.child2.dataChild1)}={v2.child2?.dataChild1},{nameof(v2.data2)}={string.Join(",", v2.data2)}");


        }
    }
}
