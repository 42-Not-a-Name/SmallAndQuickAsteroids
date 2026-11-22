using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [SerializeField] public GameObject AsteroidBase;

    [SerializeField] public List<Transform> asteroidSpawners;

    public float waitTime;
    public float newwaitTime;

    public void GameStart()
    {
        StopAllCoroutines();
        StartCoroutine(cooldownSpawn());
        newwaitTime = waitTime;
    }

    private void FixedUpdate()
    {

    }

    public IEnumerator cooldownSpawn()
    {
        if (MainGameManager.Started)
        {
            int rand = Random.Range(0, asteroidSpawners.Count);
            GameObject newast = Instantiate(AsteroidBase, asteroidSpawners[rand].position, asteroidSpawners[rand].rotation);
            if (asteroidSpawners[rand].position.x != 0)
            {
                if (asteroidSpawners[rand].position.x > 0)
                {
                    newast.transform.position = new Vector2(newast.transform.position.x, newast.transform.position.y + Random.Range(-asteroidSpawners[rand].localScale.y / 2, asteroidSpawners[rand].localScale.y / 2));
                    newast.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.025f * (1 + (MainGameManager.score / (5000 * MainGameManager.multy))), 0));
                }
                else
                {
                    newast.transform.position = new Vector2(newast.transform.position.x, newast.transform.position.y + Random.Range(-asteroidSpawners[rand].localScale.y / 2, asteroidSpawners[rand].localScale.y / 2));
                    newast.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.025f * (1 + (MainGameManager.score / (5000 * MainGameManager.multy))), 0));
                }
            }
            else
            {
                if (asteroidSpawners[rand].position.y > 0)
                {
                    newast.transform.position = new Vector2(newast.transform.position.x + Random.Range(-asteroidSpawners[rand].localScale.x / 2, asteroidSpawners[rand].localScale.x / 2), newast.transform.position.y);
                    newast.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -0.025f * (1 + (MainGameManager.score / (5000 * MainGameManager.multy)))));
                }
                else
                {
                    newast.transform.position = new Vector2(newast.transform.position.x + Random.Range(-asteroidSpawners[rand].localScale.x / 2, asteroidSpawners[rand].localScale.x / 2), newast.transform.position.y);
                    newast.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0.025f * (1 + (MainGameManager.score / (5000 * MainGameManager.multy)))));
                }
            }
            newast.GetComponent<AstoeroidBase>().made();
            if (newwaitTime > 0.1)
            {
                newwaitTime = waitTime - (MainGameManager.score / (8000 * MainGameManager.multy));
            }
            yield return new WaitForSeconds(newwaitTime);
            StartCoroutine(cooldownSpawn());
        }
    }
}
