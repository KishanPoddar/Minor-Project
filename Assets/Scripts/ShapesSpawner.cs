using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] shapes;
    [SerializeField] private Transform[] spawnPoints;

    private GameObject selectedShape;
    private Transform selectedSpawnPoint;
    private bool isSpawing = true;

    private float spawnWaitTime = 0f;
    private float spawnWaitTimer = 2f;

    private void Update()
    {
        spawnWaitTime += Time.deltaTime;
        if (spawnWaitTime > spawnWaitTimer)
            isSpawing = true;

        if (isSpawing)
        {
            spawnWaitTime = 0f;
            StartCoroutine(SpawnShape());
        }
    }

    IEnumerator SpawnShape()
    {
        selectedShape = shapes[Random.Range(0, shapes.Length)];
        selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        isSpawing = false;
        yield return new WaitForSeconds(2f);
        GameObject spawnedObject = Instantiate(selectedShape, selectedSpawnPoint.position, selectedShape.transform.rotation);
        spawnedObject.name = selectedShape.name;
    }


}
