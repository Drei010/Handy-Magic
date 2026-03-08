using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpells : MonoBehaviour
{
    public GameObject objectToAnimate;
    
    public AudioClip blastClip;
    public AudioClip healClip;
    public AudioClip blockClip;
    public AudioClip fireballClip;

    public AudioSource audioSource;
    public string spellOutput = "";
    public Enemy_Stats enemyStats;
    [Header("Blast Stats")]
    public int blastManaCost = 20;

    public int blastUltGain = 10; 

    public int blastDamage = 10;//Placeholder
   public float blastDuration = 5f;
    public float projectileSpeed = 10.0f; // Editable speed in Unity Editor
   public GameObject blastPrefab; // Reference to your blast prefab
    public Vector3 blastSpawnPoint = new Vector3(0f, 0.5f, 0f); // Editable spawn point
    public Vector3 blastEndDestination = new Vector3(0f, 0f, 10f);

    [Header("Block Stats")]
    public int blockManaCost = 20;
    public int blockUltGain = 10;
    public float blockDuration = 5f;
     public GameObject blockPrefab; // Reference to your blast prefab
    public Vector3 blockSpawnPoint = new Vector3(0f, 0.5f, 0f); // Editable spawn point
    

     [Header("Heal Stats")]
    public int healManaCost = 20;

    public int healHealthPointsGain = 15;
    public int healUltGain = 10;
     public float healDuration = 2f;
     public GameObject healPrefab; // Reference to your blast prefab
    public Vector3 healSpawnPoint = new Vector3(0f, 0.5f, 0f); // Editable spawn point

    float timer = 0f;
    float spellInterval = 3f;

    [Header("Fireball Stats")]
    public int fireballUltCost = 90;
    public int fireballDamage = 15;
    public float fireballDuration = 6f;
    public GameObject fireballPrefab;
    public Vector3 fireballSpawnPoint = new Vector3(0f, 0.5f, 0f);
    public Vector3 fireballEndDestination = new Vector3(0f, 0f, 10f);
    public float fireballSpeed = 12.0f;

        void Update()
        {
        timer += Time.deltaTime;
        if (timer >= spellInterval)
        {
            AIAction();
            timer = 2f;
        }
        }


   public void AIAction()
{
    int randomSpell = Random.Range(0, 4); // Generate a random number between 0 and 3
    switch (randomSpell)
    {
        case 0:
            CreateBlast();
            break;
        case 1:
            CreateBlock();
            break;
        case 2:
            CreateHeal();
            break;
        case 3:
            CreateFireball();
            break;
        default:
            break;
    }
}


public void CreateBlast()
    {
        if (enemyStats.CurrentManaPoints >= blastManaCost)
        {   
            Animator animator = objectToAnimate.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("CastBlastEnemy");
            }
                // Wait for the animation to finish
        StartCoroutine(WaitForAnimationThenCreateBlast());
            
           
        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }
       
    }
   public void CreateBlock()
    {
         if (enemyStats.CurrentManaPoints >= blockManaCost)
        {   
            Animator animator = objectToAnimate.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("CastShieldEnemy");
            }
                // Wait for the animation to finish
        StartCoroutine(WaitForAnimationThenCreateShield());
            
        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }
       
    }   
   public void CreateHeal()
    {
         if (enemyStats.CurrentManaPoints >= healManaCost)
        {   
                     Animator animator = objectToAnimate.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("CastHealEnemy");
            }
                // Wait for the animation to finish
        StartCoroutine(WaitForAnimationThenCreateHeal());
            
        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }
    }
        public void CreateFireball()
        {
            if (enemyStats.CurrentUltimatePoints >= fireballUltCost)
            {
                Animator animator = objectToAnimate.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger("CastBlastEnemy");
                }
                StartCoroutine(WaitForAnimationThenCreateFireball());
            }
            else
            {
                spellOutput = "Not enough mana"; 
            }
        }


private IEnumerator WaitForAnimationThenCreateBlast()
{
    // Wait for the animation to finish
    yield return new WaitForSeconds(2f);

    GameObject blast = Instantiate(blastPrefab, blastSpawnPoint, Quaternion.identity);
    Rigidbody rb = blast.GetComponent<Rigidbody>();
    blast.tag = "EnemyBlast";

    // Calculate the direction towards the end destination
    Vector3 direction = (blastEndDestination - blastSpawnPoint).normalized;
    rb.velocity = direction * projectileSpeed; // Set projectile speed
    spellOutput = "Blast";
    PlaySound(blastClip);
    enemyStats.CurrentManaPoints -= blastManaCost;

    // Schedule the destruction of the block after 5 seconds
    Destroy(blast, blastDuration);

    // Add ult
    enemyStats.CurrentUltimatePoints += blastUltGain;
}
private IEnumerator WaitForAnimationThenCreateShield()
{
    // Wait for the animation to finish
    yield return new WaitForSeconds(1.5f);
            GameObject block = Instantiate(blockPrefab, blockSpawnPoint, Quaternion.identity);
            Rigidbody rb = block.GetComponent<Rigidbody>();
            spellOutput = "Block";
            
            enemyStats.CurrentManaPoints -= blockManaCost;
            PlaySound(blockClip);
             // Schedule the destruction of the block after 5 seconds
             Destroy(block, blockDuration);
             //Add ult
             enemyStats.CurrentUltimatePoints += blockUltGain;    
}
private IEnumerator WaitForAnimationThenCreateHeal()
{
    // Wait for the animation to finish
    yield return new WaitForSeconds(3f);
                GameObject heal = Instantiate(healPrefab, healSpawnPoint, Quaternion.identity);
            Rigidbody rb = heal.GetComponent<Rigidbody>();

            spellOutput = "Heal";
            enemyStats.CurrentManaPoints -= healManaCost; 
            PlaySound(healClip);
            //Heals the player
           enemyStats.CurrentHealthPoints += healHealthPointsGain;

           Destroy(heal, healDuration);
             //Add ult
             enemyStats.CurrentUltimatePoints += healUltGain;  
}
private IEnumerator WaitForAnimationThenCreateFireball()
{
    yield return new WaitForSeconds(2f);

    GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint, Quaternion.identity);
    Rigidbody rb = fireball.GetComponent<Rigidbody>();
    fireball.tag = "EnemyFireball";

    Vector3 direction = (fireballEndDestination - fireballSpawnPoint).normalized;
    rb.velocity = direction * fireballSpeed;
    spellOutput = "Fireball";
    PlaySound(fireballClip); // Assuming you have a fireball sound clip defined

    enemyStats.CurrentUltimatePoints -= fireballUltCost;

    Destroy(fireball, fireballDuration);
}

    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
