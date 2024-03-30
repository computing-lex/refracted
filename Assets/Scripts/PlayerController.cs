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

    [SerializeField] private float movementSpeed = 3f;
    void Start()
    {
        Look.Enable();
        Move.Enable();
        characterController = GetComponent<CharacterController>();   
    }   

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.Walking)
        {
            
            Debug.Log(Move.ReadValue<Vector2>());
            Vector3 vel = new Vector3(Move.ReadValue<Vector2>().x, 0, Move.ReadValue<Vector2>().y);

            characterController.Move(movementSpeed * vel * Time.deltaTime);
        }
        else
        {
            //do the controller shit
        }
    }

    private void LateUpdate()
    {
        float mouseX = Look.ReadValue<Vector2>().x;
        float mouseY = Look.ReadValue<Vector2>().y;

        _xRot -= (mouseY) * _ySens;
        _xRot = Mathf.Clamp(_xRot, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(_xRot, 0, 0);
        transform.Rotate(Vector3.up * (mouseX) * _xSens);
    }

    public void SetState(GameManager.PlayerState state)
    {
        playerState = state;
    }

    public PlayerState GetState()
    {
        return playerState;
    }

    public void MoveWithShip(Vector2 move)
    {
        characterController.Move(new Vector3(move.x, 0, move.y));
    }
}
