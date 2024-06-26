﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpenScript : MonoBehaviour
{
    public GameObject closeAnim;
    public GameObject openAnim;

    private bool state = false;

    public void ToggleMenu()
    {
        state = !state;
        if (state)
        {
            closeAnim.SetActive(false);
            openAnim.SetActive(true);
        }
        else
        {
            closeAnim.SetActive(true);
            openAnim.SetActive(false);
        }
    }
}
