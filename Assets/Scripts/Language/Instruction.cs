using UnityEngine;

public abstract class Instruction
{
    public abstract void Run(CodeRunner context, int param1, int param2);
}
