/*
 * Name: Vanessa Wang
 * Date: 3/16/23
 * Desc: Destroys bullet on impact or after a certain time
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float lifeTime = 1f; 

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        Destroy(gameObject, lifeTime);
    }

}
