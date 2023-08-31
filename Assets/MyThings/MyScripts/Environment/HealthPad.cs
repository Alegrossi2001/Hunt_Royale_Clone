using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPad : MonoBehaviour
{
    private int healQuantity = 1;
    private float interval = 1f;
    private float timer = 0f;
    private bool isHealing;

    private void OnCollisionEnter(Collision other)
    {
        FriendlyCharacter isCharacterFriendly = other.gameObject.GetComponent<FriendlyCharacter>();
        if(isCharacterFriendly != null)
        {
            HealthSystem healthSystem = other.gameObject.GetComponent<HealthSystem>();
            isHealing = true;
            StartHealing(healthSystem);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isHealing = false;
    }

    private void StartHealing(HealthSystem healthSystem)
    {
        Debug.Log(isHealing);
        if(isHealing)
        {
            timer += Time.time;

            while (timer >= interval)
            {
                healthSystem.Heal(healQuantity);
                timer -= interval;
                
            }
        }
    }

    private void StopHealing()
    {
        isHealing = false;
    }

}
