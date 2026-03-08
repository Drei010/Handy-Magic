using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Char_Stats : MonoBehaviour
{
    public EnemySpells enemySpells;
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
     [SerializeField] private int currentDefPoints;

    public double weakenDamageReduction;
     public bool weakenExpired;
     public bool thornsExpired;

    public GameObject thornsPrefab; 
    public Vector3  thornsSpawnPoint;
    private void OnCollisionEnter(Collision collision)
{
if (collision.gameObject.CompareTag("EnemyBlast") || collision.gameObject.CompareTag("EnemyFireball"))
{
    if (!thornsExpired)
    {
        GameObject Thorns = Instantiate(thornsPrefab, thornsSpawnPoint, Quaternion.identity);
        Thorns.tag = "PlayerThorns";
        Destroy(Thorns, 1f);
    }

    int damage = collision.gameObject.CompareTag("EnemyBlast") ? enemySpells.blastDamage : enemySpells.fireballDamage;

    if (!weakenExpired)
    {
        damage = (int)(damage * (1 - (weakenDamageReduction / 100)));
    }

    TakeDamage(damage);
}

}

    private void TakeDamage(int damage)
    {   
        
        if(CurrentDefPoints <= 0){
        CurrentHealthPoints -= damage;
        if (CurrentHealthPoints <= 0)
        {
            // Handle character death (e.g., respawn, game over, etc.)
        }
        }else{

            CurrentDefPoints -= damage;
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
     public int CurrentDefPoints
    {
        get { return currentDefPoints; }
        set { currentDefPoints = Mathf.Clamp(value, 0, 30); }
    }
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
