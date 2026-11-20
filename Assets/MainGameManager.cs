using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static float score;
    public static bool Started;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);
    }

    private void FixedUpdate()
    {
        if (Started)
            score = Time.time;
    }

    public static void gameOver()
    {
        Debug.Log("Done");
    }
}
