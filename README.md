# DeribitApiWebSocketClient

## Task broadly speaking 
Create a console application
Create a long running service
Connect to websocket server
Let the stream come until Ctr+C
Shut down service gracefully

Would like to not use third party library, but create my own requests and responses.

## Comments for Tamas
The DeribitApiClient only have one method `Task RunStreamAsync()`, if I had more time, I would have created several methods such as: `Authenticate(), SendMessage(), Subscribe(), ReceiveMessages()` and would have used one injected ClientWebsocket -> readonly _clientWebsocket instance that way the dispose would actually contain something. I wouldn't have used the `using{}` scope then.

When the `host.StopAsync().GetAwaiter().GetResult();` is called in the `Program.cs` in the `ShutDownGracefully` method the _client.Dispose would have disposed of the websocketclient object.

I could have used enums to put together pieces of the requests.
I did no tests, I usually use XUnit, and Moq.
