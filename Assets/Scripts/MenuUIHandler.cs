using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public string playerNameText { get; set; }
    public void Play()
    {
        SceneManager.LoadScene("main");
    }
    public void InsertPlayerName()
    {
        GameManager.Instance.currentPlayerName = playerNameText;
    }
    public void ClearData()
    {
        GameManager.Instance.ClearData();
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else 
        Application.Exit();
#endif
    }
}
