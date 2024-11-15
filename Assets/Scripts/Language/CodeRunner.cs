using System;
using System.Collections.Generic;
using UnityEngine;

public class CodeRunner : MonoBehaviour
{
    private string[] _lines = new []{""};
    public int InstructionPointer;

    public Dictionary<string, int> Memory = new();
    public Dictionary<string, int> Flags = new();
    private Dictionary<string, Instruction> _instructions = new Dictionary<string, Instruction>()
    {
        //Math
        {"ADD", new InstrAdd()},
        {"SUB", new InstrSubstract()},
        {"STORE", new InstrStore()},
        {"COMP", new InstrComparison()},

        //Code flow
        {"JUMP", new InstrJump()},
        {"FLAG", new InstrSetFlag()},
        {"GOTO", new InstrGoto()},
        
        //Gameplay
        //{"DRAW", new InstrDrawCard()},
        //{"SHUFFLE", new InstrShuffle()},
        //{"WIN", new InstrWin()},
        //{"LOOSE", new InstrLoose()},
    };

    public void SetInstructions(string code)
    {
        InstructionPointer = 0;
        _lines = code.Split("\n");
    }

    public int Step()
    {
        if (InstructionPointer < _lines.Length)
        {
            ExecuteInstruction(_lines[InstructionPointer]);
            InstructionPointer++;
        }
        
        if (InstructionPointer >= _lines.Length)
        {
            // warn end of exec
            InstructionPointer = -1;
        }

        return InstructionPointer;
    }

    public void ExecuteInstruction(string line)
    {
        var words = line.Split(" ");
        var param1 = 0;
        var param2 = 0;
        
        if (words.Length >= 2)
        {
            param1 = ParseParam(words[1]);
        }
        if (words.Length >= 3)
        {
            param2 = ParseParam(words[2]);
        }

        if (words.Length > 1)
        {
            ExecuteInstruction(words[0], param1, param2);
        }
    }

    public int ParseParam(string param)
    {
        if (param[0] == '$')
        {
            return Memory.GetValueOrDefault(param);
        }
        else
        {
            return Int32.Parse(param);
        }
    }
    
    public void ExecuteInstruction(string instructionName, int param1, int param2)
    {
        if (_instructions.ContainsKey(instructionName))
        {
            _instructions[instructionName].Run(this, param1, param2);
        }
    }
}
