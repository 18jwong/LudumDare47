using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject blobPrefab = null;
    [SerializeField] private Transform[] spawnPoint = null;
    [SerializeField] private float spawnRate = 1f;

    private List<GameObject> blobs = null;

    public static Spawner instance;
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        blobs = new List<GameObject>();
    }

    private IEnumerator SpawnBlobs()
    {
        while(true)
        {
            SpawnABlob();
            yield return new WaitForSeconds(1f/spawnRate);
        }
    }

    void SpawnABlob()
    {
        int p = (int)Mathf.Round(Random.Range(0,3));
        GameObject blob = Instantiate(blobPrefab, spawnPoint[p].position, Quaternion.identity);
        blobs.Add(blob);
    }

    public void StartSpawning()
    {
        StartCoroutine("SpawnBlobs");
    }

    public void StopSpawning()
    {
        StopCoroutine("SpawnBlobs");
    }

    public void KillAllBlobs()
    {
        StopSpawning();
        foreach(GameObject g in blobs)
        {
            Destroy(g);
        }
    }

    public void AddToSpawnRate(float i){
        spawnRate += i;
    }
}
