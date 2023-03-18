
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public GameObject player;
    public float runspeed = 5.0f;
    public float playerHealth = 20;
    [HideInInspector]
    public float maxPlayerHealth;
    public Camera cam;
    Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    //private bool dead = false;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        maxPlayerHealth = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //getting the axis so directions can be controlled via wasd or arrow keys
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //turning the mouse position from pixel values to actual coordinates i think
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //increasing the velocity of going in the direction of a vector at the set speed
        rb.velocity = new Vector2(horizontal * runspeed, vertical * runspeed);

        //creating a vector from the player position to the mouse position
        Vector2 lookDir = mousePos - rb.position;
        //getting an angle that is just tangent so y/x of the vector (x, y)
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
