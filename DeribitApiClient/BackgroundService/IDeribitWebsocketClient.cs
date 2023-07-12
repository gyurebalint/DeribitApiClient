namespace DeribitApiClient.BackgroundService
{
    public interface IDeribitWebsocketClient: IDisposable
    {
        //This being a POC I think I wil just create a websocket that auth, subscribe to channels then receives messages 
        //in a using{} scope so the Dispose method is automatic. We ll see if I have time creating a normal api like:
        //Authenticate(), SendMessage(), Subscribe(), ReceiveMessages() methods.

        Task RunStreamAsync();
    }
}
