using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static float score;

    public static string Pname;
    public static bool Started = false;
    [SerializeField] public TMP_Text scoreText;
    public static float multy = 1;

    [SerializeField] public GameObject MainMenu;
    [SerializeField] public GameObject GameOverMenu;
    [SerializeField] public GameObject InputScreen;
    [SerializeField] public GameObject ScoreBoards;
    [SerializeField] public TMP_InputField inpu;

    [SerializeField] public TMP_Text scoreboardNames;
    [SerializeField] public TMP_Text scoreScores;
    public static Dictionary<string, int> LeaderboardData = new Dictionary<string, int>();

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);
    }

    private void FixedUpdate()
    {
        if (Started)
        {
            score += Time.deltaTime * 100 * multy;
            scoreText.text = score.ToString("F0");
            multy += Time.deltaTime / 25;
        }

        if (InputScreen.activeInHierarchy && Input.GetKey(KeyCode.Return))
        {
            setValue();
            Reset();
        }

        if (Input.GetKey(KeyCode.R) && !MainMenu.activeInHierarchy && !ScoreBoards.activeInHierarchy && !InputScreen.activeInHierarchy)
        {
            Reset();
        }
    }

    public void toinput()
    {
        GameOverMenu.SetActive(false);
        MainMenu.SetActive(false);
        ScoreBoards.SetActive(false);
        scoreText.gameObject.SetActive(false);
        InputScreen.SetActive(true);
    }

    public void quitin()
    {
        Application.Quit();
    }

    public void toMainMenu()
    {
        GameOverMenu.SetActive(false);
        MainMenu.SetActive(true);
        ScoreBoards.SetActive(false);
        scoreText.gameObject.SetActive(false);
        InputScreen.SetActive(false);
    }

    public void toHighScores()
    {
        GameOverMenu.SetActive(false);
        MainMenu.SetActive(false);
        ScoreBoards.SetActive(true);
        scoreText.gameObject.SetActive(false);
        InputScreen.SetActive(false);

        scoreScores.text = "";
        scoreboardNames.text = "";

        foreach (KeyValuePair<string, int> scores in LeaderboardData.OrderByDescending(x => x.Value))
        {
            scoreScores.text += scores.Value.ToString() + '\n';
            scoreboardNames.text += scores.Key.ToString() + '\n';
        }
    }

    public void setValue()
    {
        Pname = inpu.text;
    }

    public static void gameOver()
    {
        Started = false;
        if (LeaderboardData.ContainsKey(Pname) && LeaderboardData[Pname] < score)
        {
            LeaderboardData.Remove(Pname);
        }
        if (!LeaderboardData.ContainsKey(Pname))
        {
            LeaderboardData.Add(Pname, Mathf.RoundToInt(score));
        }

        if (LeaderboardData.Count > 10)
        {
            var lowestKey = LeaderboardData.OrderBy(x => x.Value).First().Key;

            LeaderboardData.Remove(lowestKey);
        }
        FindFirstObjectByType<Saving>().SaveGame();
        FindFirstObjectByType<MainGameManager>().GameOverMenu.SetActive(true);
    }

    public static void Reset()
    {
        FindFirstObjectByType<MainGameManager>().InputScreen.SetActive(false);
        score = 0;
        multy = 1;
        Started = true;
        FindFirstObjectByType<MainGameManager>().GameOverMenu.SetActive(false);
        FindFirstObjectByType<MainGameManager>().MainMenu.SetActive(false);
        FindFirstObjectByType<MainGameManager>().ScoreBoards.SetActive(false);
        FindFirstObjectByType<MainGameManager>().scoreText.gameObject.SetActive(true);
        FindFirstObjectByType<Spawners>().GameStart();

        FindFirstObjectByType<MovementScript>().transform.position = Vector3.zero;
        foreach (AstoeroidBase ast in FindObjectsByType<AstoeroidBase>(FindObjectsSortMode.None))
        {
            Destroy(ast.gameObject);
        }
    }

    public void Load(GameData data)
    {
        LeaderboardData = data.scores.ToDictionary(x => x.name, x => x.score);
    }

    public void Save(ref GameData data)
    {
        data.scores = LeaderboardData.Select(x => new ScoreEntry{name = x.Key, score = x.Value}).ToList();
    }

}
