﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nodeSys2;
using System;

public class EditorManager : MonoBehaviour
{
    public GameObject contextMenu;
    public Transform popupHolder;

    public GameObject boolEditor;
    public GameObject floatEditor;
    public GameObject StringEditor;
    public GameObject StringViewer;
    public GameObject ColorEditor;
    public GameObject ColorTableEditor;
    public GameObject VectorEditor;
    public GameObject EnumEditor;



    //Idea, assign a type here that is used to change which data type gets instantiated. 
    //all dependancy injections should function soley on properties and never 
    //a properties instance of data
    public void SetupEditor(Property props, Transform EditorHolder)
    {
        if (props.visible)
        {
            switch (props.currentEditor)
            {
                case EditorTypeManagement.Editor.nonEvaluable:
                    NonEvaluableSetup(props, EditorHolder);
                    break;
                case EditorTypeManagement.Editor.boolean:
                    SetupEditor(Instantiate(boolEditor, EditorHolder), props);
                    break;
                case EditorTypeManagement.Editor.number:
                    SetupEditor(Instantiate(floatEditor, EditorHolder), props);
                    break;
                case EditorTypeManagement.Editor.color:
                    SetupEditor(Instantiate(ColorEditor, EditorHolder), props);
                    break;
                case EditorTypeManagement.Editor.table:
                    SetupEditor(Instantiate(ColorTableEditor, EditorHolder), props);
                    break;
            }
            /*
            switch (props.GetData())
            {
                case EvaluableFloat num:
                    {
                        SetupEditor(Instantiate(floatEditor, EditorHolder), props);
                        break;
                    }
                case EvaluableColorVec clrVec:
                    {
                        if (clrVec.displayMode == EvaluableColorVec.DefaultDisplayMode.Color)
                        {
                            SetupEditor(Instantiate(ColorEditor, EditorHolder), props);
                        }
                        else
                        {
                            throw new NotImplementedException();
                            SetupEditor(Instantiate(floatEditor, EditorHolder), props);
                        }
                        break;
                    }
                case EvaluableColorTable table:
                    {
                        SetupEditor(Instantiate(ColorTableEditor, EditorHolder), props);
                        break;
                    }
                case EvaluableMixRGB table:
                    {
                        SetupEditor(Instantiate(ColorTableEditor, EditorHolder), props);
                        break;
                    }
                case Enum _enum:
                    {
                        SetupEditor(Instantiate(EnumEditor, EditorHolder), props);
                        break;
                    }
                case StringData str:
                    {
                        SetupEditor(Instantiate(StringEditor, EditorHolder), props);
                        break;
                    }
                case EvaluableCustomEquation _:
                    {
                        SetupEditor(Instantiate(ColorTableEditor, EditorHolder), props);
                        break;
                    }
                default:
                    {
                        SetupEditor(Instantiate(StringViewer, EditorHolder), props);
                        break;
                    }

            }
            */

        }
        else
        {
            EditorHolder.parent.gameObject.SetActive(false);
        }

    }

    private void NonEvaluableSetup(Property props, Transform EditorHolder)
    {
        switch (props.GetData())
        {
            case Enum _enum:
                {
                    SetupEditor(Instantiate(EnumEditor, EditorHolder), props);
                    break;
                }
            case StringData str:
                {
                    SetupEditor(Instantiate(StringEditor, EditorHolder), props);
                    break;
                }
            default:
                {
                    SetupEditor(Instantiate(StringViewer, EditorHolder), props);
                    break;
                }
        }
    }

    private void SetupEditor(GameObject editor, Property prop)
    {
        editor.GetComponent<GenericEditor>().SetupEditor(prop, contextMenu, popupHolder);
        RectTransform rt = editor.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(1, 1);
        rt.anchoredPosition = new Vector2(0, 0);
    }
}
