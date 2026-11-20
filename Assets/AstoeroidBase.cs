using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstoeroidBase : MonoBehaviour
{
    [SerializeField] public List<Sprite> potentialSprites;
    public void made()
    {
        GetComponent<SpriteRenderer>().sprite = potentialSprites[Random.Range(0, potentialSprites.Count)];
        gameObject.AddComponent<PolygonCollider2D>();

        StartCoroutine(endOff());
    }

    public IEnumerator endOff()
    {
        yield return new WaitForSeconds(15);
        Debug.Log("Destroy");
        Destroy(this.gameObject);
    }
}
