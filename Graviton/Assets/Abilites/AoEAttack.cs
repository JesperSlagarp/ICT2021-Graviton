using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEAttack : MonoBehaviour
{
    [SerializeField]
    private Bar CooldownBar;
    public int cooldown = 2;
    public int mana;
    public int attackDistance;
    public int damage;
    private float nextUse = 0f;
    private CharStats charstats;
    [SerializeField]
    private Abilities abilities;
    // Start is called before the first frame update
    void Awake()
    {
        
        CooldownBar.SetMaxCooldown(1f);
        CooldownBar.SetCooldown(0f);
        charstats = GetComponent<CharStats>();
    }

    void Start()
    {
        //charstats = GetComponent<CharStats>();
    }

    // Update is called once per frame
    void Update()
    {
        damage = charstats.baseDamage;
        if (Input.GetButtonDown("Ability 2") && nextUse < Time.time && mana <= charstats.playerMana)
        {
            nextUse = Time.time + cooldown;
            charstats.DepleteMana(mana);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject target in enemies)
            {
                float distance = Vector2.Distance(target.transform.position, transform.position);
                if (distance <= attackDistance)
                {
                    EnemyStats enemystat = target.GetComponent<EnemyStats>();
                    enemystat.TakeDamage(damage);
                }
            }
        }

        /*if (nextUse > Time.time)
        {
            CooldownBar.SetCooldown(1 - ((nextUse - Time.time) / cooldown));
        }
        else
        {
            CooldownBar.SetCooldown(0);
        }*/

        //float cooldownset = charstats.SetCooldownBar(nextUse, cooldown);

        CooldownBar.SetCooldown(charstats.SetCooldownBar(nextUse, cooldown));
    }

    public void Use()
    {  
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject target in enemies)
            {
                float distance = Vector2.Distance(target.transform.position, transform.position);
                if (distance <= attackDistance)
                {
                    EnemyStats enemystat = target.GetComponent<EnemyStats>();
                    enemystat.TakeDamage(damage);
                }
            }
    }
}
