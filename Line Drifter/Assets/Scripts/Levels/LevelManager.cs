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

    private void Awake()
    {
        AssignValues();
    }

    private void Start()
    {
        UpdateUI();
    }

    private void AssignValues()
    {
        for (int i = 0; i < levelButton.Length; i++)
        {
            textObjects[i] = levelButton[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            levelStarsIamge[i] = this.levelButton[i].transform.GetChild(1).gameObject;
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < levelButton.Length; i++)
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
