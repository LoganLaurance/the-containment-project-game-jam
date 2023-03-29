/*
 * Name: Vanessa Wang
 * Date: 3/15/23
 * Desc: Makes an enemy chase the player
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    private GameObject player =  null;
    private GameObject bullet;
    public float speed;
    public float enemyHealth;
    public float enemyDamage;
    public float damageDelay = 1f;
    public float knockbackDelay = 1f;
    public float knockback;

    private shooter shooter;
    private playerMovement playerMov;
    private Rigidbody2D rb;

    private float distance;
    private float bulletDmg;
    private float dmgLast;

    private float knockbackLast;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        shooter = player.GetComponent<shooter>();
        playerMov = player.GetComponent<playerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            if (shooter != null)
            {
                enemyHealth = enemyHealth - shooter.bulletDamage;
                knockbackLast += Time.time;
                if (knockbackLast >= knockbackDelay)
                {
                    rb.velocity = Vector3.zero;
                    knockbackLast = Time.time;
                }
            }
        }
        if (collision.gameObject.name == "Player")
        {
            dmgLast += Time.time;
            if (playerMov != null)
            {
                if (dmgLast >= damageDelay)
                {
                    playerMov.playerHealth = playerMov.playerHealth - enemyDamage;
                    dmgLast = Time.time;
                }
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (playerMov.gameObject.transform.position.x < transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (playerMov.gameObject.transform.position.x > transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        
            
    }
}
