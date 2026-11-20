using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [SerializeField] public GameObject AsteroidBase;

    [SerializeField] public List<Transform> asteroidSpawners;

    public float waitTime;

    private void Start()
    {
        StartCoroutine(cooldownSpawn());
    }

    public IEnumerator cooldownSpawn()
    {
        int rand = Random.Range(0, asteroidSpawners.Count);
        GameObject newast = Instantiate(AsteroidBase, asteroidSpawners[rand].position, asteroidSpawners[rand].rotation);
        if (asteroidSpawners[rand].position.x != 0)
        {
            if (asteroidSpawners[rand].position.x > 0)
            {
                newast.transform.position = new Vector2(newast.transform.position.x, newast.transform.position.y + Random.Range(-asteroidSpawners[rand].localScale.y / 2, asteroidSpawners[rand].localScale.y / 2));
                newast.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.04f, 0));
            }
            else
            {
                newast.transform.position = new Vector2(newast.transform.position.x, newast.transform.position.y + Random.Range(-asteroidSpawners[rand].localScale.y / 2, asteroidSpawners[rand].localScale.y / 2));
                newast.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.04f, 0));
            }
        }
        else
        {
            if (asteroidSpawners[rand].position.y > 0)
            {
                newast.transform.position = new Vector2(newast.transform.position.x + Random.Range(-asteroidSpawners[rand].localScale.x / 2, asteroidSpawners[rand].localScale.x / 2), newast.transform.position.y);
                newast.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -0.04f));
            }
            else
            {
                newast.transform.position = new Vector2(newast.transform.position.x + Random.Range(-asteroidSpawners[rand].localScale.x / 2, asteroidSpawners[rand].localScale.x / 2), newast.transform.position.y);
                newast.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0.04f));
            }
        }
        newast.GetComponent<AstoeroidBase>().made();
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(cooldownSpawn());
    }
}
