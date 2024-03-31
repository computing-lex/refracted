using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LettersInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] public GameObject LettersUIRoot;
    [SerializeField] public TextMeshProUGUI MessageTMP;
    // Start is called before the first frame update
    void Start()
    {
        LettersUIRoot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        if (GameManager.instance.Player.GetState() == GameManager.PlayerState.Walking)
        {
            //GameManager.instance.delievery.GeneratePackage(GameManager.instance.planetGeneratoRandometPlanet());

            GameManager.instance.Player.SetState(GameManager.PlayerState.Extra);
            LettersUIRoot.SetActive(true);
            string content = GameManager.instance.delievery.currentDelivery.contents;
            string dest = "(" + GameManager.instance.delievery.currentDelivery.destinationX.ToString() + ", " + GameManager.instance.delievery.currentDelivery.destinationY.ToString() + ")\n";
            string finalText = "Destination: " + dest + "\n" + content;
            MessageTMP.text = finalText;
            Debug.Log(finalText);
        }
        else
        {
            GameManager.instance.Player.SetState(GameManager.PlayerState.Walking);
            LettersUIRoot.SetActive(false);
        }
    }
}
