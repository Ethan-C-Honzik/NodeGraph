﻿using UnityEngine;
using UnityEngine.EventSystems;
using nodeSys2;
using UnityEngine.UI;

public class GUIPort : MonoBehaviour
     , IPointerClickHandler // 2
     , IPointerDownHandler
     , IPointerUpHandler 
     , IDragHandler
     , IEndDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler     
{    
    public Port portRef;
    public bool inputPort;
    public Color PortColor;
    public Sprite HoverSprite;
    public GameObject LinePrefab;
    private GameObject line;
    //used to store cursors position
    private GameObject cursor;
    private RectTransform ct;
    // Start is called before the first frame update
    void Start()
    {
        cursor = new GameObject();
        cursor.transform.SetParent(transform);
        //cursor.AddComponent<RawImage>();
        cursor.AddComponent<RectTransform>();
        ct = cursor.GetComponent<RectTransform>();
        ct.sizeDelta = new Vector2(10, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        //print("I was clicked at " + eventData.position);
        if (inputPort)
        {
            portRef.Disconnect();
        }
        GUIGraph.updateGraphGUI.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("dragging");
        if(line != null)
        {
            Destroy(line);
        }
        ct.position = eventData.pointerCurrentRaycast.worldPosition;
        line = GUIGraph.DrawLinesFromRect(gameObject, cursor, LinePrefab, transform);
    }    

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("pointer up");
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {            
            //check if over other port
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out GUIPort otherPort))
            {
                //if so make sure it's a different type of port
                if (otherPort.inputPort != inputPort)
                {
                    //run the connection method on the input port
                    if (inputPort)
                    {
                        if (portRef.IsConnected())
                            portRef.Disconnect();
                        portRef.Connect(otherPort.portRef);
                    }
                    else
                    {
                        if (otherPort.portRef.IsConnected())
                            otherPort.portRef.Disconnect();
                        otherPort.portRef.Connect(portRef);
                    }
                }
            }
        }
        else
        {
            Destroy(line);
        }

        GUIGraph.updateGraphGUI.Invoke();
        if(line != null)
        {
            Destroy(line);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //these need to be implemented because of a unity bug:
        //https://answers.unity.com/questions/1082179/mouse-drag-element-inside-scrollrect-throws-pointe.html
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //need to be implemented because of unity bug
    }
}