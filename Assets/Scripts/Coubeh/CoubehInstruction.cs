using System.Collections.Generic;
using UnityEngine;

public abstract class CoubehInstruction
{
    public abstract void Run(CoubehRunner.CoubehRunnerInstance context, List<string> parameters);
}
