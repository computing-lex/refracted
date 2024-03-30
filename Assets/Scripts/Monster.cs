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
}
