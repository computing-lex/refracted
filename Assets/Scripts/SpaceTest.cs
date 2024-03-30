using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTest : MonoBehaviour
{
    [Range(0, 100)]
    public int chanceForPlanet;
    public GameObject planetoid;

    public int height;
    public int width;
    int[,] map;

    void Start()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }


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
                    map[x, y] = (Random.value * 100 == chanceForPlanet) ? 1 : 0; 
            }
        }
    }

    int GetSurroundingPlanetCount(int gridX, int gridY)
    {
        int bodyCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        bodyCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    bodyCount++;
                }
            }
        }

        return bodyCount;
    }

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbouringPlanets = GetSurroundingPlanetCount(x, y);

                if (neighbouringPlanets > 1)
                    map[x, y] = 0;
                else if (neighbouringPlanets < 1)
                    map[x, y] = 1;

            }
        }
    }
}
