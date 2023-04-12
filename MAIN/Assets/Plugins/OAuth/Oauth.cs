using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine.SceneManagement;


public class Oauth : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;

    public void OnLoginPress()
    {

        HttpClient client = new HttpClient();

        string clientId = "";
        string clientSecret = "";
        string username = this.username.text;
        string password = this.password.text;
        string userAgent = "MyBot/0.0.1";

        var authValue = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", 
        Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{clientId}:{clientSecret}")));

            client.DefaultRequestHeaders.Authorization = authValue;
            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password),
            });


        client.DefaultRequestHeaders.Add("User-Agent", userAgent);

        var response = client.PostAsync("https://www.reddit.com/api/v1/access_token", content).Result;

        var jsonContent = response.Content.ReadAsStringAsync().Result;
        Debug.Log(jsonContent);
        var accessToken = jsonContent; //maybe change this

        if (!string.IsNullOrEmpty(accessToken)) //this needs fixing
        {
        Debug.Log("Logged in with username: " + username);

        SceneManager.LoadScene("ApartmentTour");
        }
        else
        {
        Debug.Log("Authentication failed for username: " + username);
        Debug.Log(jsonContent);
        // Display an error message to the user
        }


    }
    


}
