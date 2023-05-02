using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScaleAndRotateSliders : MonoBehaviour
{
    private Slider scaleSlider;
    private Slider rotateSlider;

    public float scaleMinValue;
    public float scaleMaxValue;

    public float rotateMinValue;
    public float rotateMaxValue;

    Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
        scaleSlider = GameObject.Find("ScaleSlider").GetComponent<Slider>();
        scaleSlider.minValue = scaleMinValue;
        scaleSlider.maxValue = scaleMaxValue;
        scaleSlider.onValueChanged.AddListener(ScaleSliderUpdate);

        rotateSlider = GameObject.Find("RotateSlider").GetComponent<Slider>();
        rotateSlider.minValue = rotateMinValue;
        rotateSlider.maxValue = rotateMaxValue;
        rotateSlider.onValueChanged.AddListener(RotateSliderUpdate);
    }

    void ScaleSliderUpdate(float value)
    {
        transform.localScale = new Vector3(initialScale.x + value, initialScale.y + value, initialScale.z + value);
    }
    void RotateSliderUpdate(float value)
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, value, transform.rotation.z);
    }
}
