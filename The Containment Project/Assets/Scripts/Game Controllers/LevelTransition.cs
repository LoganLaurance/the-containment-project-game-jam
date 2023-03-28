//------------------------------------------------------
//
//  File: LevelTransition.cs
//  By: Logan Laurance
//  Last Edited: 3.21.2023
//  Description: Transition to the desired level.
//
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [Tooltip("Put scene name to transition to here. Case-sensitive!")]public string levelDestination;
    [Tooltip("Put the open door sprite here. Will be different depending on the position of this object.")] public Sprite openState;

    private void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            EnableTriggers();
        }
    }

    public void EnableTriggers()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = openState;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7) // If player
        {
            if(levelDestination != "Lobby") // If going to next level, then preserve temporary upgrades.
            {
                GameManager.Instance.changedLevels = true;
            }
            else
            {
                GameManager.Instance.loseTempStats = true;
            }
            GameManager.Instance.ClearPowerUps();
            SceneManager.LoadScene(levelDestination);
        }
    }
}
