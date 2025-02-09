using System.Collections.Generic;
using UnityEngine;

public class Coubeh_InstrQuoi : CoubehInstruction
{
    public override void Run(CoubehRunner.CoubehRunnerInstance coubehRunnerInstance, List<string> parameters)
    {
        var executedParameters = coubehRunnerInstance.ExecuteOperators(parameters, false);
        var concatenatedParameters = string.Join(" ", executedParameters);

        coubehRunnerInstance.Flags.SetOrCreate(concatenatedParameters, coubehRunnerInstance.InstructionPointer);
    }
}