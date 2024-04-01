using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField playerName;
    public static UIManager Instance;
    public string highestScoreName;
    public int highestScore;
    public Dictionary<string, int> bestScoresDictionary = new Dictionary<string, int>();

    public string scoreBoardTexts;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadHighScoreData();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnChangedInputField(string input)
    {
        Debug.Log("[OnChangedInputField] " + input);
    }

    public void OnEndInputField(string input)
    {
        name = input;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenScoreBoard()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public string highestScoreName;
        public int highestScore;
        public string scoreBoard;
    }

    public void SaveHighestScore(string name, int score)
    {
        SaveData data = new SaveData();
        if (score > highestScore)
        {
            data.highestScore = score;
            data.highestScoreName = name;

            highestScore = score;
            highestScoreName = name;
        }

        string scoreBoardText = ScoresDictionaryToString();
        scoreBoardTexts += scoreBoardText;
        data.scoreBoard = scoreBoardTexts;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }


    public void LoadHighScoreData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highestScore = data.highestScore;
            highestScoreName = data.highestScoreName;
            scoreBoardTexts = data.scoreBoard;
        }
    }

    public void UpdateScoreBoard(string name, int score)
    {
        if (!bestScoresDictionary.ContainsKey(name))
        {
            bestScoresDictionary.Add(name, score);
        }
        else
        {
            bestScoresDictionary[name] = score;
        }
    }

    private string ScoresDictionaryToString()
    {
        string text = "";
        foreach (var item in bestScoresDictionary)
        {
            text += $"{item.Key} : {item.Value} \n ";
        }

        return text;
    }
}
