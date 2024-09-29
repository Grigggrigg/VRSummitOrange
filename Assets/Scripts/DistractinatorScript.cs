using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractinatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject distraction;
    public float spawnRate = 4;
    private float timer = 0;
    public float heightOffset = 4;
    void Start()
    {
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnPipe();
            timer = 0;
        }
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        

        Instantiate(distraction, new Vector3(transform.position.x + Random.Range(5, 15), Random.Range(lowestPoint, highestPoint) + Random.Range(0, 15), Random.Range(0, 15)), transform.rotation);
        Instantiate(distraction, new Vector3(transform.position.x + Random.Range(5, 15), Random.Range(lowestPoint, highestPoint) + Random.Range(0, 15), Random.Range(0, 15)), transform.rotation);
        Instantiate(distraction, new Vector3(transform.position.x + Random.Range(5, 15), Random.Range(lowestPoint, highestPoint) + Random.Range(0, 15), Random.Range(0, 15)), transform.rotation);
    }
}

