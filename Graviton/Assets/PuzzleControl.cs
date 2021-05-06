using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControl : MonoBehaviour
{
    [SerializeField] private Transform[] logicalPuzzle;
    [SerializeField] private GameObject keyObject;
    public static bool youWin;

    // Start is called before the first frame update
    void Start()
    {
        keyObject.SetActive(false);
        youWin = false;
    }

    // Update is called once per frame
    // Check if angels are corect 
    void Update()
    {
        if (logicalPuzzle[0].rotation.z == 0 &&
            logicalPuzzle[1].rotation.z == 0 &&
            logicalPuzzle[2].rotation.z == 0 &&
            logicalPuzzle[3].rotation.z == 0 &&
            logicalPuzzle[4].rotation.z == 0)
        {
            youWin = true;
            keyObject.SetActive(true);
        }
    }
}