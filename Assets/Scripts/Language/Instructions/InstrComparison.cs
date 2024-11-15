using System;
using UnityEngine;

public class InstrComparison : Instruction
{
    public override void Run(CodeRunner context, int param1, int param2)
    {
        context.Memory.SetOrCreate("$0", param1.CompareTo(param2));
    }
}
