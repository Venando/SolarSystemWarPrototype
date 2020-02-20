using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderSpawner : MonoBehaviour
{
    public AttachSlider AttachSliderPrefab;
    public VerticalSlider VerticalSliderPrefab;

    public static SliderSpawner Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }

    public static void SpawnAttached(Renderer attachRenderer, ISliderValueProvider valueProvider, float offset, Color tintColor)
    {
        var slider = Instantiate(Instance.AttachSliderPrefab, Instance.transform);
        slider.Attach(attachRenderer, valueProvider, offset);
        slider.SetTintColor(tintColor);
    }

    public static void SpawnVertical(ISliderValueProvider valueProvider, float offset, Color tintColor)
    {
        var slider = Instantiate(Instance.VerticalSliderPrefab, Instance.transform);

        var sliderRectTransform = slider.transform as RectTransform;

        sliderRectTransform.anchoredPosition += Vector2.right * offset;

        slider.Attach(valueProvider);
        slider.SetTintColor(tintColor);
    }
}
