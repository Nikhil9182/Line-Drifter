using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(UIHandler))]
public class UIGameOver : MonoBehaviour
{
    [SerializeField] private UIButtons buttons;
    [SerializeField] private Game game;
    private void Start()
    {
        PlayerPrefs.SetInt("sCount", 3);
    }

    public void Save(int count)
    {
        PlayerPrefs.SetInt("sCount", count);
    }

    public void NextScene()
    {
        PlayerPrefs.SetInt("index", buttons.GetCurrentIndex());
        int i = PlayerPrefs.GetInt("index") - 1;
        if (game.gameData.levelStars[i] < PlayerPrefs.GetInt("sCount"))
        {
            game.gameData.levelStars[i] = PlayerPrefs.GetInt("sCount");
            game.SaveGameData();
        }
        SceneManager.LoadScene("GameEnd");
    }
}
