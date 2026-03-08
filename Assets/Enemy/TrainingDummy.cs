using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : MonoBehaviour
{
    public Enemy_Stats enemyStats; // Reference to the Enemy_Stats script

    private int previousHealth;
    public GameObject objectToAnimate;

    void Start()
    {
        if (enemyStats == null)
        {
            Debug.LogError("Enemy_Stats reference is not set in TrainingDummy script.");
            enabled = false; // Disable the script to prevent errors
            return;
        }

        previousHealth = enemyStats.CurrentHealthPoints;
    }

    void Update()
    {
        int currentHealth = enemyStats.CurrentHealthPoints;

        if (currentHealth < previousHealth)
        {
            int reductionAmount = previousHealth - currentHealth;
            
            Animator animator = objectToAnimate.GetComponent<Animator>();
            animator.SetBool("Attacked", true);
            Debug.Log("Health reduced by: " + reductionAmount);
            if (currentHealth <= 10)
            {
                // Play death animation
                animator.SetBool("Dead", true);

                // After 2 seconds, reduce the current health by 10
                StartCoroutine(ReduceHealthAfterDelay(2f, 10));
            }
            else
            {
                StartCoroutine(ResetAttackedParameter(1.5f)); // Wait 1.5 seconds before resetting "Attacked"
            }
        }

        previousHealth = currentHealth;
    }

    IEnumerator ResetAttackedParameter(float delay)
    {
        yield return new WaitForSeconds(delay);

        Animator animator = objectToAnimate.GetComponent<Animator>();
        animator.SetBool("Attacked", false);
    }

    IEnumerator ReduceHealthAfterDelay(float delay, int amount)
    {
        yield return new WaitForSeconds(delay);
        enemyStats.CurrentHealthPoints -= amount;
    }
}
