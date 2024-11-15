using UnityEngine;

public class InstrGoto : Instruction
{
    public override void Run(CodeRunner context, int param1, int param2)
    {
        if (context.Flags.ContainsKey("$" + param1))
        {
            context.InstructionPointer = context.Flags["$" + param1];
        }
    }
}
