using System;
using System.IO;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    private TutorialData tutData;

    private string path = "";
    private string persistentPath = "";

    private int tutNpcIndex;
    private int loreIndex;
    public TutNPCManager tutMan;
    public bool tutComplete = false;
    public GameObject player;
    GameObject tutSpawnLocation;
    private Vector3 playerV;
    private Vector3 tutLocationV;
    
    


    private void Awake()
    {
       
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        tutMan = GameObject.FindWithTag("TutNPC").GetComponent<TutNPCManager>();

    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the sceneLoaded event
    }

//Load Data if on prison scene    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
        {
            LoadData();
            tutMan = GameObject.FindWithTag("TutNPC").GetComponent<TutNPCManager>();
            tutMan.tutComplete = tutComplete;
            tutMan.TutNPCIndex = tutNpcIndex;
            tutSpawnLocation = GameObject.FindWithTag("Respawn");
            tutLocationV = tutSpawnLocation.transform.position;
            player.transform.position = tutLocationV;


        }
    }

    public void CreateTutorialData()
    {
        tutData = new TutorialData(tutMan.TutNPCIndex, 0, true, tutMan.tutComplete);
    }

    public void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "TutorialData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "TutorialData.json";
    }

    public void SaveData()
    {
        string savePath = path;
        //Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(tutData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write((json));
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        TutorialData data = JsonUtility.FromJson<TutorialData>(json);
        Debug.Log("Loaded Tutorial Data: " + data);

        tutComplete = data.tutComplete;
        tutNpcIndex = data.tutNpcIndex;
    }
}
