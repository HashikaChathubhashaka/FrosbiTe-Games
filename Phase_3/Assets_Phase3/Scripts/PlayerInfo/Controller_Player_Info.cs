using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Player_Info : MonoBehaviour
{
    private int character_id = 0;
    private string done_mcq = "true";

    private string u_name = initMenue.username;

    public void OnPressNextButton()
    {
        if (character_id == 0){
            SceneManager.LoadSceneAsync(1);
        } 
        else{
            if (done_mcq == "true"){
                SceneManager.LoadSceneAsync(3);
            }
            else{
                Application.OpenURL("http://localhost:3000");
                SceneManager.LoadSceneAsync(2);
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _ = checkMCQ(u_name);
    }


    async Task checkMCQ(string username)
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
        done_mcq = response.Content.ToString();

        // Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
