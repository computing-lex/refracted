using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFieldGeneration : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private int chanceForPlanet;
    [SerializeField] private GameObject planetoid;
    [SerializeField] private GameObject planetParent;
    [SerializeField] private Vector3 maxShift;
    public bool regenMap = false;
    [SerializeField] private int planetSpacing = 10;

    [SerializeField] private int height;
    [SerializeField] private int width;
    [SerializeField] private int[,] map;
    private List<GameObject> planets = new List<GameObject>();
    
    // Generates new planet array randomly
    void Start()
    {
        map = new int[width, height];

        GenerateMap();

    }

    void Update()
    {
        if (regenMap)
        {
            DestroyPlanets();
            GenerateMap();
            regenMap = false;
        }
    }

    void RandomFillMap()
    {
        for (int x = 0; x < width; x += planetSpacing)
        {
            for (int y = 0; y < height; y += planetSpacing)
            {
                float planetDecision = Random.value * 100;
                if (planetDecision < chanceForPlanet)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    private void GenerateMap()
    {
        RandomFillMap();

        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] == 1)
                    {
                        Vector3 randomShift = maxShift * Random.value;
                        Vector3 position = new Vector3(-width / 2 + x + randomShift.x, randomShift.y, -height / 2 + y + randomShift.z);
                        
                        GameObject newPlanetoid = Instantiate(planetoid, position, Quaternion.identity);

                        newPlanetoid.transform.SetParent(planetParent.transform, false);
                        newPlanetoid.name = "Planetoid " + x + ", " + y;
                        planets.Add(newPlanetoid);

                        // !: Call function from component
                        newPlanetoid.GetComponent<IndividualOffset>().Lol();
                    }
                }
            }
        }
    }

    private void DestroyPlanets() {
        for (int i = 0; i < planets.Count; i++)
        {
            Destroy(planets[i]);
            planets.RemoveAt(i);
        }
    }
}
