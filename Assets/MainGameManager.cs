using TMPro;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static float score;
    public static bool Started = true;
    [SerializeField] public TMP_Text scoreText;
    public static float multy = 1;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);
        Reset();
    }

    private void FixedUpdate()
    {
        if (Started)
        {
            score += Time.deltaTime * 100 * multy;
            scoreText.text = score.ToString("F0");
            multy += Time.deltaTime / 25;
        }
    }

    public static void gameOver()
    {
        Debug.Log("Done");
        Reset();
    }

    public static void Reset()
    {
        score = 0;
        multy = 1;
        Started = true;

        FindFirstObjectByType<MovementScript>().transform.position = Vector3.zero;
        foreach (AstoeroidBase ast in FindObjectsByType<AstoeroidBase>(FindObjectsSortMode.None))
        {
            Destroy(ast.gameObject);
        }
    }
}
