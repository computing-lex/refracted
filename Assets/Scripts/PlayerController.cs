using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameManager;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private InputAction Look;
    [SerializeField] private InputAction Move;

    private PlayerState playerState;
    private CharacterController characterController;
    [SerializeField] private Camera cam;

    [SerializeField] private float _xRot = 0f;
    [SerializeField] private float _xSens = .1f;
    [SerializeField] private float _ySens = .01f;

    [SerializeField] private float movementSpeed = 1.5f;


    private float clampNormal = 80f;

    private float clampA = 80f;
    private float clampB = 80f;

    private float clampPilotA = 0;
    private float clampPilotB = 50;

    private bool startBreathing = false;

    private float fuckyou = .1f;
    private float fuckup = 0;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Look.Enable();
        Move.Enable();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        fuckup += Time.deltaTime;
        if (fuckup < fuckyou)
        {
            characterController.enabled = false;
            transform.position= new Vector3(GameManager.instance.ShipCore.transform.position.x,1f, GameManager.instance.ShipCore.transform.position.z+7);
            characterController.enabled = true;
        }

        if (Vector3.Distance(transform.position, GameManager.instance.ShipCore.transform.position) > 100)
        {
            MoveTo(GameManager.instance.ShipCore.transform.position);
        }

        if (playerState == PlayerState.Walking)
        {
            //Debug.Log(Move.ReadValue<Vector2>());
            Vector3 _transformedPlayerInput = transform.TransformDirection(new Vector3(Move.ReadValue<Vector2>().x, 0, Move.ReadValue<Vector2>().y));

            Vector3 vel = new Vector3(_transformedPlayerInput.x, 0, _transformedPlayerInput.z);

            characterController.Move((movementSpeed * vel) * Time.deltaTime);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Look.Enable();
            Move.Enable();
        }
        else if (playerState == PlayerState.Piloting)
        {
            characterController.enabled = false;
            transform.position = new Vector3(GameManager.instance.SteeringWheel.GetStandingPos().x, GameManager.instance.SteeringWheel.GetStandingPos().y, GameManager.instance.SteeringWheel.GetStandingPos().z);
            characterController.enabled = true;
            //transform.LookAt(GameManager.instance.SteeringWheel.transform.position);
            //do the controller shit

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Look.Enable();
            Move.Disable();
        }
        else if (playerState == PlayerState.Extra)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Look.Disable();
            Move.Disable();
        }
    }

    private void LateUpdate()
    {
        float mouseX = Look.ReadValue<Vector2>().x;
        float mouseY = Look.ReadValue<Vector2>().y;

        _xRot -= (mouseY) * _ySens;
        _xRot = Mathf.Clamp(_xRot, -clampA, clampB);
        cam.transform.localRotation = Quaternion.Euler(_xRot, 0, 0);
        transform.Rotate(Vector3.up * (mouseX) * _xSens);
    }

    public void SetState(GameManager.PlayerState state)
    {
        playerState = state;
        if (playerState == PlayerState.Walking)
        {
            clampA = clampNormal;
            clampB = clampNormal;
        }

        if (playerState == PlayerState.Piloting)
        {
            clampA = clampPilotA;
            clampB = clampPilotB;
        }
    }

    public void SetRotation()
    {

    }
    public PlayerState GetState()
    {
        return playerState;
    }

    public void MoveWithShip(Vector3 move, Vector3 rotate)
    {
        characterController.enabled = false;
        transform.position += (new Vector3(move.x, 0, move.z));
        transform.position = new Vector3(transform.position.x, 0.083f, transform.position.z);
        //cam.transform.localRotation = Quaternion.Euler(rotate.x, 0, 0);
        //if(playerState == PlayerState.Piloting)
        //{
        transform.Rotate(rotate);


        characterController.enabled = true;
        //}

    }

    public Vector2 GetPlayerInput()
    {
        return Move.ReadValue<Vector2>();
    }

    public void PlayBreathing()
    {
        if (!startBreathing)
        {
            startBreathing = true;
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            Debug.Log("breathe");


        }
    }

    public void MoveTo(Vector3 newPosition)
    {
        characterController.enabled = false;
        // transform.position = newPosition;
        transform.position = new Vector3(newPosition.x, 0.83f, newPosition.z);
        transform.position += new Vector3(0, 0, 7);
        //do not
        characterController.enabled = true;
    }


    [SerializeField] private AudioSource deathSound;
    public void DoAJumpscare()
    {
        deathSound.PlayOneShot(deathSound.clip);
        //make image fill canvas too
    }
}