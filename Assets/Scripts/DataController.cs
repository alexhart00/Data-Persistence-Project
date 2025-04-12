using System;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public static DataController Instance;
    public String playerName;
    public String bestPlayerName;
    public int bestPlayerScore;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        } 
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
