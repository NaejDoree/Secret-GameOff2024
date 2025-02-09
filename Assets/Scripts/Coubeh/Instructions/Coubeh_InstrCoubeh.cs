using System.Collections.Generic;
using UnityEngine;

public class Coubeh_InstrCoubeh : CoubehInstruction
{
    public override void Run(CoubehRunner.CoubehRunnerInstance coubehRunnerInstance, List<string> parameters)
    {
        var executedParameters = coubehRunnerInstance.ExecuteOperators(parameters, false);
        var concatenatedParameters = string.Join(" ", executedParameters);
        if (coubehRunnerInstance.Flags.ContainsKey(concatenatedParameters))
        {
            coubehRunnerInstance.InstructionPointer = coubehRunnerInstance.Flags[concatenatedParameters];
        }
    }
}