using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{

    public int iNewScene;
    public string sNewScene;

    public bool ToLoadScene = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.name == "Player")
        {

            LoadScene();
        }

    }
    void LoadScene()
    {
        if (ToLoadScene)
        {
            Debug.Log("iNewScene");
            SceneManager.LoadScene(iNewScene);



            // SceneManager.MoveGameObjectToScene(GameObject.Find("Player"), SceneManager.GetSceneByBuildIndex(iNewScene));


        }
        else
        {
            Debug.Log("sNewScene");
            SceneManager.LoadScene(sNewScene);

            // SceneManager.MoveGameObjectToScene(GameObject.Find("Player"), SceneManager.GetSceneByName("sNewScene"));


        }
    }
}
