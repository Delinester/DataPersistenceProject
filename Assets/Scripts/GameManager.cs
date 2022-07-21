using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string bestPlayerName { get; set; }
    public int bestPlayerScore { get; set; }
    public string currentPlayerName { get; set; }

    private string saveDataPath;
    private void Awake()
    {
        saveDataPath = Application.persistentDataPath + "/savedata.json";
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Load();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    [System.Serializable]
    public class SaveData
    {
        public string name;
        public int score;
    }
    public void Save()
    {
        SaveData savingData = new SaveData();
        savingData.name = bestPlayerName;
        savingData.score = bestPlayerScore;

        string json = JsonUtility.ToJson(savingData);
        json = EncryptDecryptData(json);
        File.WriteAllText(saveDataPath, json);        
    }
    public void Load()
    {        
        if (File.Exists(saveDataPath))
        {
            string fileData = EncryptDecryptData(File.ReadAllText(saveDataPath));
            SaveData loadData = JsonUtility.FromJson<SaveData>(fileData);
            bestPlayerName = loadData.name;
            bestPlayerScore = loadData.score;
        }
    }
    public void ClearData()
    {
        if (File.Exists(saveDataPath)) File.Delete(saveDataPath);
        bestPlayerScore = 0;
    }
    private string EncryptDecryptData(string data)
    {        
        System.Text.StringBuilder strData = new System.Text.StringBuilder(data);
        System.Text.StringBuilder outData = new System.Text.StringBuilder(data.Length);
        for (int i = 0; i < data.Length; i++)
        {
            outData.Append((char)(strData[i] ^ 145));
        }
        return outData.ToString();
    }
}
