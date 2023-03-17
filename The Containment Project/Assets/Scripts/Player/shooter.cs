/*
 * Name: Vanessa Wang
 * Date: 3/16/23
 * Desc: Shoots bullets from the player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 5f;
    public float bulletSpeed = 20f;
    public float bulletDamage = 5f;
    public float shotDelay = 0.1f;

    private float lastShot = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            lastShot += Time.deltaTime;
            if(lastShot >= shotDelay)
            {
                Shoot();
                lastShot = Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletSpeed * transform.up;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
