using UnityEngine;

public class InstrAdd : Instruction
{
    public override void Run(CodeRunner context, int param1, int param2)
    {
        context.Memory.SetOrCreate("$0", param1 + param2);
    }
}
