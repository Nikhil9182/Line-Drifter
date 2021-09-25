using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] Game game;
    [SerializeField] TextMeshProUGUI[] textObjects;
    [SerializeField] GameObject[] levelButton;
    [SerializeField] GameObject[] levelStarsIamge;
    [SerializeField] Sprite[] starsImage;
    [SerializeField] Sprite lockedLevel, unlockedLevel;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < textObjects.Length; i++)
        {
            if(game.gameData.levels[i] == 1)
            {
                textObjects[i].text = (i + 1).ToString();
                levelButton[i].GetComponent<Button>().interactable = true;
                levelButton[i].GetComponent<Image>().sprite = unlockedLevel;
                levelStarsIamge[i].SetActive(true);
                levelStarsIamge[i].GetComponent<Image>().sprite = starsImage[game.gameData.levelStars[i]];
            }
        }
    }
}
