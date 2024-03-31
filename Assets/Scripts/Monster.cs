using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    private bool active;
    //[SerializeField] private Transform startPos;
    //[SerializeField] private Transform endPos;

    [SerializeField] private Light light;
    [SerializeField] private GameObject cone;

    [SerializeField] private float scanBackChance;

    [SerializeField] private Transform[] waypoints;
    private int currentWaypoint;


    private float monsterTimestamp;
    private float monsterTimer1;
    private float monsterTimer2;


    public enum MonsterPhase
    {
        Unaware,
        Aware,
        Approaching,
        StartScan,
        Scanning,
        ScanAgain,
        Kill,
        Leaving
    }

    [SerializeField] private MonsterPhase phase;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(phase == MonsterPhase.Aware)
        {
            monsterTimer1 += Time.deltaTime;
            if(monsterTimer1> monsterTimestamp)
            {
                phase = MonsterPhase.Approaching;
                //play approaching roar
                
            }
        }

        //hi so
        //i'm thinking there are a few ways for it to get you
        //if you ping it and then sit still, you die
        //you need to get away from the monster's range
        //it waits a few seconds before roaring and approaching
        //there's a 10% chance your ping won't affect it which just confuses you further
        //want a lot of random chance involved here

        //once you're a certain distance away we should probably implement a light switch.
        //you need to get away then cut power. 
        //once cut power, it'll scan manually
        //if power is on you die
        //avoid the scan - it scans once or twice
        //and then it leaves for a while

        //let me know if there's anything else you want to add/change for this.
        //i really don't know how this ping system is gonna work so i don't know what else to do here
        //i'll set up needed methods ig?



        if (phase == MonsterPhase.StartScan)
        {
            transform.position = waypoints[0].transform.position;
            phase = MonsterPhase.Scanning;
        }
       if(phase==MonsterPhase.Scanning)
        {
            transform.LookAt(new Vector3(0,3,2));
            if (!transform.position.Equals(waypoints[currentWaypoint].position))
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, 4*Time.deltaTime);
            }
            else
            {
                if (currentWaypoint != waypoints.Length-1)
                {
                    currentWaypoint++;
                }
                else
                {
                    phase = MonsterPhase.Unaware;
                }
            }
        }
    }

    public void SetEvilPhase(MonsterPhase state)
    {
        phase = state;
    }

    public void Pinged()
    {
        if (Random.Range(0, 10) != 1)
        {
            phase = MonsterPhase.Aware;
            monsterTimestamp = Random.Range(10, 20);

        }
    }

    public void Kill()
    {

    }
}

