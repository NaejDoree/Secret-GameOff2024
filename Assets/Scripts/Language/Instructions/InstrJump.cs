using UnityEngine;

public class InstrJump : Instruction
{
    public override void Run(CodeRunner context, int param1, int param2)
    {
        context.InstructionPointer += param1;
    }
}
