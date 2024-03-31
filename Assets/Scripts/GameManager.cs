using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerController Player;
    public ShipController SteeringWheel;
    public ShipCore ShipCore;
    public DelieveryManager delievery;

    public static GameManager instance;

    public GameState state;

    private float breathingTimer;
    private float timeToDie = 60;
    public enum GameState
    {
        Intro,
        Main,
        Kill
    }
    public enum PlayerState
    {
        Walking,
        Cutscene,
        Piloting,
        Extra
    };
    public enum ShipState
    {

    };
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (instance.ShipCore.currentFuel < 0)
        {
            state = GameState.Kill;
        }

        if (state == GameState.Kill)
        {
            breathingTimer += Time.deltaTime;
            Debug.Log(breathingTimer);
            if (breathingTimer > 20)
            {
                instance.Player.PlayBreathing();
            }
        }
    }
}

