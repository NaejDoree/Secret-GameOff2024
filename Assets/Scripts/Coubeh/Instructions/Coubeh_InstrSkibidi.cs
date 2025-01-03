using System.Collections.Generic;
using UnityEngine;

public class Coubeh_InstrSkibidi : CoubehInstruction
{
    public override void Run(CoubehRunner.CoubehRunnerInstance coubehRunnerInstance, List<string> parameters)
    {
        var concatenatedParameters = string.Join(" ", parameters);
        coubehRunnerInstance._lines.Add(concatenatedParameters);
    }
}