﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ResizeBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler
{
    public RectTransform resizeTarget;
    public float sensitivity;

    public enum ResizeType {vertical, horizontal, both};
    public ResizeType resizeType;

    public Color defaultColor;
    public Color hoverColor;
    private RawImage image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
        image.color = defaultColor;
    }

    Vector2 initialSize;
    Vector2 initalCursor;
    public void OnBeginDrag(PointerEventData eventData)
    {
        initialSize = resizeTarget.sizeDelta;
        initalCursor = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        sensitivity = 1 / BackgroundScroll.zoom.x * 110;
        Vector2 PosDiff = ((Vector2)eventData.pointerCurrentRaycast.worldPosition) - initalCursor;
        switch (resizeType)
        {
            case ResizeType.vertical:
                resizeTarget.sizeDelta = new Vector2(initialSize.x, initialSize.y - (PosDiff.y * sensitivity));
                break;
            case ResizeType.horizontal:
                resizeTarget.sizeDelta = new Vector2(initialSize.x + PosDiff.x * sensitivity, initialSize.y);
                break;
            case ResizeType.both:
                resizeTarget.sizeDelta = new Vector2(initialSize.x + PosDiff.x * sensitivity, initialSize.y - (PosDiff.y * sensitivity));
                break;                
            default:
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = defaultColor;
    }
}
