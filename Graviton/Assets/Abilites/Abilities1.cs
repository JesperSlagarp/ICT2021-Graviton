using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities1 : MonoBehaviour
{
    [SerializeField]
    private Bar aoeCooldownBar;
    private Bar shieldCooldownBar;

    public bool aoeReady = true;
    public bool shieldReady = true;
    public float aoeCd = 3f;
    public float shieldCd = 5f;

    private float aoeNextUse = 0f;
    private float shieldNextUse = 0f;

    public int aoeMana { get; private set; }
    public int shieldMana { get; private set; }

    public float shieldDuration { get; private set; }
    private float shieldStop = 0f;

    public int attackDistance { get; private set; }
    public int aoeDamage { get; private set; }

    private CharStats charstats;


    void Awake()
    {
        aoeCooldownBar = GameObject.Find("cooldownBar").GetComponent<Bar>();
        shieldCooldownBar = GameObject.Find("cooldownBar2").GetComponent<Bar>();

        aoeCooldownBar.SetMaxCooldown(aoeCd);
        aoeCooldownBar.SetCooldown(0f);

        shieldCooldownBar.SetMaxCooldown(shieldCd);
        shieldCooldownBar.SetCooldown(0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        charstats = GetComponent<CharStats>();
    }

    // Update is called once per frame
    void Update()
    {
        aoeMana = 20 + 5 * (charstats.aoeLevel - 1);
        shieldMana = 30 + 5 * (charstats.shieldLevel - 1);


        attackDistance = 2 + 1 * (charstats.aoeLevel - 1);
        shieldDuration = 2f + 0.5f * (charstats.shieldLevel - 1);
        

        if (Input.GetButtonDown("Ability 1") && aoeMana <= charstats.playerMana && aoeReady)
        {
            AoeAttack();
            aoeReady = false;
            aoeNextUse = aoeCd + Time.time;
        }

        if (Input.GetButtonDown("Ability 2") && shieldMana <= charstats.playerMana && shieldReady)
        {
            Shield();
            shieldReady = false;
            shieldNextUse = shieldCd + Time.time;
            shieldStop = shieldDuration + Time.time;
        }

        if(aoeNextUse < Time.time)
        {
            aoeReady = true;
            aoeCooldownBar.SetCooldown(0);
        }
        else
        {
            aoeCooldownBar.SetCooldown(aoeCd - (aoeNextUse - Time.time));
        }

        if(shieldNextUse < Time.time)
        {
            shieldReady = true;
            shieldCooldownBar.SetCooldown(0);
        }
        else
        {
            shieldCooldownBar.SetCooldown(shieldCd - (shieldNextUse  - Time.time));
        }


        if (shieldStop < Time.time && charstats.shield == true)
        {
            charstats.shield = false;
        }

    }

    void AoeAttack()
    {
        charstats.DepleteMana(shieldMana);
        aoeDamage = charstats.baseDamage;


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject target in enemies)
        {
            float distance = Vector2.Distance(target.transform.position, transform.position);
            if (distance <= attackDistance)
            {
                EnemyStats enemystat = target.GetComponent<EnemyStats>();
                enemystat.TakeDamage(aoeDamage);
            }
        }
    }
    void Shield()
    {
        charstats.DepleteMana(aoeMana);
        charstats.shield = true;
    }
}
