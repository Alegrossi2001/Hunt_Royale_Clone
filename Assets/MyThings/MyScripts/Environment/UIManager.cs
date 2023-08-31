using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class UIManager: MonoBehaviour
{
    //[SerializeField] private FriendlyCharacter[] characters;
    [SerializeField] private List<FriendlyCharacter> characters = new List<FriendlyCharacter>();
    private Dictionary<FriendlyCharacter, int> scoreDictionary = new Dictionary<FriendlyCharacter, int>();
    private Dictionary<FriendlyCharacter, TextMeshProUGUI> scoreTextDictionary = new Dictionary<FriendlyCharacter, TextMeshProUGUI>();
    private Dictionary<FriendlyCharacter, HealthSystem> healthSystemDictionary = new Dictionary<FriendlyCharacter, HealthSystem>();

    //death display
    [SerializeField] private Transform deathDisplay;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private GameObject playerDeathDisplay;
    //player death

    private void Awake()
    {
        int offsetAmount = -120;
        int index = 1;
        Transform scoreTransform = this.transform.Find("ScoreBoardTransform");
        scoreTransform.gameObject.SetActive(false);

        foreach(FriendlyCharacter character in characters)
        {
            Transform scoreObject = Instantiate(scoreTransform, transform);
            TextMeshProUGUI titleText = scoreObject.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = scoreObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            scoreObject.gameObject.SetActive(true);
            scoreObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, offsetAmount * index);
            scoreTextDictionary[character] = scoreText;
            string name = character.transform.name;
            titleText.SetText(name);
            int score = scoreDictionary[character] = 0;
            
            scoreText.SetText(score.ToString());
            index++;
        }
        foreach(FriendlyCharacter character in characters)
        {
            HealthSystem healthSystem = character.gameObject.GetComponent<HealthSystem>();
            if(healthSystem!= null)
            {
                healthSystemDictionary[character] = healthSystem;
            }
        }

        deathDisplay.gameObject.SetActive(false);
        playerDeathDisplay.gameObject.SetActive(false);

    }

    private void Start()
    {
        Star.OnStarPickup += UpdateScore;
        //death manager
        foreach (KeyValuePair<FriendlyCharacter, HealthSystem> healthSystem in healthSystemDictionary)
        {
            healthSystem.Value.OnDied += BroadCastCharacterDeath; 
        }

        Player.OnPlayerDeath += BroadCastGameOver;
    }

    private void UpdateScore(object sender, Star.OnPickUpEventArgs e)
    {
        int score = scoreDictionary[e.character] += e.xpDropped;
        TextMeshProUGUI text = scoreTextDictionary[e.character];
        text.SetText(score.ToString());

    }

    private void BroadCastCharacterDeath(object sender, System.EventArgs e)
    {
        //Loop through the health dictionary and find whomever's health reached zero

        List<FriendlyCharacter> charactersToRemove = new List<FriendlyCharacter>();
        foreach(FriendlyCharacter character in characters)
        {

            bool isCharacterDead = healthSystemDictionary[character].IsDead();
            if (isCharacterDead == true)
            {
                string name = character.transform.name;
                deathText.SetText(name + " has been slain");
                deathDisplay.gameObject.SetActive(true);
                charactersToRemove.Add(character);
                
            }
        }

        foreach(FriendlyCharacter character in charactersToRemove)
        {
            characters.Remove(character);
        }
    }

    private void BroadCastGameOver(object sender, System.EventArgs e)
    {
        playerDeathDisplay.gameObject.SetActive(true);
    }
}
