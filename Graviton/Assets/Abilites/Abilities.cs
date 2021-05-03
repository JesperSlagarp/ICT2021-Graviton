using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    /*private Shield shield;
    private AoEAttack aoeattack;
    private HealthBar CooldownBar;
    public string shieldAbilitynr;
    public string aoeattackAbilitynr;

    void Awake()
    {
        CooldownBar = GetComponent<HealthBar>();
        CooldownBar.SetMaxCooldown(1f);
        CooldownBar.SetCooldown(0f);
        CooldownBar.SetMaxCooldown2(1f);
        CooldownBar.SetCooldown2(0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        shield = GetComponent<Shield>();
        aoeattack = GetComponent<AoEAttack>();
        if (shield != null)
        {
            shieldAbilitynr = "Ability 1";
        }
        else if (aoeattack != null)
        {
            shieldAbilitynr = "Ability 2";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }*/

    public void SetCooldownBar(float nextUse, float currentTime, int cooldown)
    {

    }

    public void SetCooldownBar2(float nextUse, float currentTime, int cooldown)
    {

    }
}
