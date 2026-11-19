using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static float score;
    public static bool Started;

    private void FixedUpdate()
    {
        if (Started)
            score = Time.time;
    }
}
