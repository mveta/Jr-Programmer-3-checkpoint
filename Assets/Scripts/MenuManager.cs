using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public TMP_InputField nameInput;
    public GameObject warningText;
    public TextMeshProUGUI highScoreText;
    public static string playerName;

    public string highScorer;
    public int highScore;




    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        LoadHS();
        highScoreText.text = "Best score: " + highScorer + " : " + highScore;
    }
    public void StartNew()
    {
        if(nameInput.text == "")
        {
            warningText.SetActive(true);
        }
        else
        {
            playerName = nameInput.text;
            SceneManager.LoadScene(1);
        }
        
       
    }
    public void Quit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else 
        Application.Quit();
        #endif
    }

    [System.Serializable]
    public class HighScoreData
    {
        public string playerName;
        public int score;
    }
    public void SaveHS(int points)
    {
        HighScoreData data = new HighScoreData();

        data.playerName = playerName;
        data.score = points;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }
    public void LoadHS()
    {
        string path = Application.persistentDataPath + "/highscore.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);

            highScorer = data.playerName;
            highScore = data.score;
        }
    }
}
