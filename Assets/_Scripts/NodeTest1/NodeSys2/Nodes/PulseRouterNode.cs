﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nodeSys2;
using Newtonsoft.Json;

public class PulseRouterNode : Node
{
    [JsonProperty] private Property value, outputCount, PulseProp;
    [JsonProperty] private List<Property> elements;

    public PulseRouterNode(ColorVec pos) : base(pos)
    {
        base.nodeDisc = "Pulse Router";
        outputCount = CreateInputProperty("Output Count", false, new EvaluableFloat(0));
        outputCount.interactable = true;
        value = CreateInputProperty("Router Value", true, new EvaluableFloat(0));
        value.interactable = true;
        elements = new List<Property>(0);
        PulseProp = CreateInputProperty("Pulse Input", true, new Pulse());
    }

    public override void Init()
    {
        base.Init();
        ProcessRes();
    }

    public override void Handle()
    {
        if (PulseProp.GetPulse().PulsePresent())
        {
            RoutePulse();
        }
    }

    private void RoutePulse()
    {
        int index = (int)value.GetEvaluable().EvaluateValue();
        if (index < 0 || index >= elements.Count) return;
        elements[index].Invoke(new Pulse());
    }

    private void ProcessRes()
    {
        int setRes = (int)outputCount.GetEvaluable().EvaluateValue();
        //if the set resoltion is different than the current one resize the list by either removing excess data
        //or adding new data
        if (elements.Count != setRes)
        {
            int diff = setRes - elements.Count;
            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                {
                    elements.Add(CreateOutputProperty("value >= " + (elements.Count + i) + " output"));
                }
            }
            else
            {
                int intialSize = elements.Count;
                for (int i = intialSize - 1; i > intialSize - 1 + diff; i--)
                {
                    if (RemoveProperty(elements[i]))
                    {
                        elements.RemoveAt(i);
                    }
                }
            }
        }
    }
}
