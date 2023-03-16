using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //public GameObject hitEffect;
    public float lifeTime = 1f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject effect = Instantiate(hitEffect, tranform.position, Quaternion.identity);
        //Destry(effect, 5f);
        Destroy(gameObject);
    }

    void Update()
    {
        Destroy(gameObject, lifeTime);
    }

}
