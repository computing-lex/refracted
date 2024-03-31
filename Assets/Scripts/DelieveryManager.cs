using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DelieveryManager : MonoBehaviour
{
    [SerializeField] public DeliveryList deliveryList;
    public TextAsset deliveryContents;

    [SerializeField] private Package currentDelivery;
    public int DeliveriesCompleted { get; private set; }

    // The delieveries that can be made, a list of packages
    [Serializable]
    public class DeliveryList
    {
        public Package[] deliveries;
    }

    // Packages are defined here
    [Serializable]
    public class Package
    {
        public int[,] destination;
        public string contents;
        public PackageType type;

        public Package()
        {
            contents = "No content defined.";
        }

        public Package(int[,] location)
        {
            contents = "No content defined.";
        }
    }
    
    public enum PackageType
    {
        Flat,
        Medium,
        Large
    };

    // Start is called before the first frame update
    void Start()
    {
        DeliveriesCompleted = 0;
        deliveryList = JsonUtility.FromJson<DeliveryList>(deliveryContents.text);
        Debug.Log("Packages parsed!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeliverPackage()
    {
        DeliveriesCompleted++;
        Debug.Log("Package delivered!");
    }

    public void GeneratePackage()
    {
        currentDelivery = new Package();
    }

    public Package GetPackage() {

        return currentDelivery;
    }
}
