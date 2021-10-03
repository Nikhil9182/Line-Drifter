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

    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("GameManager") == null)
        {
            Instantiate(gameManager);
        }
        game.LoadGameData();
        totalCoins.text = game.gameData.coinsAmount.ToString();
    }

    public void UpdateCoins()
    {
        totalCoins.text = game.gameData.coinsAmount.ToString();
    }

}
