using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Char_Stats characterStats;
    public Slider healthSlider;
     public Slider DefupHealthSlider;
    public Slider manaSlider;
    public Slider ultimateSlider;

    void Update()
    {
         DefupHealthSlider.value = characterStats.CurrentDefPoints;
        if (characterStats.CurrentDefPoints == 0){
        DefupHealthSlider.gameObject.SetActive(false);
        }
        else{
            DefupHealthSlider.gameObject.SetActive(true);
        }
        // Update sliders with current values from character stats
        healthSlider.value = characterStats.CurrentHealthPoints;
        manaSlider.value = characterStats.CurrentManaPoints;
        ultimateSlider.value = characterStats.CurrentUltimatePoints;
    }
}
