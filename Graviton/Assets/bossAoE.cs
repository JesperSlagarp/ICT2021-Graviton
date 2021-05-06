using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAoE : MonoBehaviour
{
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private float attackRadius;

    private SpriteRenderer spriteRenderer;
    private Material material;
    private GameObject player;
    private CharStats charStats;
    private bool isCasting = true;
    private Color color;
    
    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = new Vector3(attackRadius, attackRadius, 1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
        material.EnableKeyword("_EMISSION");
        color = material.GetColor("_EmissionColor");
        player = GameObject.Find("Player");
        charStats = player.GetComponent<CharStats>();

        InvokeRepeating("attack", 0, attackDelay/2);
    }

    private void attack() {

        if (isActiveAndEnabled) {
            if (!isCasting)
            {
                isCasting = true;
                flash();
                Vector3 AttackToPlayer = player.transform.position - transform.position;
                float distance = AttackToPlayer.magnitude;
                if (distance < attackRadius)
                    charStats.TakeDamage(10);
            }
            else
            {
                isCasting = false;
            }
        } 
    }


    private void flash() {
        material.SetColor("_EmissionColor", color * 100);
        Invoke("reset", 0.3f);
    }

    private void reset() {
        material.SetColor("_EmissionColor", color);
        transform.position = player.transform.position;
    }
}
