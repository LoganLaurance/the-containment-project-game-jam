/*
 * Name: Vanessa Wang
 * Date: 3/22/23
 * Desc: Rotates arm towards where the mouse is at.
 *


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm : MonoBehaviour
{
    public GameObject armPrefab;
    public GameObject player;
    Vector2 mousePos;
    public Camera cam;
    private Rigidbody2D rb;
    private playerMovement playerMov;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //turning the mouse position from pixel values to actual coordinates i think
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //creating a vector from the player position to the mouse position
        Vector2 lookDir = mousePos - rb.position;
        //getting an angle that is just tangent so y/x of the vector (x, y)
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
*/
