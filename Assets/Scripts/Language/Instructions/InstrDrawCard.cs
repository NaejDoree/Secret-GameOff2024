using UnityEngine;

public class InstrDrawCard : Instruction
{
    public override void Run(CodeRunner context, int param1, int param2)
    {
        if (param1 < 1)
        {
            context.Draw();
        }
        else
        {
            context.Draw(param1);
        }
    }
}
