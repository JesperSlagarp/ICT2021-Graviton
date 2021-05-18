using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Dialog : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    public string[] startTips;
    public string[] egyptTips;
    public string[] endTips;
    //bool firstLoad = true;

    private string[] sentences;
    private int index;
    public float typingSpeed;
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject Title;

    void Start()
    {
        sentences = startTips;
        StartCoroutine(Type());
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        button.SetActive(true);
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            button.SetActive(false);
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            button.SetActive(false);
            Title.SetActive(false);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        switch (scene.name) {
            case "HUB level":
                textDisplay.text = "";
                index = 0;
                if (button == null)
                    return;
                button.SetActive(true);
                Title.SetActive(true);
                sentences = endTips;
                StartCoroutine(Type());
                break;
            case "Gen_map":
                textDisplay.text = "";
                index = 0;
                if (button == null)
                    return;
                button.SetActive(true);
                Title.SetActive(true);
                sentences = egyptTips;
                StartCoroutine(Type());
                break;
        }
    }
}
