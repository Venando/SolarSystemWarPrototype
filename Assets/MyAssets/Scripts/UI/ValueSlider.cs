using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueSlider : MonoBehaviour
{
    public Image BrightImage;
    public Image DimImage;

    public ISliderValueProvider m_ValueProvider;
    public GameObject m_ProviderGameObject;
    private Slider m_Slider;

    protected virtual void Awake()
    {
        m_Slider = GetComponent<Slider>();
    }

    protected virtual void Update()
    {
        if (m_ProviderGameObject == null)
        {
            if (gameObject != null)
                Destroy(gameObject);
            return;
        }

        m_Slider.value = m_ValueProvider.GetSliderFillPercent();
    }

    public void Attach(ISliderValueProvider valueProvider)
    {
        m_ValueProvider = valueProvider;
        m_ProviderGameObject = (valueProvider as Component).gameObject;
    }

    public void SetTintColor(Color tintColor)
    {
        Color.RGBToHSV(tintColor, out var H, out var S, out var V);

        BrightImage.color = Color.HSVToRGB(H, S / 2f, V);
        DimImage.color = Color.HSVToRGB(H, S / 20f, V);
    }
}
