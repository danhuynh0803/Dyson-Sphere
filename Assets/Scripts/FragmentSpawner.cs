using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentSpawner : MonoBehaviour
{
    //public GameObject[] p1List, p2List;
    [Header("Spawn objects")]
    public GameObject[] fragmentObjects;
    public GameObject meteor;
    public Transform planetTransform;

    // Number of fragments to spawn for each player
    [Header("Spawn amount")]
    public int numFragments;
    public float spawnRadius;

    [Header("Spawn Delay")]
    public float spawnDelayMin = 3.0f;
    public float spawnDelayMax = 10.0f;
    private float spawnTimer = 0.0f;

    void Start()
    {
        spawnTimer = spawnDelayMin;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        // Spawn the objects around the planet randomly
        if (spawnTimer <= 0)
        {
            //Debug.Log("Spawn Meteor");
            SpawnMeteor();
            spawnTimer = Random.Range(spawnDelayMin, spawnDelayMax);
        }
    }

    void SpawnMeteor()
    {
        // Spawn a random spot around the planet
        Instantiate(meteor, planetTransform.position + Random.insideUnitSphere*spawnRadius, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

}
