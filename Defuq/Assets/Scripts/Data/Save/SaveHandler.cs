using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Data.Save;

public class SaveHandler : MonoBehaviour
{
    public SaveFields saveFile = new SaveFields();

    [SerializeField] Transform playerTransform;
    [SerializeField] Transform enemyTransform;

    string json;

    private void Awake()
    {
        SaveManager.Initialize();

        if (SaveManager.Load() != null)
        {
            string saveString = SaveManager.Load();
            SaveFields savedObject = JsonUtility.FromJson<SaveFields>(saveString);

            playerTransform.position = savedObject.playerLocation;
        }
    }

    private void Start()
    {
        InvokeRepeating("SaveIntervals", 5, 20);
    }

    void SaveIntervals()
    {
        saveFile.playerLocation = playerTransform.position;
        saveFile.enemyLocation = enemyTransform.position;

        json = JsonUtility.ToJson(saveFile);
        SaveManager.Save(json);

        Debug.Log(SaveManager.Load());
    }

}
