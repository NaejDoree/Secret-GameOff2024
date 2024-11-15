using UnityEngine;

public class InstrSetFlag : Instruction
{
    public override void Run(CodeRunner context, int param1, int param2)
    {
        context.Flags.SetOrCreate("$" + param1, context.InstructionPointer);
    }
}
