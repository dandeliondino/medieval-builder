using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderLabel : MonoBehaviour
{
    public Slider slider;

    private TextMeshProUGUI tmp;
    private string originalText;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TMPro.TextMeshProUGUI>();
        originalText = tmp.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateNumber()
    {
        tmp.text = originalText + " (" + (int)slider.value + ")";
    }
}
