using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PenItems
{
    public GameObject prefab;
    public int price;
}

public class PensManager : MonoBehaviour
{
    public PenItems[] pens;

    [SerializeField] private Game game;
    [SerializeField] private UIMenu menu;
    [SerializeField] private TextMeshProUGUI[] penItemsText;
    [SerializeField] private GameObject[] penBuyButtons;
    [SerializeField] private GameObject[] penSelectButtons;

    private GameManager manager;

    private void Start()
    {
        UpdatePenUI();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void UpdatePenUI()
    {
        for(int i = 0; i < pens.Length; i++)
        {
            if(game.gameData.pensbuy[i] == 1)
            {
                penBuyButtons[i].SetActive(false);
                penSelectButtons[i].SetActive(true);
            }
            else if(game.gameData.pensbuy[i] == 0)
            {
                penSelectButtons[i].SetActive(false);
                penBuyButtons[i].SetActive(true);
                penItemsText[i].text = pens[i].price.ToString();
            }
        }
    }

    public void SelectItem(int position)
    {
        manager.selectedPencilPrefab = pens[position].prefab;
    }

    public void BuyItem(int i)
    {
        if(pens[i].price <= game.gameData.coinsAmount)
        {
            penBuyButtons[i].SetActive(false);
            penSelectButtons[i].SetActive(true);
            game.DecreaseCoins(pens[i].price);
            game.UnlockPen(i);
            menu.UpdateCoins(pens[i].price);
            game.SaveGameData();
        }
    }
}
