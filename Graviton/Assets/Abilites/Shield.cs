using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private Bar CooldownBar;
    public int cooldown;
    public int mana;
    public int duration;
    private float nextUse = 0f;
    private float stop = 0f;
    private CharStats charstats;

    void Awake()
    {
        CooldownBar.SetMaxCooldown(1f);
        CooldownBar.SetCooldown(0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        charstats = GetComponent<CharStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Ability 1") && nextUse < Time.time && mana <= charstats.playerMana)
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

        /*if (nextUse > Time.time)
        {
            CooldownBar.SetCooldown(1 - ((nextUse - Time.time)/cooldown));
        }
        else
        {
            CooldownBar.SetCooldown(0);
        }*/

        //CooldownBar.SetCooldown(charstats.SetCooldownBar(nextUse, cooldown));
    }

    public void Use()
    {

    }
}
