using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
public class SpellHistory : MonoBehaviour
{
    public List<MonoBehaviour> ExpertSystemScript = new List<MonoBehaviour>();
    public List<string> spellHistory = new List<string>();
    private string lastAddedSpell = "";
    public AudioClip blastClip;
    public AudioClip healClip;
    public AudioClip blockClip;
    public AudioClip counterClip;
    public AudioClip boltClip;
    public AudioClip thornsClip;
    public AudioClip weakenClip;
    public AudioClip ATKupClip;
    public AudioClip DEFupClip;
    public AudioClip fireballClip;
    public AudioSource audioSource;

    void Update()
    {
        foreach (MonoBehaviour expertScript in ExpertSystemScript)
        {
            if (expertScript is ISpells)
            {
                string currentOutput = ((ISpells)expertScript).GetSpellOutput();

                if (currentOutput == "Activate")
                {
                    spellHistory.Clear();
                }
                else if (!string.IsNullOrEmpty(currentOutput) && currentOutput != lastAddedSpell)
                {
                    spellHistory.Add(currentOutput);
                    lastAddedSpell = currentOutput;
                    currentOutput = RemoveBracketedText(currentOutput);
                    if (currentOutput == "Blast")
                    {
                        PlaySound(blastClip);
                    }
                    else if (currentOutput == "Heal")
                    {
                        PlaySound(healClip);
                    }
                    else if (currentOutput == "Block")
                    {
                        PlaySound(blockClip);
                    }
                    
                    else if (currentOutput == "Counter")
                    {
                        PlaySound(counterClip);
                    }
                    else if (currentOutput == "Bolt")
                    {
                        PlaySound(boltClip);
                    }
                    else if (currentOutput == "Weaken")
                    {
                        PlaySound(weakenClip);
                    }
                    else if (currentOutput == "ATKup")
                    {
                        PlaySound(ATKupClip);
                    }
                    else if (currentOutput == "DEFup")
                    {
                        PlaySound(DEFupClip);
                    }
                    else if (currentOutput == "Fireball")
                    {
                        PlaySound(fireballClip);
                    }
                    
                }
            }
        } 
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
     static string RemoveBracketedText(string input)
    {
        // Define the pattern to match text inside square brackets
        string pattern = @"\[.*?\]";

        // Use Regex.Replace to remove the matched text
        string result = Regex.Replace(input, pattern, "");

        return result;
    }
}
public interface ISpells
{
    string GetSpellOutput();
}
