using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFieldGeneration : MonoBehaviour
{
    [Range(0, 100)]
    public int chanceForPlanet;
    public GameObject planetoid;
    [SerializeField] private GameObject planetParent;

    public int height;
    public int width;
    int[,] map;

    void Start()
    {
        map = new int[width, height];
        RandomFillMap();


        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] == 1)
                    {
                        Vector3 position = new Vector3(-width / 2 + x + 0.5f, 0, -height / 2 + y + 0.5f);
                        GameObject newPlanetoid = Instantiate(planetoid, position, Quaternion.identity);
                        newPlanetoid.transform.SetParent(planetParent.transform, false);
                    }
                }
            }
        }
    }

    void RandomFillMap()
    {

        for (int x = 0; x < width; x += 10)
        {
            for (int y = 0; y < height; y += 10)
            {
                float planetDecision = Random.value * 100;
                if (planetDecision < chanceForPlanet)
                {
                    map[x, y] = 1;
                } else {
                    map[x, y] = 0;
                }
            }
        }
    }
}
