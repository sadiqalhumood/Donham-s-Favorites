using System;
using UnityEngine;
using System.Data;
using SQLite4Unity3d;
using System.Data.SQLite;

public class SQLiteDB : MonoBehaviour
{
    private string databaseName = "UnityProject.db";
    private SQLite4Unity3d.SQLiteConnection connection;

    public string ConnectionString
    {
        get
        {
            return string.Format("URI=file:{0}/{1}", Application.streamingAssetsPath, databaseName);
        }
    }

    void Start()
    {
        // Create a connection to the SQLite database
        string dbPath = ConnectionString;
        connection = new SQLite4Unity3d.SQLiteConnection(dbPath);
        UnityEngine.Debug.Log("Database path: " + dbPath);

        // Log the connection string to verify if it is correct
        UnityEngine.Debug.Log("Connection String: " + dbPath);

        try
        {
            // Create the database file if it doesn't exist
            if (!System.IO.File.Exists(dbPath))
            {
                System.Data.SQLite.SQLiteConnection.CreateFile(dbPath);
            }

            // Create a table in the database
            connection.CreateTable<WeatherData>();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Error creating table: " + e.Message);
        }
    }

    void OnDestroy()
    {
        // Close the connection to the database
        if (connection != null)
        {
            connection.Close();
        }
    }
}

public class WeatherData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Time { get; set; }
    public string Temperature { get; set; }
    public string Humidity { get; set; }

    public WeatherData()
    {
        // Default constructor required by SQLite4Unity3d
    }

    public WeatherData(string userName, string time, string temperature, string humidity)
    {
        UserName = userName;
        Time = time;
        Temperature = temperature;
        Humidity = humidity;
    }
}