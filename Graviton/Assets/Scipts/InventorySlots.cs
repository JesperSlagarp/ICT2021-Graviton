using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public bool[] isFull;
    public Image[] slots;
    public GameObject inventory;
    void Awake()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = inventory.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();
        }
    }
}
