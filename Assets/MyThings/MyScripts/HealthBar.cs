using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar: MonoBehaviour
{
    private Image progressImage;
    [SerializeField] private HealthSystem healthSystem;
    
    private void Awake()
    {
        progressImage = GetComponent<Image>();
    }
    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;

        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void UpdateBar()
    {
        progressImage.fillAmount = healthSystem.GetHealthAmountNormalized();
    }

    private void UpdateHealthBarVisible()
    {
        if (healthSystem.IsFullHealth())
        {
            progressImage.gameObject.SetActive(false);
        }
        else
        {
            progressImage.gameObject.SetActive(true);
        }
    }


}
