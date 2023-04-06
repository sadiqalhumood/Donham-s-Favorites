using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class WeatherInfo
{
    public List<WeatherDay> days;
}

[System.Serializable]
public class WeatherDay
{
    public float temp;
    public string conditions;
    public string icon;
    public List<Hour> hours; 
}

[System.Serializable]
public class WeatherConditions
{
    public string icon;
}

[System.Serializable]
public class Hour 
{
    public string conditions;

}

public class WeatherAPI : MonoBehaviour
{
    public string apiKey;
    public string location = "Miami,FL";
    public string apiUrl = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/";
    public GameObject rainSystem; // Assign rain system object in the Inspector
    public int actualHour;
    private string url;

    void Start()
    {
        url = apiUrl + location + "?key=" + apiKey;
        StartCoroutine(GetWeather());
    }

    IEnumerator GetWeather()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResult = webRequest.downloadHandler.text;
                //Parse the jsonResult and use the weather data in your game
                WeatherInfo weatherInfo = JsonUtility.FromJson<WeatherInfo>(jsonResult);
                Debug.Log("Temperature: " + weatherInfo.days[0].temp);
                Debug.Log("Conditions: " + weatherInfo.days[0].hours[actualHour].conditions);

                if (weatherInfo.days[0].hours[actualHour].conditions.ToLower().Contains("rain"))
                {
                    rainSystem.SetActive(true);
                    Debug.Log("It is raining in the city that you chose!");
                }
                else
                {
                    rainSystem.SetActive(false);
                    Debug.Log("It is not raining in the city that you chose!");
                }
            }
        }
    }
}

