using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl control;
    private bool hasSpawnedPlayer = false;

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject canvasPrefab;
    private void Awake()
    {
        if (control == null)
        {
            GameObject player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            player.name = "Player";
            GameObject canvas = Instantiate(canvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            canvas.name = "Canvas";
            hasSpawnedPlayer = true;
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
        /*if (!hasSpawnedPlayer)
        {
            GameObject player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            player.name = "Player";
            GameObject canvas = Instantiate(canvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            canvas.name = "Canvas";
        }*/
    }
}
