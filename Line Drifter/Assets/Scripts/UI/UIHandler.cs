using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(UIButtons))]
public class UIHandler : MonoBehaviour
{
    #region Variables

    [Header("Components")]

    [SerializeField] private UIGameOver gameOver;
    [SerializeField] private Game game;
    [SerializeField] private UIButtons buttons;
    [SerializeField] private Slider pointsCounter;
    [SerializeField] private Slider firstStar;
    [SerializeField] private Slider secondStar;
    [SerializeField] private Text percentage;
    [SerializeField] private Text levelName;
    [SerializeField] private TextMeshProUGUI hintPrice;
    [SerializeField] private Button hintBuyButton;
    [SerializeField] private List<Image> starsImage = new List<Image>();
    [SerializeField] private Sprite brightStar;
    [SerializeField] private GameObject hintPrefab,hintUI;

    public Sprite Play, Unplay;
    public Image playButtonImage;

    [Header("Variables")]

    [SerializeField] private List<bool> starValue = new List<bool>();
    [SerializeField] private int firstStarPoints;
    [SerializeField] private int secondStarPoints;
    [SerializeField] private int totalPoints = 200;
    [SerializeField] private float timeInterval = 0.1f;
    [SerializeField] private int hintCoinValue = 60;
    [HideInInspector] public int maxPoints;
    [HideInInspector] public int pointsRemaining;
    [HideInInspector] public bool canDraw = true;
    [HideInInspector] public int usedPoints = 0;

    private float amountInPercentage;
    private float timer;

    #endregion

    #region BuiltIn Methods
    private void Awake()
    {
        canDraw = true;
    }

    private void Start()
    {
        SetMainUI();
        HintCheck();
    }
    
    private void Update()
    {
        pointsCounter.value = pointsRemaining;
        if(Time.time > timer)
        {
            amountInPercentage = Mathf.Round((pointsCounter.value / totalPoints) * 100f);
            percentage.text = amountInPercentage.ToString() + "%";
            timer = Time.time + timeInterval;
        }


        if(pointsRemaining < secondStarPoints && starValue[1])
        {
            starValue[1] = StarsCount(1);
            gameOver.Save(1);
        }
        else if(pointsRemaining < firstStarPoints && starValue[2])
        {
            starValue[2] = StarsCount(2);
            gameOver.Save(2);
        }
    }
    #endregion

    #region Custom Methods
    public void StopLine()
    {
        canDraw = false;
    }

    private bool StarsCount(int count)
    {
        var tempColor = starsImage[0].color;
        tempColor.a = 0f;
        for (int i = 3; i > count; i--)
        {
            starsImage[i - 1].color = tempColor;
        }
        return false;
    }

    public void HintUI()
    {
        if(game.gameData.hints[buttons.GetCurrentIndex() - 1] != 1)
        {
            hintUI.SetActive(true);
        }
        else
        {
            hintPrefab.SetActive(true);
        }
    }

    public void BuyHint()
    {
        game.gameData.hints[buttons.GetCurrentIndex() - 1] = 1;
        game.DecreaseCoins(hintCoinValue);
        game.SaveGameData();
        hintUI.SetActive(false);
        hintPrefab.SetActive(true);
    }

    public void UnlockHint()
    {
        game.gameData.hints[buttons.GetCurrentIndex() - 1] = 1;
        game.SaveGameData();
        hintUI.SetActive(false);
        hintPrefab.SetActive(true);
    }

    public void SetMainUI()
    {
        levelName.text = levelName.text + (buttons.GetCurrentIndex()).ToString();

        for (int i = 0; i < starsImage.Count; i++)
        {
            starsImage[i].sprite = brightStar;
            starValue[i] = true;
        }

        maxPoints = totalPoints;
        pointsCounter.maxValue = totalPoints;
        pointsRemaining = totalPoints;
        pointsCounter.value = pointsRemaining;

        firstStar.maxValue = totalPoints;
        secondStar.maxValue = totalPoints;
        firstStar.value = firstStarPoints;
        secondStar.value = secondStarPoints;
    }

    public void HintCheck()
    {
        if(game.CheckCoins(hintCoinValue))
        {
            hintBuyButton.interactable = true;
        }
        else
        {
            hintBuyButton.interactable = false;
        }
        hintPrice.text = "  " + hintCoinValue.ToString();
    }
    #endregion
}
