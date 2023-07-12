using System.Text.Json.Serialization;

namespace DeribitApiClient.Models
{
    public class Params
    {
        public string grant_type { get; set; } = "";
        public string client_id { get; set; } = "";
        public string client_secret { get; set; } = "";
    }
    public class AuthenticationRequest
    {
        public string jsonrpc { get; set; } = "";
        public int id { get; set; } = 0;
        public string method { get; set; } = "";
        [JsonPropertyName("params")]
        public Params _params { get; set; }= new Params();
    }
}


/*
 {
  "jsonrpc" : "2.0",
  "id" : 9929,
  "method" : "public/auth",
  "params" : {
    "grant_type" : "client_credentials",
    "client_id" : "fo7WAPRm4P",
    "client_secret" : "W0H6FJW4IRPZ1MOQ8FP6KMC5RZDUUKXS"
  }
}
 */