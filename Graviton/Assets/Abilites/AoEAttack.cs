using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEAttack : MonoBehaviour
{
    private HealthBar CooldownBar;
    public int cooldown;
    public int mana;
    public int attackDistance;
    public int damage;
    private float nextUse = 0f;
    private CharStats charstats;
    // Start is called before the first frame update
    void Awake()
    {
        CooldownBar = GetComponent<HealthBar>();
        CooldownBar.SetMaxCooldown2(1f);
        CooldownBar.SetCooldown2(0f);
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

        if (nextUse > Time.time)
        {
            CooldownBar.SetCooldown2(1 - ((nextUse - Time.time) / cooldown));
        }
        else
        {
            CooldownBar.SetCooldown2(0);
        }
    }
}
