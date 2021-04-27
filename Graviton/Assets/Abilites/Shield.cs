using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int cooldown;
    public int mana;
    public int duration;
    private float nextUse = 0f;
    private float stop = 0f;
    private CharStats charstats;
    // Start is called before the first frame update
    void Start()
    {
        charstats = GetComponent<CharStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Ability 1") && nextUse < Time.time)
        {
            nextUse = Time.time + cooldown;
            stop = Time.time + duration;
            charstats.DepleteMana(mana);
            charstats.shield = true;
        }

        if (stop < Time.time && charstats.shield == true)
        {
            charstats.shield = false;
        }
    }
}
