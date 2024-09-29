using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerDataController : MonoBehaviour
{
    public struct FishData
    {
        public string fishId;
        public int exp;
    }
    
    [NonSerialized] public static PlayerDataController instance = default!;
    
    public List<string> caughtFishesId = new List<string>();
    public List<FishData> fishDatas = new List<FishData>(); 
    public Dictionary<string, int> items = new Dictionary<string, int>();
    public List<string> completedDives = new List<string>();
    public List<string> completedBattles = new List<string>();
    
    private void Awake()
    {
        if (instance)
            return;
            
        instance = this;
        Load();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += (_, _) => Save();
    }

    public void Save()
    {
        string jsonSaveData = JsonUtility.ToJson(this);
        string filepath = Application.persistentDataPath + "/playerData.json";
        File.WriteAllText(filepath, jsonSaveData);
    }

    public void Load()
    {
        string filepath = Application.persistentDataPath + "/playerData.json";
        if (!File.Exists(filepath)) 
            return;
        
        string text = File.ReadAllText(filepath);
        PlayerDataController playerData = JsonUtility.FromJson<PlayerDataController>(text);
        caughtFishesId = playerData.caughtFishesId;
        fishDatas = playerData.fishDatas;
        items = playerData.items;
        completedDives = playerData.completedDives;
        completedBattles = playerData.completedBattles;
    }
}