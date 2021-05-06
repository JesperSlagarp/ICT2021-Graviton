using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public bool[] isFull;
    public Image[] slots;
    private GameObject canvas;
    private GameObject inventory;
    void Awake()
    {
        canvas = GameObject.Find("Canvas");
        inventory = canvas.transform.Find("Inventory").gameObject;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = inventory.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.Find("Image").gameObject.GetComponent<Image>();
        }
    }
}
