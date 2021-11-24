using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    [SerializeField] private GameObject[] pensObjects;
    [SerializeField] private TextMeshProUGUI[] penItemsText;
    [SerializeField] private GameObject[] penBuyButtons;
    [SerializeField] private GameObject[] penSelectButtons;

    private int point = 0;

    private void Awake()
    {
        LoadPenObjects();
    }

    private void Start()
    {
        UpdatePenUI();
    }

    private void LoadPenObjects()
    {
        for (int i = 0; i < pensObjects.Length; i++)
        {
            penItemsText[i] = pensObjects[i].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            penBuyButtons[i] = this.pensObjects[i].transform.GetChild(1).gameObject;
            penSelectButtons[i] = this.pensObjects[i].transform.GetChild(2).gameObject;
        }
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
        penSelectButtons[point].GetComponent<Button>().interactable = true;
        point = position;
        penSelectButtons[position].GetComponent<Button>().interactable = false;
        GameManager.Instance.selectedPencilPrefab = pens[position].prefab;
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
