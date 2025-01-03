using System.Collections.Generic;
using UnityEngine;

public class Coubeh_InstrRizz : CoubehInstruction
{
    public override void Run(CoubehRunner.CoubehRunnerInstance coubehRunnerInstance, List<string> parameters)
    {
        if (parameters.Count >= 2)
        {
            var executedParameters = coubehRunnerInstance.ExecuteOperators(parameters);
            coubehRunnerInstance.Memory.SetOrCreate(executedParameters[0], string.Join(" ", executedParameters.GetRange(1, executedParameters.Count-1)));
        }
        else
        {
            coubehRunnerInstance.PrintToOutput("No rizz");
        }
    }
}
