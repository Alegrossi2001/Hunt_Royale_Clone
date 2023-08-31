using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBar : MonoBehaviour
{
    [SerializeField] private XPManager xpManager;
    private Image xpBar;
    private TextMeshProUGUI levelText;
    private int level = 1;

    private void Awake()
    {
        xpBar = transform.Find("XPBar").GetComponent<Image>();
        levelText = transform.Find("XPText").GetComponent<TextMeshProUGUI>();
        levelText.SetText("LV." + level.ToString());
        xpBar.fillAmount = xpManager.GetXPAmountNormalized();
        xpManager.OnXPPickUp += UpdateXPBar;
        xpManager.OnLevelUp += LevelUpCharacter;
    }

    private void UpdateXPBar(object sender, System.EventArgs e)
    {
        xpBar.fillAmount = xpManager.GetXPAmountNormalized();
    }

    private void LevelUpCharacter(object sender, System.EventArgs e)
    {
        level++;
        levelText.SetText("LV." + level.ToString());
    }
}
