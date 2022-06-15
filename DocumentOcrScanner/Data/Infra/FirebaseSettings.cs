using System.Text.Json.Serialization;

namespace DocumentOcrScanner.Data.Infra;

public class FirebaseSettings
{
    [JsonPropertyName("project_id")]
    public string ProjectId => "assisti-tv-globinho";


    [JsonPropertyName("private_key_id")]
    public string PrivateKeyId => "AIzaSyBA7Q7kVwPxu89Ch_ytauh934mk1QeBX2s";

    //[JsonPropertyName("authDomain")]
    //public string AuthDomain => "assisti-tv-globinho.firebaseapp.com";



    //[JsonPropertyName("storageBucket")]
    //public string StorageBucket => "assisti-tv-globinho.appspot.com";

    //[JsonPropertyName("messagingSenderId")]
    //public string MessagingSenderId => "379871614208";

    //[JsonPropertyName("appId")]
    //public string AppId => "1:379871614208:web:6ffd5e4b792e57a1a0cfe5";

}
