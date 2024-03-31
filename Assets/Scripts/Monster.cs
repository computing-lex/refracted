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

    [SerializeField] AudioSource roarAudioSourceFar;
    [SerializeField] AudioSource roarAudioSourceNear;

    [SerializeField] GameObject waypoint1;
    [SerializeField] GameObject waypoint2;

    [SerializeField] GameObject origin;



    [SerializeField] StupidFuckingKillzoneScript killZone;

    private AudioSource staticSource;

    private bool bludIsDead = false;


    private int currentWaypoint;


    private float monsterTimestamp;
    private float monsterTimer1;
    private float monsterTimer2;

    private Vector3 locatedPos;



    public enum MonsterPhase
    {
        Unaware,
        Pinged,
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
        staticSource = GetComponent<AudioSource>();
        staticSource.volume = 0f;
        Pinged(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {

        if (phase == MonsterPhase.Pinged)
        {
            monsterTimer1 += Time.deltaTime;
            if (monsterTimer1 > monsterTimestamp)
            {
                roarAudioSourceFar.PlayOneShot(roarAudioSourceFar.clip);
                phase = MonsterPhase.Aware;
                monsterTimer1 = 0;
                monsterTimestamp = Random.Range(10, 15);
            }

        }

        if (phase == MonsterPhase.Aware)
        {
            monsterTimer1 += Time.deltaTime;
            if (monsterTimer1 > monsterTimestamp)
            {
              
                if (Random.Range(0, 10) == 1)
                {

                    phase = MonsterPhase.Unaware;
                    monsterTimer1 = 0;
                }
                else
                {
                    roarAudioSourceNear.PlayOneShot(roarAudioSourceNear.clip);
                    phase = MonsterPhase.Approaching;
                    monsterTimer1 = 0;
                    monsterTimestamp = Random.Range(15, 25);
                }


            }


        }

        if(phase == MonsterPhase.Approaching)
        {
            monsterTimer1 += Time.deltaTime;
            if(monsterTimer1 > monsterTimestamp)
            {
                if (Vector3.Distance(locatedPos, GameManager.instance.ShipCore.transform.position) < 200)
                {
                    GameManager.instance.KillDaPlayer("MonsterSlow");
                    Debug.Log("Dead!!!");
                }

                else if (GameManager.instance.ShipCore.GetVelocity().magnitude > 5)
                {
                    monsterTimer1 = 0;
                    monsterTimestamp = Random.Range(5, 20);
                    phase = MonsterPhase.Kill;
                }

                else
                {
                    monsterTimer1 = 0;
                    phase = MonsterPhase.StartScan;
                }
                
            }

            

            if (staticSource.volume < 1) staticSource.volume += .01f*Time.deltaTime;
        }



        if(phase == MonsterPhase.Kill)
        {
            monsterTimer1 += Time.deltaTime;
            if (monsterTimer1 > monsterTimestamp)
            {
                GameManager.instance.KillDaPlayer("Jumpscare");

            }
        }

        if (phase == MonsterPhase.StartScan)
        {
            transform.LookAt(GameManager.instance.Player.transform.position);
            transform.position = waypoint1.transform.position;
            phase = MonsterPhase.Scanning;
        }

        if(phase== MonsterPhase.Scanning)
        {
            transform.LookAt(GameManager.instance.Player.transform.position);
            transform.position=Vector3.MoveTowards(transform.position,waypoint2.transform.position,20f*Time.deltaTime);
            if (transform.position.Equals(waypoint2.transform.position))
            {
                if (Random.Range(0, 4) == 2)
                {
                    phase = MonsterPhase.ScanAgain;
                }
                else
                {
                    phase = MonsterPhase.Unaware;
                }
            }

        }

        if (phase == MonsterPhase.ScanAgain)
        {
            transform.LookAt(GameManager.instance.Player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, waypoint1.transform.position, 15f * Time.deltaTime);
            if (transform.position.Equals(waypoint1.transform.position))
            {
                phase = MonsterPhase.Unaware;
                //maybe play annoyed sound
            }

        }

        if(phase==MonsterPhase.Scanning || phase == MonsterPhase.ScanAgain)
        {
            //Debug.Log("Scanning, blud is here: "+ killZone.ISBROHERE);
            if (killZone.ISBROHERE && !bludIsDead)
            {
               // Debug.Log("Dumbass Down");
                bludIsDead = true;
                monsterTimer2 = 0;
                monsterTimestamp = Random.Range(1, 3);

            }


        }

        if (bludIsDead)
        {
            monsterTimer2 += Time.deltaTime;
            if (monsterTimer2 > monsterTimestamp)
            {
                GameManager.instance.KillDaPlayer("Jumpscare");
                Debug.Log("Fuckhead");
            }
        }

        if (phase == MonsterPhase.Unaware)
        {
            monsterTimer1 = 0;
            if(staticSource.volume>0)staticSource.volume -= .01f * Time.deltaTime;
            transform.position = origin.transform.position;
        }



    }

    public void SetEvilPhase(MonsterPhase state)
    {
        phase = state;
    }

    public void Pinged(Vector3 pos)
    {
        if (Random.Range(0, 10) != 1)
        {
            phase = MonsterPhase.Pinged;
            monsterTimestamp = Random.Range(2, 5);
            monsterTimer1 = 0;
            //roarAudioSourceFar.PlayOneShot(roarAudioSourceFar.clip);
            locatedPos = pos;
        }
    }

    public void Kill()
    {

    }
}

