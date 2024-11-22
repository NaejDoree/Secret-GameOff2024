using UnityEngine;

public class InstrUnlock : Instruction
{
    public override void Run(CodeRunner context, int param1, int param2)
    {
        context.Unlock();
    }
}
