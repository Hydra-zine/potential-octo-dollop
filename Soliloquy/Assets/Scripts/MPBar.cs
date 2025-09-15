using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI MPText;
    private int maxMP = 50;
    private int currentMP;
    void Start()
    {
        slider.value = 50;
        currentMP = maxMP;
    }

    public void setMP(int mp)
    {
        slider.value = mp;
        currentMP = mp;
    }


    public bool useMP(int spellMP)
    {
        if (spellMP > currentMP) return false;

        currentMP -= spellMP;
        MPText.text = "MP: " + currentMP;
        return true;
    }

}
