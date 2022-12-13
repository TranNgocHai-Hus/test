using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DailyRewardPanelsDay3 : MonoBehaviour
{
    public float MsToWait = 5000.0f;
    public Text ChestTimer;
    private Button ChestButton;
    private ulong lastchestopen;
    public GameObject DailyBonusPopup;
    public int CoinsGive;
    public Text DailyValue;
    private void Start()
    {
        ChestButton = GetComponent<Button>();
        if (PlayerPrefs.GetInt("ValueOnceSet1") == 1)
        {
            //do nothing
        }
        else
        {
            if (PlayerPrefs.GetInt("Callchestonce3") == 1)
            {

            }
            else
            {
                PlayerPrefs.SetInt("Callchestonce3", 1);
                ChestClick();
            }
        }
        lastchestopen =  ulong.Parse(PlayerPrefs.GetString("LastChestOpenDay3"));
        if (!isChestReady())
        {
            ChestButton.interactable = false;
        }

    }

    private void Update()
    {
        if (!ChestButton.IsInteractable())
        {
            if (isChestReady())
            {
                ChestButton.interactable = true;
                return;
            }


            //Set Timer
            ulong diff = ((ulong)DateTime.Now.Ticks - lastchestopen);

            ulong m = diff / TimeSpan.TicksPerMillisecond;

            float SecondLeft = (float)(MsToWait - m) / 1000;

            string r = "";

            // Hours
            r += ((int)SecondLeft / 3600).ToString() + "h ";
            SecondLeft -= ((int)SecondLeft / 3600) * 3600;
            // Minutes
            r += ((int)SecondLeft / 60).ToString("00") + "m ";
            //seconds
            r += (SecondLeft % 60).ToString("00") + "s";

            ChestTimer.text = r;
        }
    }

    public void ChestClick()
    {
        lastchestopen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastChestOpenDay3", lastchestopen.ToString());
        ChestButton.interactable = false;
        if (PlayerPrefs.GetInt("ValueOnceSet1") == 1)
        {
            DailyBonusPopup.SetActive(true);
            DailyValue.text = "You Got " + CoinsGive + " Coins";
            DeductionMoney.instance.GiveCoin(CoinsGive);
        }
        else
        {
            //do nothing
        }
        // DeductionMoney.instance.GiveCoin(2000);
    }

    private bool isChestReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastchestopen);

        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float SecondLeft = (float)(MsToWait - m) / 1000;
       // Debug.Log(SecondLeft);
        if (SecondLeft < 0)
        {
            ChestTimer.text = "Ready!";
            PlayerPrefs.SetInt("ValueOnceSet1", 1);
            return true;
        }
        return false;   
    }

    public void PopupOff()
    {
        DailyBonusPopup.SetActive(false);
    }
}
