/*
 * Name: Vanessa Wang
 * Date: 3/15/23
 * Desc: Makes an enemy chase the player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float health;
    public float damage;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= 10)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        /*if (Collision2D.ReferenceEquals(transform.position, player.transform.position))
        {
            health -= 10;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }*/
    }
}