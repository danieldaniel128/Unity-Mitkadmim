using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Data.Save;

public class SaveHandler : MonoBehaviour
{
    public SaveFields saveFile = new SaveFields();

    [SerializeField] GameObject current_PlayerGameObject;
    [SerializeField] List<GameObject> enemiesList;

    string json;

    private void Awake()
    {
        SaveManager.Initialize();
    }

    private void Start()
    {
        InvokeRepeating("SaveTrigger", 5, 20);
    }

    public void SaveTrigger()
    {
        // saves player gameobject
        saveFile.playerGO = current_PlayerGameObject;
        // saves enemy list
        saveFile.enemies = enemiesList;


        json = JsonUtility.ToJson(saveFile);
        SaveManager.Save(json);

        Debug.Log(SaveManager.Load());
    }

    public void LoadGame()
    {
        if (SaveManager.Load() != null)
        {
            string saveString = SaveManager.Load();
            SaveFields savedObject = JsonUtility.FromJson<SaveFields>(saveString);

            current_PlayerGameObject = savedObject.playerGO;
        }
    }
}
