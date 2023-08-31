using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter(Collision other)
    {
        FriendlyCharacter character = other.gameObject.GetComponent<FriendlyCharacter>();
        if(character != null)
        {
            character.GetComponent<HealthSystem>().Damage(damage);
        }
    }
}
