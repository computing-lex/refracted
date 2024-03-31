using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}
public class Interaction : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform interactionSource;
    public float range;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("try");
            Ray r = new Ray(interactionSource.position, interactionSource.forward);
            Debug.DrawRay(interactionSource.position,interactionSource.forward,Color.red,1);
            if(Physics.Raycast(r,out RaycastHit hitInfo, range))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
            
        }
    }
}
