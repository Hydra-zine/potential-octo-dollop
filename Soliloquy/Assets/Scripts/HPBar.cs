using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI hpText;
    public void UpdateHP(int current, int max)
    {
        slider.maxValue = max;
        slider.value = current;
        hpText.text = $"HP: {current}/{max}";
    }
}
