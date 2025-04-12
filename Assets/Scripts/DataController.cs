using System;
using System.IO;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataController : MonoBehaviour
{
    public static DataController Instance;
    public String playerName;
    public String bestPlayerName;
    public int bestPlayerScore;
    public TMP_Text BestScore;
    public TMP_Text ByBestPlayer;
    public TMP_Text PlayerName;
    public Text MainBestScoreAndName;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        } 
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject bestScoreObj = GameObject.Find("BestScore");
        GameObject byBestPlayerObj = GameObject.Find("ByBestPlayer");
        GameObject playerNameObj = GameObject.Find("CurrentPlayer");
        GameObject MBestScoreAndName = GameObject.Find("MainBestScoreAndName");

        if (bestScoreObj != null)
            BestScore = bestScoreObj.GetComponent<TMP_Text>();
        if (byBestPlayerObj != null)
            ByBestPlayer = byBestPlayerObj.GetComponent<TMP_Text>();
        if (playerNameObj != null)
            PlayerName = playerNameObj.GetComponent<TMP_Text>();
        if (MBestScoreAndName != null)
            MainBestScoreAndName = MainBestScoreAndName = MBestScoreAndName.GetComponent<Text>();
        
        LoadBestPlayerAndScore();
        UpdatePlayerName();
        UpdateBestPlayerNameAndScore();
        UpdateMainBestScoreAndName();
    }

    public void UpdateMainBestScoreAndName()
    {
    if (MainBestScoreAndName != null)
        MainBestScoreAndName.text = $"Best Score: {bestPlayerScore} Name: {bestPlayerName}";
    }

    public void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void UpdatePlayerName()
    {
        if (PlayerName != null)
            PlayerName.text = $"Current Player: {playerName}";
    }

    public void UpdateBestPlayerNameAndScore()
    {
        if (BestScore != null)
            BestScore.text = $"Best Score: {bestPlayerScore}";

        if (ByBestPlayer != null)
            ByBestPlayer.text = $"By: {bestPlayerName}";
    }

    public void UpdateBestPlayerNameAndScore(int newPlayerScore)
    {
        if(bestPlayerScore < newPlayerScore)
        {
            bestPlayerName = playerName;
            bestPlayerScore = newPlayerScore;
            UpdateBestPlayerNameAndScore();
            SaveBestPlayerAndScore();
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestPlayerScore;
    }

    public void SaveBestPlayerAndScore()
    {
        SaveData data = new SaveData();
        data.bestPlayerName = bestPlayerName;
        data.bestPlayerScore = bestPlayerScore;

        string json = JsonUtility.ToJson(data);
    
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestPlayerAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.bestPlayerName;
            bestPlayerScore = data.bestPlayerScore;
        }
    }

}