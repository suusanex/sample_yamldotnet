# sample_yamldotnet
Code experimenting with several cases of serialization deserialization using YamlDotNet.

バージョンアップでクラスに変更が入った場合、古いバージョンの文字列を新しいバージョンのクラスへデシリアライズできるかどうかの実験。

YamlDotNet Ver.6.1.2 + .NET Framework 4.7.2を使用。

変数の追加：デシリアライズ成功。追加された変数は、new時のデフォルト値となった

変数の削除：デシリアライズ失敗。同名の変数が必ず存在している必要があるため、不要になった変数であっても、過去バージョンとの互換のために残しておく必要がある。プレフィックスobsolete_を付けた変数名を用意し、YamlMemberのAliasで過去バージョンの名前を指定することで、不要な変数を分かりやすく分類することはできた。

変数の削除 応用：IgnoreUnmatchedProperties()を使用する事で、削除された（存在しない）値を無視する動作にすることができた。互換が取りやすくなる。ドキュメントは無いが、ソースコードおよびWebの情報から存在を確認した。

  コード上のコメント：>Determines if an exception or null should be returned if name can't be found in type

  Web情報：https://stackoverflow.com/questions/44470352/yamldotnet-need-deserializer-to-ignore-extra

リストの読み込み：読み込み対象のリストにデフォルト値がある場合、上書きされる（デフォルト値は消える）。直観通りの動作。（ちなみにJSON.NETの場合、デフォルト値に加えて読み込んだ値が追加されるという動きになる）

