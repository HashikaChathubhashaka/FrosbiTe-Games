using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;

public class leaderBoard : MonoBehaviour
{
    public TMP_Text fp_name;
    public TMP_Text fp_score;

    public TMP_Text sp_name;
    public TMP_Text sp_score;

    public TMP_Text tp_name;
    public TMP_Text tp_score;
    // URL and authorization information
    private string url = "http://localhost:8090/score/leaderboard";
    private string authorizationHeader = "Basic dXNlcjpwYXNzd29yZA==";

    // // Variables to store the extracted data
    private string username1, username2, username3;
    private int score1, score2, score3;

    // Start is called before the first frame update
    void Start()
    {
        // Call the FetchLeaderboardData method
        StartCoroutine(FetchLeaderboardData());
    }

    // Coroutine to handle the async task
    private IEnumerator FetchLeaderboardData()
    {
        Task<string> fetchTask = FetchDataAsync();
        yield return new WaitUntil(() => fetchTask.IsCompleted);

        if (fetchTask.Exception != null)
        {
            Debug.LogError($"Error fetching data: {fetchTask.Exception}");
        }
        else
        {
            Debug.Log(fetchTask.Result);
            ParseJson(fetchTask.Result);
        }
    }

    // Async method to fetch data
    private async Task<string> FetchDataAsync()
    {
        using (var client = new HttpClient())
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", authorizationHeader);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }

    // Class to represent the JSON structure
    [System.Serializable]
    public class LeaderboardEntry
    {
        public string username;
        public int score;
    }

    // Method to parse the JSON and extract the data
    private void ParseJson(string json)
    {
        // Deserialize the JSON into a list of LeaderboardEntry
        List<LeaderboardEntry> leaderboard = JsonConvert.DeserializeObject<List<LeaderboardEntry>>(json);

        if (leaderboard != null && leaderboard.Count >= 3)
        {
            // Extract the data into separate variables
            username1 = leaderboard[0].username;
            score1 = leaderboard[0].score;
            username2 = leaderboard[1].username;
            score2 = leaderboard[1].score;
            username3 = leaderboard[2].username;
            score3 = leaderboard[2].score;

            // Log the extracted data to the console
            Debug.Log($"Username1: {username1}, Score1: {score1}");
            Debug.Log($"Username2: {username2}, Score2: {score2}");
            Debug.Log($"Username3: {username3}, Score3: {score3}");

            fp_name.text = username1;
            fp_score.text = score1.ToString();
            sp_name.text = username2;
            sp_score.text = score2.ToString();
            tp_name.text = username3;
            tp_score.text = score3.ToString();
        }
        else
        {
            Debug.LogError("Insufficient data in the leaderboard.");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
