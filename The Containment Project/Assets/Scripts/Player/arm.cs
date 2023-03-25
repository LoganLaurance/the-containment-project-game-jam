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
        //creating a vector from the player position to the mouse position
        Vector2 lookDir = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);

        float temp = Mathf.Abs(transform.localPosition.x);
        if (playerMov.gameObject.transform.position.x < mousePos.x)
        {
            transform.localPosition = new Vector3(temp, transform.localPosition.y, transform.localPosition.x);
            playerMov.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(playerMov.gameObject.transform.position.x > mousePos.x)
        {
            transform.localPosition = new Vector3(temp * -1f, transform.localPosition.y, transform.localPosition.x);
            playerMov.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}

