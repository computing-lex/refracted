using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerController Player;
    public ShipController SteeringWheel;
    public SpaceFieldGeneration planetGenerator;
    public ShipCore ShipCore;
    public DelieveryManager delievery;

    public static GameManager instance;

    public GameState state;

    private float breathingTimer;
    private float timeToDie = 60;

    private bool jumpscareShit = false;
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
    public IEnumerator LoadPlanets() {
        Debug.Log("Loading planets!");
        yield return new WaitUntil(() => GameManager.instance.planetGenerator.generationComplete);
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
            //Debug.Log(breathingTimer);
            if (breathingTimer > 20)
            {
                instance.Player.PlayBreathing();
            }
        }
        //TODO: add fadeout 

        //TODO: SOS (just run do a jumpscare)

        if (jumpscareShit) {
            death2 += Time.deltaTime;
            if (death2 > death1)
            {
                //load kill scene
            }
            }

    }

    private float death1;
    private float death2;
    public void KillDaPlayer(string v)
    {
        death1 = 1;
        if (v.Equals("Jumpscare"))
        {
            jumpscareShit = true;
            Player.DoAJumpscare();
        }else if (v.Equals("MonsterSlow"))
        {

        }
        
    }
}

