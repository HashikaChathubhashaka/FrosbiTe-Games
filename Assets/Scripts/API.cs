using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class API : MonoBehaviour

{

    // public TextMeshProUGUI nic;
    public Text FirstNameText;
    public Text LastNameText;
    public Text UserNameText;

    private string JWTtoken; // Store JWT token




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Authenticate());
    }
    

    // 2. Authenticate Player - JWT Token
    IEnumerator Authenticate()
    {
        string url = "http://20.15.114.131:8080/api/login";  // End point for Authenticate Player
        string jsonData = "{\"apiKey\": \"NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGI3OjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhZA\"}";

        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest req = new UnityWebRequest(url, "POST")) // Using POST Method
        {
            req.uploadHandler = new UploadHandlerRaw(postData);
            req.SetRequestHeader("Content-Type", "application/json");
            req.downloadHandler = new DownloadHandlerBuffer();

            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(req.error);
            }
            else
            {
                var token_obj = JsonUtility.FromJson<AuthenticatePlayer>(req.downloadHandler.text); // Convert To JSON into object
                JWTtoken = token_obj.token;
                Debug.Log(req.downloadHandler.text); // output the Response JSON
                Debug.Log(token_obj.token); // output the JWT token

                StartCoroutine(ViewProfile());
            }
        }
    }



    // 3. View Player Profile
    IEnumerator ViewProfile()
    {
        string url = "http://20.15.114.131:8080/api/user/profile/view";  // End point for View Player Profile

        using (UnityWebRequest req = new UnityWebRequest(url, "GET"))
        {
            req.SetRequestHeader("Authorization", "Bearer " + JWTtoken);
            req.downloadHandler = new DownloadHandlerBuffer();
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(req.error);
            }
            else
            {                
                string profileJson = req.downloadHandler.text;
                Debug.Log("Player Profile: " + profileJson);

                var player_obj =  UserProfile.CreateFromJSON(profileJson);  
                Debug.Log(player_obj.user.firstname);

                FirstNameText.text = "First Name: " + player_obj.user.firstname ; 
                LastNameText.text = "Last Name: " + player_obj.user.lastname;
                UserNameText.text = "User Name: " + player_obj.user.username;

            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}



    [System.Serializable]
    public class  AuthenticatePlayer
    {
        public string token;
        public static AuthenticatePlayer CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<AuthenticatePlayer>(jsonString);
        }
    }


    [System.Serializable]
    public class  UserProfile
    {
        public User user;

        public static UserProfile CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<UserProfile>(jsonString);
        }

    }

    [System.Serializable]
    public class  User
    {
        public string firstname;
        public string lastname;
        public string username;
        public string nic;
        public string phoneNumber;
        public string email;
        public string profilePictureUrl;
    }


