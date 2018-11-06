using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCurrencyControl : MonoBehaviour {
    public int currentMoney;
    public Text moneyText;
	// Use this for initialization
public void PayForUnit(int cost)
    {
        if (cost < currentMoney)
        {
            currentMoney -= cost;
        }
        else
        {
            return;
        }
    }
    private void Update()
    {
        moneyText.text = currentMoney.ToString();
    }
}
