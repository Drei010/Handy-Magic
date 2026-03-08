using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    public Enemy_Stats enemyStats;
    public Slider EnemyHealthSlider;
     public Slider EnemymanaSlider;
    public Slider EnemyultimateSlider;

    void Update()
    {
        // Update sliders with current values from character stats
        EnemyHealthSlider.value = enemyStats.CurrentHealthPoints;
        EnemymanaSlider.value = enemyStats.CurrentManaPoints;
        EnemyultimateSlider.value = enemyStats.CurrentUltimatePoints;

    }
}
