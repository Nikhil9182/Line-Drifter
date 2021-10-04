using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMenu : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Game game;
    [SerializeField] private TextMeshProUGUI totalCoins;
    [SerializeField] private GameObject gameManager;

    [SerializeField] private float waitTime = 0.001f;

    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("GameManager") == null)
        {
            Instantiate(gameManager);
        }
        game.LoadGameData();
        totalCoins.text = game.gameData.coinsAmount.ToString();
    }

    public void UpdateCoins(int price)
    {
        StartCoroutine(UpdateText(price));
        totalCoins.text = game.gameData.coinsAmount.ToString();
    }

    IEnumerator UpdateText(int price)
    {
        int totalAmount = price + game.gameData.coinsAmount;
        while(totalAmount > game.gameData.coinsAmount)
        {
            totalAmount -= 10;
            totalCoins.text = totalAmount.ToString();
            yield return new WaitForSeconds(waitTime);
        }
    }

}
