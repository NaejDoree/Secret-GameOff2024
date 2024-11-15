using UnityEngine;

public class InstrStore : Instruction
{
    public override void Run(CodeRunner context, int param1, int param2)
    {
        context.Memory.SetOrCreate("$" + param1, param2);
    }
}
