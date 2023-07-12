
namespace DeribitApiClient.BackgroundService
{
    public interface IDeribitWebsocketClient
    {
        //This being a POC I think I wil just create a websocket that auth, subscribes then receives messages 
        //in a using{} scope so the Dispose method is automatic. We ll see if I have time creating a normal api like:
        //Authenticate(), SendMEssage(), Subscribe(), ReceiveMessages() methods.
        void RunStream();
    }
    public class DeribitWebsocketClient : IDeribitWebsocketClient
    {
        public DeribitWebsocketClient()
        {
            
        }

        public void RunStream()
        {
            throw new NotImplementedException();
        }
    }
}
