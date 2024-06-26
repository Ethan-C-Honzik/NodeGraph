﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluableBool : IEvaluable
{
    [JsonProperty]private bool val;

    public EvaluableBool(bool val)
    {
        this.val = val;
    }

    public void Setval(bool val)
    {
        this.val = val;
    }

    public ColorVec EvaluateColor(float vector)
    {
        return EvaluateValue(0);
    }

    public float EvaluateValue(float vector)
    {
        if (val)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public object GetCopy()
    {
        return new EvaluableBool(val);
    }

    public override string ToString()
    {
        return val.ToString();
    }

    public int GetResolution()
    {
        return 1;
    }
}
