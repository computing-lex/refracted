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
    public bool generationComplete = false;
    [SerializeField] private int planetSpacing = 10;

    // Map generation parameters 
    [SerializeField] private int height, width;
    [SerializeField] private int scaleX, scaleY;
    public Vector3 planetScale;
    [SerializeField] private int[,] map;

    public Material PlanetMaterial;
    public ShapeSettings shapeSettings;
    public List<GameObject> planets = new List<GameObject>();

    // Generates new planet array randomly
    void Start()
    {
        map = new int[width, height];

        StartCoroutine(GenerateMap());

    }

    void Update()
    {
        if (regenMap)
        {
            DestroyPlanets();
            StartCoroutine(GenerateMap());
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

    IEnumerator GenerateMap()
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
                        Vector2 scaledMapPos = new(x * scaleX, y * scaleY);
                        Vector3 randomShift = maxShift * Random.value;
                        randomShift = new Vector3(randomShift.x * scaleX, randomShift.y, randomShift.z * scaleY);
                        Vector3 position = new(width / 2 + scaledMapPos.x + randomShift.x, randomShift.y, height / 2 + scaledMapPos.y + randomShift.z);

                        GameObject newPlanetoid = Instantiate(planetoid, position, Quaternion.identity);

                        //newPlanetoid.transform.SetParent(planetParent.transform, true);
                        newPlanetoid.name = x + " " + y;
                        planets.Add(newPlanetoid);

                        InitializePlanet(newPlanetoid);
                        foreach (var planetPosition in newPlanetoid.GetComponentsInChildren<Transform>())
                        {
                            if (planetPosition.gameObject.name != "planetIcon")
                            {
                                planetPosition.position = new Vector3(0, 0, 0);
                            }
                            else
                            {
                                planetPosition.position = new Vector3(0, 20, 0);
                            }

                        }

                        newPlanetoid.transform.position = position;
                        newPlanetoid.transform.SetParent(planetParent.transform, true);
                        newPlanetoid.transform.localScale = planetScale;
                    }
                }
            }
        }

        generationComplete = true;

        yield return null;
    }

    private void InitializePlanet(GameObject newPlanetoid)
    {
        Planet planetComponent = newPlanetoid.AddComponent<Planet>();
        planetComponent.PlanetMaterial = PlanetMaterial;
        planetComponent.shapeSettings = shapeSettings;
        planetComponent.GeneratePlanet();
        RandomizeMaterial(newPlanetoid);
        //Debug.Log("Planet generated at " + newPlanetoid.transform.position.ToString());
    }

    private void RandomizeMaterial(GameObject newPlanetoid)
    {
        MeshRenderer[] renderer = newPlanetoid.GetComponentsInChildren<MeshRenderer>();
        Texture2D[] skyTextures = Resources.LoadAll<Texture2D>("PlanetSkies");
        Texture2D rsky = skyTextures[Random.Range(0, skyTextures.Length)];
        Texture2D[] baseTextures = Resources.LoadAll<Texture2D>("PlanetBases");
        Texture2D rbase = baseTextures[Random.Range(0, baseTextures.Length)];

        foreach (MeshRenderer r in renderer)
        {
            Material pm = r.material;
            pm.SetTexture("_SkyNoise", rsky);
            pm.SetTexture("_MainTex", rbase);
        }
    }
    
    public GameObject getPlanet()
    {
        int idx = Random.Range(0, planets.Count);
        return planets[idx];
    }

    public void DestroyPlanet(GameObject toFind)
    {
        Destroy(planets[planets.IndexOf(toFind)]);
        planets.RemoveAt(planets.IndexOf(toFind));
    }

    private void DestroyPlanets()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            Destroy(planets[i]);
            planets.RemoveAt(i);
        }
    }
}
