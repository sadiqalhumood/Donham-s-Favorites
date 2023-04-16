using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI; // for Unity UI
using TMPro; // for TextMeshPro

public class Time : MonoBehaviour
{
    public string location = "Oxford, United Kingdom";
    public string apiKey;
    public TextMeshProUGUI textComponent; // for TextMeshPro

    async void Start()
    {
        var apiUrl = $"https://timezone.abstractapi.com/v1/current_time/?api_key={apiKey}&location={location}";

        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(apiUrl);
            var jsonString = await response.Content.ReadAsStringAsync();

            // Extract time and location from JSON response
            var apiResponse = JsonUtility.FromJson<ApiResponse>(jsonString);
            var dateTime = System.DateTime.Parse(apiResponse.datetime);
            var timeZone = apiResponse.timezone;
            var abbreviation = apiResponse.abbreviation;

            // Set text on 3D text object
            var textComp = textComponent.GetComponent<TextMeshProUGUI>();
            textComp.text = $"Current time in {location} is {dateTime} ({timeZone} {abbreviation})";
        }
    }

    [System.Serializable]
    public class ApiResponse
    {
        public string datetime;
        public string timezone;
        public string abbreviation;
    }
}
