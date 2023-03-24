/*
 * Name: Vanessa Wang
 * Date: 3/22/23
 * Desc: Rotates arm towards where the mouse is at.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm : MonoBehaviour
{
    public GameObject armPrefab;
    public GameObject player;

    private Rigidbody2D rb;
    Vector2 mousePos;
    public Camera cam;

    private playerMovement playerMov;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerMov = player.GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(playerMov.horizontal * playerMov.runspeed, playerMov.vertical * playerMov.runspeed);
        //creating a vector from the player position to the mouse position
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }
}

