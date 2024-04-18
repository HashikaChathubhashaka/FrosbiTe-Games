using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class initMenue : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField fname;
    public TMP_InputField lname;
    public TMP_InputField uname;
    public TMP_InputField nicnumber;
    public TMP_InputField pnumber;
    public TMP_InputField email;

    public Button nextbutton;

    // public TMP_B


    private string JWTtoken; // Store JWT token

    // private string JWTtokenloc = "eyJhbGciOiJSUzI1NiJ9.eyJpc3MiOiJzZWxmIiwic3ViIjoidXNlciIsImV4cCI6MTcxMzM3MDMxNCwiaWF0IjoxNzEzMzY2NzE0LCJzY29wZSI6InJlYWQgd3JpdGUifQ.DiW6e-9hqNz6tCyXgRuv4W9ji0nQddZqo7o7zRzvzVEmDV1P8wXY1GFiQQJSbuImSuVlGSjA-zkOeXyfVt7lM5yaScNB6RBTDc3JQQAQPSM_-VB8rIdhurGTHvQmCufHGhaR_IGftCzxTLI0rgTLGOec9FSx010Uk0w9ouOSjiFCHU7SH28o1kTOf1pjq0SqejEc_Ps_k2pB2YJHbKoYYW3RNldMwrFgH9pJ7Oy6Fy4iMBzadb45Jvu2qoAiECuY1QGB0tBf7DTxMz8izjlv8lb0lqgRfQgmarjo21MG351nrQ1yHA3-yOT7iFqoIoQft85X-W4PqM-u_JxerGEwtw"; // Store JWT token


    private string firstname; // Store first name
    private string lastname; // Store last name
    private string username; // Store username
    private string nic; // Store NIC number
    private string phoneNumber; // Store phone number
    private string email_; // Store email

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

                var player_obj = UserProfile.CreateFromJSON(profileJson);
                Debug.Log(player_obj.user.firstname);

                fname.text = player_obj.user.firstname;
                uname.text = player_obj.user.username;
                nicnumber.text = player_obj.user.nic;
                pnumber.text = player_obj.user.phoneNumber;

                ChangeFunction();

            }
        }
    }

    async Task addPlayerAsync(string username, string firstName, string lastName, string phoneNumber, string email, string nic)
    {
        string jsonData = $@"{{
        ""username"": ""{username}"",
        ""email"": ""{email}"",
        ""firstname"": ""{firstname}"",
        ""secondname"": ""{lastName}"",
        ""nic"": ""{nic}"",
        ""quizmark"": ""0"",
        ""quiztaken"": ""no""
    }}";

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8090/user/addplayer");
        request.Headers.Add("username", $"\"{username}\"");
        request.Headers.Add("Authorization", "Basic dXNlcjpwYXNzd29yZA==");
        // var content = new StringContent("{\"username\": $\"hiruna\",\r\n\"email\": \"alice@gmail.com\",\r\n\"firstname\": \"Alice\",\r\n\"secondname\": \"Wond\",\r\n\"nic\": \"al\",\r\n\"quizmark\": \"0\",\r\n\"quiztaken\": \"no\"}", null, "application/json");
        var content = new StringContent(jsonData, null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        // Console.WriteLine(await response.Content.ReadAsStringAsync());

    }

    async Task checkComplete(string username)
    {
        string jsonData = $@"{{
        ""username"": ""{username}""}}";
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8090/marks/checkuser");
        request.Headers.Add("Authorization", "Basic dXNlcjpwYXNzd29yZA==");
        var content = new StringContent(jsonData, null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Debug.Log(await response.Content.ReadAsStringAsync());
        if (response.Content.ToString() == "false")
        {
            Application.OpenURL("http://localhost:3000");
        }
        else
        {
            // Debug.Log("User Already Exists");
            // SceneManager.LoadScene(1);
            Application.OpenURL("http://localhost:3000");
        }

        // Console.WriteLine(await response.Content.ReadAsStringAsync());
    }

    // [Obsolete]
    // IEnumerator addPlayer(string username, string password, string lastname, string phoneNumber, string email, string nic)
    // {
    //     string url = "http://localhost:8090/user/addplayer";  // End point for View Player Profile

    //     string jsonData = $@"{{
    //     ""username"": ""{username}"",
    //     ""email"": ""{email}"",
    //     ""firstname"": ""{firstname}"",
    //     ""secondname"": ""{lastname}"",
    //     ""nic"": ""{nic}"",
    //     ""quizmark"": ""0"",
    //     ""quiztaken"": ""no""
    //     }}";

    //     var body = JsonUtility.ToJson(jsonData);

    //     using (UnityWebRequest req = UnityWebRequest.Post(url, jsonData))
    //     {
    //         req.SetRequestHeader("Authorization", "Bearer " + JWTtokenloc);
    //         req.downloadHandler = new DownloadHandlerBuffer();
    //         yield return req.SendWebRequest();

    //         if (req.result != UnityWebRequest.Result.Success)
    //         {
    //             Debug.LogError(req.error);
    //         }
    //         else
    //         {
    //             Debug.Log("Player Added: " + req.downloadHandler.text);

    //         }
    //     }
    // }

    void ChangeFunction()
    {
        nextbutton.onClick.AddListener(() =>
        {
            firstname = fname.text.ToString();
            lastname = lname.text.ToString();
            username = uname.text.ToString();
            nic = nicnumber.text.ToString();
            phoneNumber = pnumber.text.ToString();
            email_ = email.text.ToString();
            // Debug.Log("Button Clicked");
            _ = addPlayerAsync(username, firstname, lastname, phoneNumber, email_, nic);
            _ = checkComplete(username);
            // StartCoroutine(addPlayer(username, firstname, lastname, phoneNumber, email, nic));

            // Application.OpenURL("http://unity3d.com/");
        });
    }


    [System.Serializable]
    public class AuthenticatePlayer
    {
        public string token;
        public static AuthenticatePlayer CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<AuthenticatePlayer>(jsonString);
        }
    }


    [System.Serializable]
    public class UserProfile
    {
        public User user;

        public static UserProfile CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<UserProfile>(jsonString);
        }

    }

    [System.Serializable]
    public class User
    {
        public string firstname;
        public string lastname;
        public string username;
        public string nic;
        public string phoneNumber;
        public string email;
        public string profilePictureUrl;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
