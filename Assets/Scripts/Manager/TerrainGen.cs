using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    [SerializeField] private Transform Startend;
    [SerializeField] private List<Transform> Terrainlist;
    [SerializeField] private Transform p;

    private float spawndistance = 200f;
    private Vector3 lastend;

    List<GameObject> hiechary = new List<GameObject>();

    private void Awake()
    {        

        lastend = Startend.position;

        for (int i = 0; i < 3; i++)
        {
            spawn();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(p.position, lastend) < spawndistance)
        {
            spawn();
        }
        Destroyterrain();
    }

    private void spawn()
    {
        Transform randomterrain = Terrainlist[Random.Range(0, Terrainlist.Count)];
        Transform lastterrain = newterrainspawn(randomterrain, lastend);
        lastend = lastterrain.Find("Endmap").position;
    }

    private Transform newterrainspawn(Transform terrain, Vector3 Spawnpos)
    {
        Transform newterrain = Instantiate(terrain, Spawnpos, Quaternion.identity);
        return newterrain;
    }
    
    private void Destroyterrain()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Terrainmap"))
        {
            if (o.transform.Find("Endmap").position.x < (p.position.x - 70f))
            {
                Destroy(o);
            }
        }
    }
}