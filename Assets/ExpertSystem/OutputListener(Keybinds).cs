using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputListenerKeybinds : MonoBehaviour, ISpells
{
    public Char_Stats charStats;
    public List<MonoBehaviour> keybindsScripts = new List<MonoBehaviour>();
    public List<string> outputHistory = new List<string>(); // List to store the output history

    public string spellOutput = "";
    private float lastOutputTime;

    private bool isCooldownActive = false;


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
    public GameObject healPrefab;
    public Vector3 healSpawnPoint = new Vector3(0f, 0.5f, 0f); // Editable spawn point


    [Header("Counter Stats")]
    public int counterManaCost = 20;
    public int counterDamage = 15;
    public int counterUltGain = 10;
    public float counterDuration = 5f;
    public GameObject counterPrefab;
    public Vector3 counterSpawnPoint = new Vector3(0f, 0.5f, 0f);

    public float counterProjectileSpeed = 10.0f;
    public GameObject counterblastPrefab;
    public Vector3 counterblastSpawnPoint = new Vector3(0f, 0.5f, 0f);
    public Vector3 counterblastEndDestination = new Vector3(0f, 0f, 10f);

    [Header("Bolt Stats")]
    public int boltManaCost = 20;

    public int boltDamage = 15;
    public int boltUltGain = 10;

    public double boltatkGainPercentage = 15.0;
    public float boltDuration = 5f;
    public float boltprojectileSpeed = 10.0f; // Editable speed in Unity Editor
    public GameObject boltPrefab; // Reference to your bolt prefab
    public Vector3 boltSpawnPoint = new Vector3(0f, 0.5f, 0f); // Editable spawn point
    public Vector3 boltEndDestination = new Vector3(0f, 0f, 10f);

    [Header("Thorns Stats")]
    public int thornsManaCost = 20;
    public int thornsDamage = 15;
    public int thornsUltGain = 10;

    public int thornsDuration = 10;
    public GameObject thornsPrefab;
    public Vector3 thornsSpawnPoint = new Vector3(0f, 0.5f, 0f);


    [Header("Weaken Stats")]
    public int weakenManaCost = 20;
    public double weakenDamageReductionPercentage = 15.0;
    public int weakenUltGain = 10;
    public int weakenDuration = 10;
    public GameObject weakenPrefab;
    public Vector3 weakenSpawnPoint = new Vector3(0f, 0.5f, 0f);

    [Header("ATKup Stats")]
    public int atkupManaCost = 20;
    public int atkupUltGain = 10;
    public double atkGainPercentage = 15.0;
    public float atkupDuration = 2f;
    public GameObject atkupPrefab;
    public Vector3 atkupSpawnPoint = new Vector3(0f, 0.5f, 0f); // Editable spawn point


    [Header("DEFup Stats")]
    public int defupManaCost = 20;
    public int defupUltGain = 10;
    public float defupDuration = 5f;
    public GameObject defupPrefab;
    public Vector3 defupSpawnPoint = new Vector3(0f, 0.5f, 0f); // Editable spawn point

    [Header("Fireball Stats")]
    public int fireballUltCost = 90;
    public int fireballDamage = 50;
    public float fireballDuration = 5f;
    public float fireballprojectileSpeed = 10.0f; // Editable speed in Unity Editor
    public GameObject fireballPrefab; // Reference to your blast prefab
    public Vector3 fireballSpawnPoint = new Vector3(0f, 0.5f, 0f); // Editable spawn point
    public Vector3 fireballEndDestination = new Vector3(0f, 0f, 10f);

    //for atkup
    private bool isATKupActive = false;

    //for bolt
    private bool isBoltATKupActive = false;


    public string GetSpellOutput()
    {
        return spellOutput;
    }
    void Update()
    {
        // Loop through each keybinds script
        foreach (IKeybind keybindsScript in keybindsScripts)
        {
            if (keybindsScript is IKeybind)
            {
                // Get the output from the current keybinds script
                string currentOutput = ((IKeybind)keybindsScript).GetOutput();

                if (currentOutput == "Activate")
                {
                    outputHistory.Clear();
                }

                // If the current output is not empty and is not already in the history, add it to the history
                if (!string.IsNullOrEmpty(currentOutput) && !outputHistory.Contains(currentOutput))
                {

                    outputHistory.Add(currentOutput);
                    lastOutputTime = Time.time; // Update the time of the last added output

                }
            }
        }
        // Check if 5 seconds have passed without new items being added to outputHistory
        if (Time.time - lastOutputTime >= 5.0f)
        {
            outputHistory.Clear();
        }


        // Check for specific sequences in the output history
        if (outputHistory.Count > 0 && outputHistory[outputHistory.Count - 1] == "Launch")
        {
            if (outputHistory[0] == "Activate" && outputHistory[1] == "Offense" && outputHistory[2] == "Launch")
            {
                // Create Blast 
                CreateBlast();
                StartCooldown();
            }
            else if (outputHistory[0] == "Activate" && outputHistory[1] == "Defense" && outputHistory[2] == "Launch")
            {
                // Create Block  
                CreateBlock();
                StartCooldown();
            }
            else if (outputHistory[0] == "Activate" && outputHistory[1] == "Support" && outputHistory[2] == "Launch")
            {
                // Create Heal 
                CreateHeal();
                StartCooldown();
            }
            else if (outputHistory[0] == "Activate" && outputHistory[1] == "Offense" && outputHistory[2] == "Defense" && outputHistory[3] == "Launch")
            {
                // Create Counter 
                CreateCounter();
                StartCooldown();
            }
            else if (outputHistory[0] == "Activate" && outputHistory[1] == "Offense" && outputHistory[2] == "Support" && outputHistory[3] == "Launch")
            {
                // Create Bolt   
                CreateBolt();
                StartCooldown();
            }
            else if (outputHistory[0] == "Activate" && outputHistory[1] == "Defense" && outputHistory[2] == "Offense" && outputHistory[3] == "Launch")
            {
                // Create Thorns  
                CreateThorns();
                StartCooldown();
            }
            else if (outputHistory[0] == "Activate" && outputHistory[1] == "Defense" && outputHistory[2] == "Support" && outputHistory[3] == "Launch")
            {
                // Create Weaken 
                CreateWeaken();
                StartCooldown();
            }
            else if (outputHistory[0] == "Activate" && outputHistory[1] == "Support" && outputHistory[2] == "Offense" && outputHistory[3] == "Launch")
            {
                // Create ATKup   
                CreateATKup();
                StartCooldown();
            }
            else if (outputHistory[0] == "Activate" && outputHistory[1] == "Support" && outputHistory[2] == "Defense" && outputHistory[3] == "Launch")
            {
                // Create DEFup  
                CreateDEFup();
                StartCooldown();
            }
            else if (outputHistory[0] == "Activate" && outputHistory[1] == "Offense" && outputHistory[2] == "Defense" && outputHistory[3] == "Support" && outputHistory[4] == "Launch")
            {
                // Create Fireball  
                CreateFireball();
                StartCooldown();
            }
        }

    }

    // Implement methods to produce spells
    void CreateBlast()
    {
        if (charStats.CurrentManaPoints >= blastManaCost)
        {
            blastDamage = CalculateDamageGain(blastDamage);
            GameObject blast = Instantiate(blastPrefab, blastSpawnPoint, Quaternion.identity);
            Rigidbody rb = blast.GetComponent<Rigidbody>();
            blast.tag = "PlayerBlast";

            // Calculate the direction towards the end destination
            Vector3 direction = (blastEndDestination - blastSpawnPoint).normalized;
            rb.velocity = direction * projectileSpeed; // Set projectile speed

            spellOutput = "Blast";
            charStats.CurrentManaPoints -= blastManaCost;

            // Schedule the destruction of the block after 5 seconds
            Destroy(blast, blastDuration);

            //Add ult
            charStats.CurrentUltimatePoints += blastUltGain;
        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }

    }
    void CreateBlock()
    {
        if (charStats.CurrentManaPoints >= blockManaCost)
        {
            GameObject block = Instantiate(blockPrefab, blockSpawnPoint, Quaternion.identity);
            spellOutput = "Block";
            charStats.CurrentManaPoints -= blockManaCost;
            // Schedule the destruction of the block after 5 seconds
            Destroy(block, blockDuration);
            //Add ult
            charStats.CurrentUltimatePoints += blockUltGain;
        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }

    }
    void CreateHeal()
    {
        if (charStats.CurrentManaPoints >= healManaCost)
        {
            GameObject heal = Instantiate(healPrefab, healSpawnPoint, Quaternion.identity);
            Rigidbody rb = heal.GetComponent<Rigidbody>();

            spellOutput = "Heal";
            charStats.CurrentManaPoints -= healManaCost;
            //Heals the player
            charStats.CurrentHealthPoints += healHealthPointsGain;

            Destroy(heal, healDuration);
            //Add ult
            charStats.CurrentUltimatePoints += healUltGain;
        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }
    }
    void CreateCounter()
    {
        if (charStats.CurrentManaPoints >= counterManaCost)
        {
            GameObject counter = Instantiate(counterPrefab, counterSpawnPoint, Quaternion.identity);
            spellOutput = "Counter";
            charStats.CurrentManaPoints -= counterManaCost;
            charStats.CurrentUltimatePoints += counterUltGain;

            Destroy(counter, counterDuration);

            counterDamage = CalculateDamageGain(counterDamage);
            GameObject counterBlast = Instantiate(counterblastPrefab, counterblastSpawnPoint, Quaternion.identity);
            Rigidbody rb = counterBlast.GetComponent<Rigidbody>();
            counterBlast.tag = "PlayerCounterBlast";

            // Calculate the direction towards the end destination
            Vector3 counterdirection = (counterblastEndDestination - counterblastSpawnPoint).normalized;
            rb.velocity = counterdirection * counterProjectileSpeed; // Set projectile speed
            Destroy(counterBlast, counterDuration);
        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }
    }
    void CreateBolt()
    {
        if (charStats.CurrentManaPoints >= boltManaCost)
        {
            boltDamage = CalculateDamageGain(boltDamage);
            isBoltATKupActive = true;
            StartCoroutine(DeactivateBoltATKup());
            GameObject bolt = Instantiate(boltPrefab, boltSpawnPoint, Quaternion.identity);
            Rigidbody rb = bolt.GetComponent<Rigidbody>();
            bolt.tag = "PlayerBolt";

            // Calculate the direction towards the end destination
            Vector3 direction = (boltEndDestination - boltSpawnPoint).normalized;
            rb.velocity = direction * boltprojectileSpeed; // Set projectile speed

            spellOutput = "Bolt";
            charStats.CurrentManaPoints -= boltManaCost;

            // Schedule the destruction of the block after 5 seconds
            Destroy(bolt, boltDuration);

            //Add ult
            charStats.CurrentUltimatePoints += boltUltGain;

        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }
    }
    void CreateThorns()
    {
        if (charStats.CurrentManaPoints >= thornsManaCost)
        {
            thornsDamage = CalculateDamageGain(thornsDamage);
            spellOutput = "Thorns";
            charStats.CurrentManaPoints -= thornsManaCost;
            charStats.thornsExpired = false;
            charStats.thornsPrefab = thornsPrefab;
            charStats.thornsSpawnPoint = thornsSpawnPoint;

            StartCoroutine(ThornsSpellDuration());
        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }
    }
    void CreateWeaken()
    {
        if (charStats.CurrentManaPoints >= weakenManaCost)
        {
            GameObject weaken = Instantiate(weakenPrefab, weakenSpawnPoint, Quaternion.identity);
            Rigidbody rb = weaken.GetComponent<Rigidbody>();

            spellOutput = "Weaken";
            charStats.CurrentManaPoints -= weakenManaCost;
            charStats.weakenExpired = false;
            charStats.weakenDamageReduction = weakenDamageReductionPercentage;

            StartCoroutine(WeakenSpellDuration());

            Destroy(weaken, weakenDuration);
            //Add ult
            charStats.CurrentUltimatePoints += weakenUltGain;

        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }
    }
    void CreateATKup()
    {
        if (charStats.CurrentManaPoints >= atkupManaCost)
        {
            GameObject ATKup = Instantiate(atkupPrefab, atkupSpawnPoint, Quaternion.identity);
            spellOutput = "ATKup";
            charStats.CurrentManaPoints -= atkupManaCost;
            Destroy(ATKup, atkupDuration);
            //Add ult
            charStats.CurrentUltimatePoints += atkupUltGain;
            isATKupActive = true; // Set ATKup as active
            StartCoroutine(DeactivateATKup()); // Start coroutine to deactivate ATKup after atkupDuration

        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }
    }
    void CreateDEFup()
    {
        if (charStats.CurrentManaPoints >= defupManaCost)
        {
            GameObject DEFup = Instantiate(defupPrefab, defupSpawnPoint, Quaternion.identity);
            spellOutput = "DEFup";
            charStats.CurrentManaPoints -= defupManaCost;

            charStats.CurrentDefPoints += 30;

            Destroy(DEFup, 1f);

        }
        else
        {
            spellOutput = "Not enough mana"; // Set output if mana is not enough
        }
    }
    void CreateFireball()
    {
        if (charStats.CurrentUltimatePoints >= fireballUltCost)
        {
            fireballDamage = CalculateDamageGain(fireballDamage);
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint, Quaternion.identity);
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            fireball.tag = "PlayerFireball";

            // Calculate the direction towards the end destination
            Vector3 direction = (fireballEndDestination - fireballSpawnPoint).normalized;
            rb.velocity = direction * fireballprojectileSpeed; // Set projectile speed

            fireballDamage = CalculateDamageGain(fireballDamage);
            spellOutput = "Fireball";
            charStats.CurrentUltimatePoints -= fireballUltCost;
            Destroy(fireball, fireballDuration);

        }
        else
        {
            spellOutput = "Not enough Ultimate Points"; // Set output if Ultimate Points is not enough
        }
    }
    private void StartCooldown()
    {
        isCooldownActive = true;
        outputHistory.Clear();
        StartCoroutine(EndCooldown());
    }

    private IEnumerator EndCooldown()
    {
        yield return new WaitForSeconds(1.0f); // Wait for 1 second
        isCooldownActive = false;
    }
    // Coroutine to deactivate ATKup after atkupDuration
    IEnumerator DeactivateATKup()
    {
        yield return new WaitForSeconds(atkupDuration);
        isATKupActive = false;
    }
    IEnumerator DeactivateBoltATKup()
    {
        yield return new WaitForSeconds(boltDuration);
        isBoltATKupActive = false;
    }

    IEnumerator WeakenSpellDuration()
    {
        yield return new WaitForSeconds(weakenDuration);
        charStats.weakenExpired = true;

    }

    IEnumerator ThornsSpellDuration()
    {
        yield return new WaitForSeconds(thornsDuration);
        charStats.thornsExpired = true;

    }

    // Modify boltDamage calculation
    int CalculateDamageGain(int baseDamage)
    {

        if (isATKupActive)
        {
            baseDamage = (int)(baseDamage * (1 + (atkGainPercentage / 100)));
        }
        else if (isBoltATKupActive)
        {
            baseDamage = (int)(baseDamage * (1 + (boltatkGainPercentage / 100)));
        }
        return baseDamage;
    }

    public interface IKeybind
    {
        string GetOutput();
    }
}

