using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIHandler : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NewPlayerName(string newPlayerName)
    {
        DataController.Instance.playerName = newPlayerName;
        DataController.Instance.UpdatePlayerName();
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit(); // original code to quit Unity player
        #endif
    }
}
