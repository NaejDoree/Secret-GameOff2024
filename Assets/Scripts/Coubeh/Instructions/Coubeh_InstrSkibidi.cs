using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Coubeh_InstrSkibidi : CoubehInstruction
{
    public override void Run(CoubehRunner.CoubehRunnerInstance coubehRunnerInstance, List<string> parameters)
    {
        var executedParameters = coubehRunnerInstance.ExecuteOperators(parameters, true);
        var concatenatedParameters = string.Join(" ", executedParameters);

        coubehRunnerInstance._lines.Add(concatenatedParameters);
    }
}