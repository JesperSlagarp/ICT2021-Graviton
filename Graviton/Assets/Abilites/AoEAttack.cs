using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEAttack : MonoBehaviour
{
    private HealthBar CooldownBar;
    public int cooldown;
    public int mana;
    public int duration;
    public int attackDistance;
    private float nextUse = 0f;
    private float stop = 0f;
    private CharStats charstats;
    // Start is called before the first frame update
    void Awake()
    {
        CooldownBar = GetComponent<HealthBar>();
        CooldownBar.SetMaxCooldown(1f);
        CooldownBar.SetCooldown(0f);
    }

    void Start()
    {
        charstats = GetComponent<CharStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Ability 2") && nextUse < Time.time)
        {
            nextUse = Time.time + cooldown;
            stop = Time.time + duration;
            charstats.DepleteMana(mana);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }
}
