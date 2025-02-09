using System;
using System.Collections.Generic;
using UnityEngine;

public class Coubeh_InstrSquidgame : CoubehInstruction
{
    public override void Run(CoubehRunner.CoubehRunnerInstance coubehRunnerInstance, List<string> parameters)
    {
        coubehRunnerInstance.ClearOutput();
    }
}