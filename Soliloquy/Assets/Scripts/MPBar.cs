using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI MPText;
    [SerializeField] SharedMP sharedMP;

    void OnEnable()
    {
        sharedMP.OnMPChanged += setMP;
        setMP(sharedMP.currentMP, sharedMP.maxMP);
    }

    void OnDisable()
    {
        sharedMP.OnMPChanged -= setMP;
    }

    public void setMP(int current, int max)
    {
        //slider.transform.localScale = new Vector2(max * 4, slider.transform.localScale.y);
        slider.maxValue = max;
        slider.value = current;
    }


    // public bool useMP(int spellMP)
    // {
    //     if (spellMP > currentMP) return false;

    //     currentMP -= spellMP;
    //     MPText.text = "MP: " + currentMP;
    //     return true;
    // }

}
