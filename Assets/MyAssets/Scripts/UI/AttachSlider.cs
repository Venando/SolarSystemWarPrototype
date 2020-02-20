using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AttachSlider : ValueSlider
{
    private Renderer m_AttachedRenderer;
    private Camera m_MainCamera;
    private RectTransform m_RectTransform;
    private Canvas m_Canvas;
    private float m_Offset;

    protected override void Awake()
    {
        base.Awake();
        m_MainCamera = Camera.main;
        m_RectTransform = transform as RectTransform;
        m_Canvas = GetComponentInParent<Canvas>();
    }

    protected override void Update()
    {
        base.Update();

        if (m_AttachedRenderer == null || m_ValueProvider == null)
        {
            if (gameObject != null)
                Destroy(gameObject);
            return;
        }

        var attachWorldPosition = m_AttachedRenderer.transform.position - Vector3.back * m_AttachedRenderer.bounds.extents.y;
        var viewportPosition = m_MainCamera.WorldToViewportPoint(attachWorldPosition);
        var canvasRect = (m_Canvas.transform as RectTransform);
        var proportionalPosition = new Vector2(viewportPosition.x * canvasRect.sizeDelta.x, viewportPosition.y * canvasRect.sizeDelta.y);

        m_RectTransform.localPosition = proportionalPosition - canvasRect.sizeDelta / 2 + Vector2.up * m_Offset;
    }

    public void Attach(Renderer renderer, ISliderValueProvider valueProvider, float offset)
    {
        m_AttachedRenderer = renderer;
        m_Offset = offset;

        Attach(valueProvider);
    }
}
