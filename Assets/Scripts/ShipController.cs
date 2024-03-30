using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPiloting()
    {
        GameManager.instance.Player.SetState(GameManager.PlayerState.Piloting);
    }

    public void StopPiloting()
    {
        GameManager.instance.Player.SetState(GameManager.PlayerState.Walking);

    }


}
