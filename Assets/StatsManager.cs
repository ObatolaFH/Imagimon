using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ImagimonTheGame
{
public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    public Slider attackSlider;
    public Slider defenseSlider;
    public Slider initSlider;
    public Slider specialAttackSlider;
    public Slider specialDefenseSlider;

    private List<Slider> sliders;
    private SaveManager saveManager;


    public List<int> stats = new List<int>();

    private const int maxPoints = 100;
    private int totalPoints;

    private Dictionary<Slider, int> previousSliderValues;

      void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sliders = new List<Slider> { attackSlider, defenseSlider, initSlider, specialAttackSlider, specialDefenseSlider };
        totalPoints = 0;

        previousSliderValues = new Dictionary<Slider, int>();

        foreach (Slider slider in sliders)
        {
            slider.value = 0;
            slider.maxValue = maxPoints;
            previousSliderValues[slider] = 0;  // Initialize previous values to 0
            slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(slider); });
        }

        stats = new List<int> {0, 0, 0, 0, 0};

    }

    void OnSliderValueChanged(Slider changedSlider)
    {
        int newValue = (int)changedSlider.value;
        int previousValue = previousSliderValues[changedSlider];

        int delta = newValue - previousValue;
        int projectedTotalPoints = totalPoints + delta;

        if (projectedTotalPoints <= maxPoints)
        {
            totalPoints = projectedTotalPoints;
            previousSliderValues[changedSlider] = newValue;

            int sliderIndex = sliders.IndexOf(changedSlider);
            if (sliderIndex >= 0 && sliderIndex < stats.Count)
            {
                stats[sliderIndex] = newValue;
            }

            Debug.Log("Used Points: " + totalPoints);
            Debug.Log("Leftover Points: " + (maxPoints - totalPoints));
            Debug.Log("Stats: " + string.Join(", ", stats));
        }
        else
        {
            changedSlider.value = previousValue;  // Revert to previous value
        }
    }
/*
    // In order to access the stats from another script:
    
        List<int> stats = StatsManager.Instance.stats;

        // Example usage: Print stats
        foreach (int stat in stats)
        {
            Debug.Log(stat);
        }
        */

}
}
