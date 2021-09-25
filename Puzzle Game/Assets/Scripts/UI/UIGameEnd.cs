using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameEnd : MonoBehaviour
{
    #region Variables
    [Header("Components")]

    [SerializeField] private Game game;
    [SerializeField] private ParticleSystem winParticles;
    [SerializeField] private Animator[] starsAnim;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private TextMeshProUGUI coinsWon;
    [SerializeField] private Button nextLevel;

    [Header("Variables")]

    [SerializeField] private float timeDelay;
    [SerializeField] private float delayTime;
    [SerializeField] private float waitTime;
    [HideInInspector] public int starsCount;

    private int coinsWonAmount;

    #endregion

    #region BuiltIn Methods
    private void Start()
    {
        game.LoadGameData();
        starsCount = PlayerPrefs.GetInt("sCount");
        coinsWonAmount = 0;
        coinsWon.text =  "+" + coinsWonAmount.ToString();
        if(starsCount == 0)
        {
            OnLoose();
        }
        else if(starsCount > 0)
        {
            OnWon();
        }
    }

    #endregion

    #region Custom Methods
    public void OnWon()
    {
        EndText();
        StartCoroutine(ShowStars());
        NextButton(true);
    }
    public void OnLoose()
    {
        EndText();
        StartCoroutine(OnLooseUI());
        NextButton(false);
    }

    IEnumerator ShowStars()
    {
        yield return new WaitForSeconds(waitTime);

        if (starsCount == 1)
        {
            coinsWonAmount = 10;
            winParticles.Play();
            game.IncreaseCoins(coinsWonAmount);
            game.SaveGameData();
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(timeDelay);
                if(i < starsCount)
                {
                    starsAnim[i].Play("GotStar");
                }
                else
                {
                    starsAnim[i].Play("NoStar");
                }
            }
            StartCoroutine(UpdateCoinCount());
        }
        else if(starsCount == 2)
        {
            coinsWonAmount = 30;
            winParticles.Play();
            game.IncreaseCoins(coinsWonAmount);
            game.SaveGameData();
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(timeDelay);
                if (i < starsCount)
                {
                    starsAnim[i].Play("GotStar");
                }
                else
                {
                    starsAnim[i].Play("NoStar");
                }
            }
            StartCoroutine(UpdateCoinCount());
        }
        else if (starsCount == 3)
        {
            coinsWonAmount = 50;
            winParticles.Play();
            game.IncreaseCoins(coinsWonAmount);
            game.SaveGameData();
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(timeDelay);
                if (i < starsCount)
                {
                    starsAnim[i].Play("GotStar");
                }
                else
                {
                    starsAnim[i].Play("NoStar");
                }
            }
            StartCoroutine(UpdateCoinCount());
        }
    }

    IEnumerator OnLooseUI()
    {
        yield return new WaitForSeconds(waitTime);
        coinsWonAmount = 0;

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(timeDelay);
            starsAnim[i].Play("NoStar");
        }

        coinsWon.text = "+" + coinsWonAmount.ToString();
    }

    IEnumerator UpdateCoinCount()
    {
        int coins = 0;
        while (coins < coinsWonAmount)
        {
            coins += 1;
            coinsWon.text = "+" + coins.ToString();
            yield return new WaitForSeconds(delayTime);
        }
    }

    private void NextButton(bool isWon)
    {
        int index = PlayerPrefs.GetInt("index");
        if(isWon)
        {
            game.UnlockNextLevel(index);
            nextLevel.interactable = true;
            game.SaveGameData();
        }
        if(!isWon)
        {
            if(game.gameData.levels[index + 1] == 1)
            {
                nextLevel.interactable = true;
            }
            else
            {
                nextLevel.interactable = false;
            }
        }
    }

    private void EndText()
    {
        if(starsCount == 0)
        {
            endText.text = "YOU LOOSE";
        }
        else if(starsCount == 1)
        {
            endText.text = "GREAT";
        }
        else if(starsCount == 2)
        {
            endText.text = "SUPERB";
        }
        else if(starsCount == 3)
        {
            endText.text = "AWESOME";
        }
    }
    #endregion
}
