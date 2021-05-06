using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [SerializeField]
    private AoEAttack aoeattack;
    [SerializeField]
    private Shield shield;
    [SerializeField]
    private Bar CooldownBar;
    [SerializeField]
    private Bar CooldownBar2;
    private CharStats charstats;
    private float nextUse = 0f;
    public bool aoeattackEnabled;
    public bool shieldEnabled;
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

    void Awake()
    {

        /*CooldownBar.SetMaxCooldown(1f);
        CooldownBar.SetCooldown(0f);
        CooldownBar2.SetMaxCooldown2(1f);
        CooldownBar2.SetCooldown2(0f);*/
        charstats = GetComponent<CharStats>();
    }

    void Update()
    {
        /*if (Input.GetButtonDown("Ability 1"))
        {
            if (shieldEnabled == true && shield.mana <= charstats.playerMana && nextUse < Time.time)
            {
                shield.Use();
                nextUse = Time.time + shield.cooldown;
                charstats.DepleteMana(shield.mana);
            }
        }

        if (Input.GetButtonDown("Ability 2"))
        {

        }

        Debug.Log("nextuse" + nextUse);

        if (nextUse > Time.time)
        {
            Debug.Log("cooldown" + (nextUse - Time.time));
            CooldownBar2.SetCooldown2(1 - ((nextUse - Time.time) / shield.cooldown));
        }
        else
        {
            CooldownBar2.SetCooldown2(0);
        }*/
    }

    public float SetCooldownBar(float nextUse, float currentTime, int cooldown)
    {
        if (nextUse > Time.time)
        {
            return (1 - ((nextUse - Time.time) / cooldown));
        }
        else
        {
            return 0;
        }
    }
}
