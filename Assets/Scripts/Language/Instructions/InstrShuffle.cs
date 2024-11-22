using UnityEngine;

public class InstrShuffle : Instruction
{
    public override void Run(CodeRunner context, int param1, int param2)
    {
        context.Suffle();
    }
}
