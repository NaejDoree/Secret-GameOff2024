using System.Collections.Generic;
using UnityEngine;

public class Coubeh_InstrAllo : CoubehInstruction
{
    public override void Run(CoubehRunner.CoubehRunnerInstance coubehRunnerInstance, List<string> parameters)
    {
        var executedParameters = coubehRunnerInstance.ExecuteOperators(parameters);
        string text = string.Join(" ", executedParameters);
        coubehRunnerInstance.PrintToOutput(text);
    }
}
