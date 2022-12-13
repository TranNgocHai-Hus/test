using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DailyRewardPanelsDay1 : MonoBehaviour
{
    public float MsToWait = 5000.0f;
    public Text ChestTimer;
    private Button ChestButton;
    private ulong lastchestopen;
    public int CoinsGive;
    public GameObject DailyBonusPopup;
    public Text DailyValue;
    private void Start()
    {
        ChestButton = GetComponent<Button>();
        lastchestopen=  ulong.Parse(PlayerPrefs.GetString("LastChestOpenDay1"));

        if (PlayerPrefs.GetInt("NewValueDay1") == 1)
        {
            MsToWait = 6.048e+08f;
        }
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
        PlayerPrefs.SetString("LastChestOpenDay1", lastchestopen.ToString());
        ChestButton.interactable = false;
        DeductionMoney.instance.GiveCoin(CoinsGive);
        DailyBonusPopup.SetActive(true);
        DailyValue.text = "You Got " + CoinsGive +" Coins";
        PlayerPrefs.SetInt("NewValueDay1", 1);
        MsToWait = 6.048e+08f;
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
            return true;
        }
              

        return false;   
    }

    public void PopupOff()
    {
        DailyBonusPopup.SetActive(false);
    }
}
