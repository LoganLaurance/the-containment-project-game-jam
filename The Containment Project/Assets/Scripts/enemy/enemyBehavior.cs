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
    public GameObject player;
    public float speed;
    public float enemyHealth;
    public float enemyDamage;

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


        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
