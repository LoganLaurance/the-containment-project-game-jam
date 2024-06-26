/*
 * Name: Vanessa Wang
 * Date: 3/15/23
 * Desc: All things pertaining to the player (movement, stats)
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public GameObject player;
    public float runspeed = 5.0f;
    public float speedCap = 15f;
    public float playerHealth = 20;
    [HideInInspector]
    public float maxPlayerHealth;
    public Camera cam;
    Rigidbody2D rb;

    [HideInInspector]
    public float horizontal;
    [HideInInspector]
    public float vertical;

    private Animator animator;

    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        maxPlayerHealth = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //getting the axis so directions can be controlled via wasd or arrow keys
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if(horizontal != 0.0f)
        {
            animator.SetFloat("isMoving", Mathf.Abs(horizontal));
        }
        else if(vertical != 0.0f)
        {
            animator.SetFloat("isMoving", Mathf.Abs(vertical));
        }
        else
        {
            animator.SetFloat("isMoving", 0.0f);
        }
    }

    private void FixedUpdate()
    {
        //increasing the velocity of going in the direction of a vector at the set speed
        rb.velocity = new Vector2(horizontal * runspeed, vertical * runspeed);

        //creating a vector from the player position to the mouse position
        Vector2 lookDir = mousePos - rb.position;
        //getting an angle that is just tangent so y/x of the vector (x, y)
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("DeathScreen");
        }

        if (runspeed > speedCap)
        {
            runspeed = speedCap;
        }

    }
}
