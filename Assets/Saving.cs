using System.IO;
using UnityEngine;

public class Saving : MonoBehaviour
{
    public static Saving inst {  get; private set; }

    private GameData gameData;

    private string dataDirPath = "";
    private string dataFileName = "Pass-teroids.json";

    private void Awake()
    {
        dataDirPath = Application.persistentDataPath;
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadGame();
    }

    public void LoadGame()
    {


        string fullpath = Path.Combine(dataDirPath, dataFileName);

        GameData loaded = null;
        if (File.Exists(fullpath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullpath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loaded = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch
            {
                Debug.LogError("Fuck");
            }
        }
        this.gameData = loaded;
        if (this.gameData == null)
        {
            this.gameData = new GameData();
        }

        FindFirstObjectByType<MainGameManager>().Load(gameData);

    }

    public void SaveGame()
    {
        FindFirstObjectByType<MainGameManager>().Save(ref gameData);

        string fullpath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
            string dataToStore = JsonUtility.ToJson(gameData, true);
            using (FileStream stream = new FileStream(fullpath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch
        {
            Debug.LogError("Fuck");
        }
    }
}
