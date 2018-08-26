# Bottom Up DDD
このプロジェクトはドメイン駆動設計のパターンを利用して作った最小限のアプリケーションです。  
機能はユーザの一覧管理機能のみです。

解説記事は以下サイトです。  
https://nrslib.com/bottomup-ddd/

# データベースを動作させて動かしたい場合
localhost に mysql を建てます。

mysql で以下のデータベースとテーブルを作成します。
データベース名: bottomup_ddd
テーブル生成
```
CREATE TABLE t_user(
  id VARCHAR(255),
  username VARCHAR(255),
  firstname VARCHAR(255),
  familyname VARCHAR(255)
);
```

Startup.cs で InMemoryUserRepository を DI 設定しているところをコメントアウトし、UserRepository を DI 設定するようにコメントを外します。
```
// Test
services.AddSingleton<IUserRepository, InMemoryUserRepository>();
// Product
// services.AddSingleton<IUserRepository, UserRepository>();
```
↓
```
// Test
// services.AddSingleton<IUserRepository, InMemoryUserRepository>();
// Product
services.AddSingleton<IUserRepository, UserRepository>();
```

以上で実行できるはずです。

## 接続文字列を変更したい場合
接続文字列は Config.ConnectionString を利用しているので必要であれば適宜変更してください。
