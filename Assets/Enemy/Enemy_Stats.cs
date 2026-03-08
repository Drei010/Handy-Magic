using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Stats : MonoBehaviour
{

    public OutputListener playerSpells;
    [SerializeField] private int maxHealthPoints = 100;
    [SerializeField] private int maxManaPoints = 100;
    [SerializeField] private int maxUltimatePoints = 100;
    [SerializeField] private int initialHealthPoints = 90;
    [SerializeField] private int initialManaPoints = 50;
    [SerializeField] private int initialUltimatePoints = 20;
    [SerializeField] private int regenerationRate = 10;

    [Header("Current Stats")]
    [SerializeField] private int currentHealthPoints;
    [SerializeField] private int currentManaPoints;
    [SerializeField] private int currentUltimatePoints;

    private void OnCollisionEnter(Collision collision)
{
        Dictionary<string, int> spellDamage = new Dictionary<string, int>()
        {
            { "PlayerBlast", playerSpells.blastDamage },
            { "PlayerFireball", playerSpells.fireballDamage },
            { "PlayerBolt", playerSpells.boltDamage },
            { "PlayerCounterBlast", playerSpells.counterDamage},
            { "PlayerThorns", playerSpells.thornsDamage }
        };

        if (spellDamage.ContainsKey(collision.gameObject.tag))
        {
            int damage = spellDamage[collision.gameObject.tag];
            TakeDamage(damage);
            Debug.Log("Hit Damage" + collision.gameObject.tag);
        }
}

    private void TakeDamage(int damage)
    {
        CurrentHealthPoints -= damage;
        if (CurrentHealthPoints <= 0)
        {
            // Handle character death (e.g., respawn, game over, etc.)
        }
    }
    private int healthPoints;
    public int HealthPoints
    {
        get { return healthPoints; }
        set { healthPoints = Mathf.Clamp(value, 0, maxHealthPoints); }
    }

    private int manaPoints;
    public int ManaPoints
    {
        get { return manaPoints; }
        set { manaPoints = Mathf.Clamp(value, 0, maxManaPoints); }
    }

    private int ultimatePoints;
    public int UltimatePoints
    {
        get { return ultimatePoints; }
        set { ultimatePoints = Mathf.Clamp(value, 0, maxUltimatePoints); }
    }

    public int CurrentHealthPoints
    {
        get { return currentHealthPoints; }
        set { currentHealthPoints = Mathf.Clamp(value, 0, maxHealthPoints); }
    }

    public int MaxHealthPoints => maxHealthPoints;

    public int CurrentManaPoints
    {
        get { return currentManaPoints; }
        set
        {
            currentManaPoints = Mathf.Clamp(value, 0, maxManaPoints);
  
             currentManaPoints = Mathf.Min(currentManaPoints, maxManaPoints);
        }
    }

    public int MaxManaPoints => maxManaPoints;

    public int CurrentUltimatePoints
    {
        get { return currentUltimatePoints; }
        set { currentUltimatePoints = Mathf.Clamp(value, 0, maxUltimatePoints); }
    }

    public int MaxUltimatePoints => maxUltimatePoints;

    void Start()
    {
        healthPoints = initialHealthPoints;
        currentHealthPoints = initialHealthPoints;
        manaPoints = initialManaPoints;
        currentManaPoints = initialManaPoints;
        ultimatePoints = initialUltimatePoints;
        currentUltimatePoints = initialUltimatePoints;
        StartCoroutine(RegenerateMana());
    }

    IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CurrentManaPoints += regenerationRate;
        }
    }
}
