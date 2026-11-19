using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [SerializeField] public GameObject AsteroidBase;

    [SerializeField] public Dictionary<Transform, Vector2> asteroidSpawners;

    public float waitTime;

    private void FixedUpdate()
    {
        
    }

    public IEnumerator cooldownSpawn()
    {
        int rand = Random.Range(0, asteroidSpawners.Count);
        GameObject newast = Instantiate(AsteroidBase, asteroidSpawners.Keys.ElementAt(rand));
        newast.GetComponent<Rigidbody2D>().AddForce(asteroidSpawners.Values.ElementAt(rand));
        yield return new WaitForSeconds(waitTime);

    }
}
