using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

public class DelieveryManager : MonoBehaviour
{
    [SerializeField] public DeliveryList deliveryList;
    public TextAsset deliveryContents;

    [SerializeField] public Package currentDelivery;
    public bool hasPackage = false;
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
        public int destinationX;
        public int destinationY;
        public string contents;
        public PackageType type;

        public Package()
        {
            destinationX = 0;
            destinationY = 0;
            contents = "";
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
        hasPackage = false;
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
        Debug.Log("Package Delivered!");
        hasPackage = false;
        currentDelivery = new Package();
    }

    public void GeneratePackage(GameObject planet)
    {
        currentDelivery = new Package();
        Package del = deliveryList.deliveries[UnityEngine.Random.Range(0, deliveryList.deliveries.Length)];
        MatchCollection mc = Regex.Matches(planet.name, @"\b(\d+) (\d+)\b");
        int.TryParse(mc[0].Groups[1].Value, out currentDelivery.destinationX);
        int.TryParse(mc[0].Groups[2].Value, out currentDelivery.destinationY);
        currentDelivery.contents = del.contents;
        hasPackage = true;
    }

    public Package GetPackage() {

        return currentDelivery;
    }
}
