using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipController : MonoBehaviour, IInteractable
{

    [SerializeField] private Transform standingPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   

    public void StartPiloting()
    {
        GameManager.instance.Player.SetState(GameManager.PlayerState.Piloting);
        Debug.Log("Piloting now :3");
    }

    public void StopPiloting()
    {
        GameManager.instance.Player.SetState(GameManager.PlayerState.Walking);

    }

    public void Interact()
    {
        if (GameManager.instance.Player.GetState() == GameManager.PlayerState.Walking)
        {
            StartPiloting();
        }
        else
        {
            StopPiloting();
        }
    }

    public Vector3 GetStandingPos()
    {
        return standingPos.position;
    }
}
