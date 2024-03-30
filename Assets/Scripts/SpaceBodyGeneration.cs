using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpaceBodyGeneration : MonoBehaviour
{
    //The arrangement should be 5 planets, all sizes should be 0.5-2x size of normal sphere.The spheres should reside within a 10-unit cube.
    //Implement something to stop them from being too close
    //Something like a center-point? where the further they are from eachother, the more likely it is?


    public GameObject planetoid;
    public int numberBodies = 5;
    public int planetoidRange = 10;
    public float min_bodySize = 0.5f;
    public float max_bodySize = 2.0f;
    public int socialDistance = 2;

    //These variables are just for adjustment

    /* GameObject[] newPlanetoid = new GameObject[numberBodies];

     private void Start()
     {

         for (int currentPlanetoid = 0; currentPlanetoid < numberBodies; currentPlanetoid++)
         {
             Debug.Log("Loop: " + currentPlanetoid);
             var position = new Vector3(Random.Range(-planetoidRange, planetoidRange), 0, Random.Range(-planetoidRange, planetoidRange));
             newPlanetoid[currentPlanetoid] = Instantiate(planetoid, position, Quaternion.identity);


                 while (Vector3.Distance(newPlanetoid[currentPlanetoid].transform.position, newPlanetoid[createdPlanetoid].transform.position) < socialDistance)
                 {
                     Debug.Log("Changing postitions");
                     var changedposition = new Vector3(Random.Range(-planetoidRange, planetoidRange), 0, Random.Range(-planetoidRange, planetoidRange));
                     newPlanetoid[currentPlanetoid].transform.position = changedposition;
                 }

             //I need to reference the new planetoid to change its size sooo
             newPlanetoid[currentPlanetoid].transform.localScale = Vector3.one * ((Random.value * (max_bodySize - min_bodySize)) + min_bodySize);
             Debug.Log("Subject " + currentPlanetoid + " Has passed testing");
         }
     }

     public ObjectCheckArray(GameObject objectA, GameObject[] array)
     {
         bool[] bools = new bool[numberBodies];
         for (int index = 0; index < numberBodies; index++)
         {
             bools[index] = Vector3.Distance(objectA.transform.position, array[index].transform.position) < socialDistance;
         }
         if 
     }*/

    /*public int width;
    public int height;

    int[,] map;
    void OnDrawGizmos()
    {
        //if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //Gizmos.color = (map[x, y] == 1) ? Color.Black : Color.white;
                    Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }*/

}