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

    private shooter shooter;

    private float distance;
    private float bulletDmg;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        shooter = player.GetComponent<shooter>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            enemyHealth = enemyHealth - shooter.bulletDamage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= 10)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
