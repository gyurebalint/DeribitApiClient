
namespace DeribitApiClient.BackgroundService
{
    public interface IDeribitWebsocketClient
    {
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
