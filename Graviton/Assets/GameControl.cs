using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl control;
    //private bool hasSpawnedPlayer = false;

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject canvasPrefab;

    private GameObject player;

    private GameObject canvas;
    private void Awake()
    {
        if (control == null)
        {
            player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            player.name = "Player";
            canvas = Instantiate(canvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            canvas.name = "Canvas";
            //hasSpawnedPlayer = true;
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

       

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /*if (GameObject.Find("Player") == null)
        {
            GameObject player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            player.name = "Player";
            GameObject canvas = Instantiate(canvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            canvas.name = "Canvas";
        }*/


        switch (scene.name)
        {
            case "HUB level": if (player != null) player.transform.position = new Vector3(0, 0, 0); break;
        }
    }
}
