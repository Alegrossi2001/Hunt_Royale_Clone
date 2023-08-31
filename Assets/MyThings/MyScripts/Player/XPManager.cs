using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    public event EventHandler OnXPPickUp;
    public event EventHandler OnLevelUp;

    private int level;
    private int XPNeededForNextLevel;
    private int currentXp;
    private int totalXP;

    private void Awake()
    {
        level = 0;
        currentXp= 0;
        XPNeededForNextLevel = 100;
    }

    public void EarnXP(int xp)
    {
        currentXp += xp;
        totalXP+= xp;
        OnXPPickUp?.Invoke(this, EventArgs.Empty);
        if(currentXp >= XPNeededForNextLevel)
        {
            level++;
            currentXp = currentXp - XPNeededForNextLevel;
            XPNeededForNextLevel = Mathf.RoundToInt(XPNeededForNextLevel * 1.10f);
            OnLevelUp?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetXPAmountNormalized()
    {
        return (float)currentXp/XPNeededForNextLevel;
    }

    public int GetTotalXP()
    {
        return totalXP;
    }


}
