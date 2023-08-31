using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star: MonoBehaviour
{
    private int xpDropped;
    private Transform starPosition;
    public static EventHandler<OnStarSpawnEventArgs> OnStarSpawn;
    public class OnStarSpawnEventArgs : EventArgs
    {
        public Transform starPosition;
    }

    public static EventHandler<OnPickUpEventArgs> OnStarPickup;
    public class OnPickUpEventArgs : EventArgs
    {
        public FriendlyCharacter character;
        public int xpDropped;
    }

    public static Star SpawnStar(int xp, Transform position, Color color)
    {
        Transform pfStar = Resources.Load<Transform>("StarDrop");
        Transform starTransform = Instantiate(pfStar, position.position, Quaternion.identity);
        Renderer renderer = starTransform.GetComponentInChildren<Renderer>();
        renderer.material.SetColor("StarColor", color);
        Star star = starTransform.GetComponent<Star>();
        star.xpDropped = xp;

        return star;
    }

    private void Awake()
    {
        starPosition = this.transform;
        OnStarSpawn?.Invoke(this, new OnStarSpawnEventArgs { starPosition = starPosition});
    }


    private void OnCollisionEnter(Collision other)
    {
        FriendlyCharacter character = other.gameObject.GetComponent<FriendlyCharacter>(); 
        if (character != null)
        {
            OnStarPickup?.Invoke(this, new OnPickUpEventArgs { 
            character = character, 
            xpDropped = xpDropped}) ;

            character.gameObject.GetComponentInChildren<XPManager>().EarnXP(xpDropped);
            Destroy(gameObject);
        }
    }
}
