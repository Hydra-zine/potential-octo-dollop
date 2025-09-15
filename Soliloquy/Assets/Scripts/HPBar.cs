using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public void UpdateHP(int current, int max)
    {
        slider.maxValue = max;
        slider.value = current;
    }
}
