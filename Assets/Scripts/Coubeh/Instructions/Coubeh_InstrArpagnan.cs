using System.Collections.Generic;
using UnityEngine;

public class Coubeh_InstrArpagnan : CoubehInstruction
{
    public override void Run(CoubehRunner.CoubehRunnerInstance coubehRunnerInstance, List<string> parameters)
    {
        var executedParameters = coubehRunnerInstance.ExecuteOperators(parameters, false);
        var concatenatedParameters = string.Join(" ", executedParameters);

        int lineToTest = coubehRunnerInstance.InstructionPointer + 1;
        CoubehRunner.CoubehRunnerInstance _localInstance = new CoubehRunner.CoubehRunnerInstance();
        while (lineToTest < coubehRunnerInstance._lines.Count &&
               !_localInstance.Flags.ContainsKey(concatenatedParameters))
        {
            _localInstance.Flags.Clear();
            _localInstance.Memory = new Dictionary<string, string>(coubehRunnerInstance.Memory);
            _localInstance.SetCode(coubehRunnerInstance._lines[lineToTest]);
            _localInstance.Step();
            lineToTest++;
        }

        if (lineToTest < coubehRunnerInstance._lines.Count)
        {
            coubehRunnerInstance.InstructionPointer = lineToTest-1;
        }
    }
}