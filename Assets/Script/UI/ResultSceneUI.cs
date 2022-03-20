using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneUI : MonoBehaviour
{

    public Text ScoreText;

    void Start()
    {
        ScoreText.text = "";
        int i = 0;

        while (PlayerPrefs.HasKey("Name_" + i))
        {
            string name = PlayerPrefs.GetString("Name_" + i);
            int score = PlayerPrefs.GetInt("Score_" + i);
            ScoreText.text += name+" "+score+"“_\n";
            i++;
        }
    }

    void Update()
    {
        
    }
}
