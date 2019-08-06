# SuperSocket
```
//// 有使用到handfire處理延遲執行，所以須利用redis做為storage
docker run -d --name redis -p 6379:6379 redis:3.2.4
```

* SuperSocket做為參考實作WebSocket Server端
* WebSocket4Net做為參考實作WebSocket Client端
* Client連線後須在身分驗證時間內(預設30秒)做驗證，控管每個連線對象