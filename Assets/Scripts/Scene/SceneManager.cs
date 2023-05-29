using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the scene manager is a singleton unique to each scene
// it acts as a service provider for key references to e.g. the player pawn, the movement area collider(s), and any scripts that should be run on scene start
// Created by: Seph 27/5
// Last edit by: Seph 28/5

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    [field: SerializeField]public Collider2D moveArea { get; private set; } // the collider which defines the area which the player pawn can move within
    [field: SerializeField]public PlayerAdventureController playerAdventure { get; private set; } // the player controller during adventure mode
    [field: Header("The scale of actors in the foreground (bottom of screen) and background (top of screen)")]
    [SerializeField]private float scaleClose = 1f;
    [SerializeField]private float posYClose = -5f;
    [SerializeField]private float scaleFar = 0.5f;
    [SerializeField]private float posYFar = 5f;
    public bool adventureState { get; private set; } = true; // is the game currently in adventure mode (rather than battle mode)?
    public bool adventurePaused { get; private set; } = false; // is the adventure mode currently paused (to run an event)

    private void Awake()
    {
        if (instance)
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
            instance = this;
    }

    // un/pauses the adventure state (used for event sequences)
    public void SetAdventurePause(bool pauseSet)
    {
        if (adventureState)
        {
            adventurePaused = pauseSet;
        }
    }

    // sets the current adventure state (used for entering/leaving battles)
    public void SetAdventureState(bool stateSet)
    {
        adventureState = stateSet;
    }

    public float GetScaleForYPos(float ypos)
    {
        if (ypos < posYClose)
        {
            return scaleClose;
        }
        else if (ypos > posYFar)
        {
            return scaleFar;
        }
        else
        {
            float distance = (ypos - posYClose) / (posYFar - posYClose);
            return Mathf.Lerp(scaleClose, scaleFar, distance);
        }
    }
}