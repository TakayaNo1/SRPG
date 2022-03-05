using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneUI : MonoBehaviour
{
    public Text StartText;

    private float time;

    void Start()
    {
    }

    void Update()
    {
        if (IsPressedAnyKey()) {
            SceneManager.LoadScene("MainGameScene");
        }

        StartText.color = GetAlphaColor(StartText.color);
    }

    private bool IsPressedAnyKey()
    {
        Array arrays = Enum.GetValues(typeof(Player.GamePadBoolKey));
        foreach (Player.GamePadBoolKey key in arrays)
        {
            if (Player.GetButtonDown(key)) return true;
        }
        return false;
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f;
        //color.a = Mathf.Sin(time) * 0.5f + 0.5f;
        color.a = time%8.0<2.0 ? 0.0f : 1.0f;

        return color;
    }
}
