using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class CoubehRunner : MonoBehaviour
{
    public class CoubehRunnerInstance
    {
        private CoubehRunnerInstance _subInstance;
        public List<string> _lines = new();
        public int InstructionPointer;
        
        public Dictionary<string, string> Memory = new();
        public Dictionary<string, int> Flags = new();
        private Dictionary<string, CoubehInstruction> _instructions = new Dictionary<string, CoubehInstruction>()
        {
            {"rizz", new Coubeh_InstrRizz()},
            {"quoi", new Coubeh_InstrQuoi()},
            {"coubeh", new Coubeh_InstrCoubeh()},
            {"feur", new Coubeh_InstrCoubeh()},
            {"allo", new Coubeh_InstrAllo()},
            {"arpagnan", new Coubeh_InstrArpagnan()},
            {"skibidi", new Coubeh_InstrSkibidi()},
            {"squidgame", new Coubeh_InstrSquidgame()},
        };
        
        public void SetCode(string code)
        {
            InstructionPointer = 0;
            Flags.Clear();
            // sanitizing line endings
            code = code.Replace("\r" ,"");
            _lines = code.Split("\n").ToList();
        }
        
        public int Step()
        {
            if (InstructionPointer < _lines.Count)
            {
                ExecuteInstruction(_lines[InstructionPointer]);
                InstructionPointer++;
            }
        
            if (InstructionPointer >= _lines.Count)
            {
                // warn end of exec
                InstructionPointer = -1;
            }

            return InstructionPointer;
        }
        
        public void ExecuteInstruction(string line)
        {
            var words = line.Split(" ").ToList();

            int instructionIndex = FindInstructionPosition(words);

            

            if (instructionIndex >= 0)
            {
                CoubehInstruction instruction = _instructions[words[instructionIndex]];
                words.RemoveAt(instructionIndex);
                instruction.Run(this,words);
            }
        }

        public List<string> ExecuteOperators(List<string> words)
        {
            var executedWords = new List<string>();
            foreach (var word in words)
            {
                executedWords.Add(ExecuteOperators(word));
            }

            return executedWords;
        }

        public string ExecuteOperators(string word)
        {
            // First evaluate everything that is between parenthesis
            int nestingLevel = 0;
            List<int> nestStarts = new();
            List<int> nestSizes = new();
            List<string> evaluatedNests = new();

            for (int i = 0; i < word.Length; i++) {
                switch (word[i]) {
                    case '(':
                        nestingLevel++;
                        if (nestingLevel == 1)
                        {
                            nestStarts.Add(i);
                        }
                        break;
                    case ')':
                        nestingLevel--;
                        if (nestingLevel == 0)
                        {
                            nestSizes.Add(i - nestStarts.Last() - 1);
                            string nestContent = word.Substring(nestStarts.Last() + 1, nestSizes.Last());
                            evaluatedNests.Add(ExecuteOperators(nestContent));
                        }
                        else if (nestingLevel < 0)
                        {
                            return word.Substring(0, i);
                        }
                        break;
                    default:
                        break;
                }
            }

            // Replace the evaluated nests, starting from the last one
            string sigmaExpr = "sigma";
            string toiletExpr = "toilet";
            for (int i = nestStarts.Count - 1; i >= 0; i--) {
                int start = nestStarts[i];
                int size = nestSizes[i];
                string evaluatedNest = evaluatedNests[i];
                
                // Is it a toilet?
                int toiletStart = start - toiletExpr.Length;
                if (toiletStart >= 0 && word.Substring(toiletStart, toiletExpr.Length) == toiletExpr)
                {
                    // Toilets are NOT evaluated in this context
                    continue;
                }

                // Is it a sigma?
                int sigmaStart = start - sigmaExpr.Length;
                if (sigmaStart >= 0 && word.Substring(sigmaStart, sigmaExpr.Length) == sigmaExpr)
                {
                    // Evaluate the sigma
                    string sigmaExec = Memory.GetOrDefault(evaluatedNest);

                    // Replace the sigma
                    word = word.Substring(0, sigmaStart) + sigmaExec + word.Substring(start + size + 2);
                }
                else {
                    // Replace the nest
                    word = word.Substring(0, start) + evaluatedNest + word.Substring(start + size + 2);
                }
            }

            string prevWord = "";
            while (word != prevWord)
            {
                prevWord = word;
                word = ExecuteMathOperator(word);
            }

            return word;
        }
        
        public string ExecuteMathOperator(string word)
        {
            // split on all operators while keeping track of which one did the split then execute them in the right order
            List<int> operatorPositions = new List<int>();

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == '*' ||
                    word[i] == '/' ||
                    word[i] == '+' ||
                    word[i] == '-' ||
                    word[i] == '%' ||
                    word[i] == '=')
                {
                    operatorPositions.Add(i);
                }
            }

            if (operatorPositions.Count == 0)
            {
                return word;
            }

            // search for %
            for (int i = 0; i < operatorPositions.Count; i++)
            {
                var currentOperatorPos = operatorPositions[i];
                var currentOperator = word[currentOperatorPos];
                var previousOperatorPos = i - 1 >= 0 ? operatorPositions[i - 1] : 0;
                var nextOperatorPos = i + 1 < operatorPositions.Count ? operatorPositions[i + 1] : word.Length;
                
                var part1 = word.Substring(previousOperatorPos, currentOperatorPos - previousOperatorPos);
                var part2 = word.Substring(currentOperatorPos + 1, nextOperatorPos - (currentOperatorPos + 1));
                
                var before = word.Substring(0, previousOperatorPos == 0 ? 0 : previousOperatorPos+1);
                var after = word.Substring(nextOperatorPos, word.Length - nextOperatorPos);
                if (currentOperator == '%')
                {
                    var result = ApplyModulo(part1,part2);
                    return before + result + after;
                }
            }
            
            //search for * or / and apply the first one
            for (int i = 0; i < operatorPositions.Count; i++)
            {
                var currentOperatorPos = operatorPositions[i];
                var currentOperator = word[currentOperatorPos];
                var previousOperatorPos = i - 1 >= 0 ? operatorPositions[i - 1] : 0;
                var nextOperatorPos = i + 1 < operatorPositions.Count ? operatorPositions[i + 1] : word.Length;
                
                var part1 = word.Substring(previousOperatorPos, currentOperatorPos - previousOperatorPos);
                var part2 = word.Substring(currentOperatorPos + 1, nextOperatorPos - (currentOperatorPos + 1));
                
                var before = word.Substring(0, previousOperatorPos == 0 ? 0 : previousOperatorPos+1);
                var after = word.Substring(nextOperatorPos, word.Length - nextOperatorPos);
                if (currentOperator == '*')
                {
                    var result = ApplyMult(part1,part2);
                    return before + result + after;
                }
                else if (currentOperator == '/')
                {
                    var result = ApplyDivide(part1,part2);
                    return before + result + after;
                    // divide
                }
            }

            // apply +,-,=
            for (int i = 0; i < operatorPositions.Count; i++)
            {
                var currentOperatorPos = operatorPositions[i];
                var currentOperator = word[currentOperatorPos];
                var previousOperatorPos = i - 1 >= 0 ? operatorPositions[i - 1] : 0;
                var nextOperatorPos = i + 1 < operatorPositions.Count ? operatorPositions[i + 1] : word.Length;
                
                var part1 = word.Substring(previousOperatorPos, currentOperatorPos - previousOperatorPos);
                var part2 = word.Substring(currentOperatorPos + 1, nextOperatorPos - (currentOperatorPos + 1));
                
                var before = word.Substring(0, previousOperatorPos == 0 ? 0 : previousOperatorPos+1);
                var after = word.Substring(nextOperatorPos, word.Length - nextOperatorPos);
                
                if (currentOperator == '+')
                {
                    var result = ApplyAdd(part1,part2);
                    return before + result + after;
                }
                else if (currentOperator == '-')
                {
                    var result = ApplySubstract(part1,part2);
                    return before + result + after;
                } else if (currentOperator == '=')
                {
                    var result = ApplyEquals(part1,part2);
                    return before + result + after;
                }
            }
            
            return word;
        }

        private string ApplyModulo(string string1, string string2)
        {
            int? value1 = TryIntParse(string1);
            int? value2 = TryIntParse(string2);

            if (value1.HasValue && value2.HasValue)
            {
                return "" + (value1.Value % value2.Value);
            }
            else
            {
                return string1 + string2;
            }
        }

        private string ApplyMult(string string1, string string2)
        {
            int? value1 = TryIntParse(string1);
            int? value2 = TryIntParse(string2);

            if (value1.HasValue && value2.HasValue)
            {
                return "" + (value1.Value * value2.Value);
            }
            else if (value1.HasValue || value2.HasValue) {
                int repeats = value1.HasValue ? value1.Value : value2.Value;
                string baseStr = value1.HasValue ? string2 : string1;

                string result = "";
                for (int i = 0; i < repeats; i++)
                {
                    result += baseStr;
                }
                return result;
            }
            else
            {
                return string1 + string2;
            }
        }
        
        private string ApplyDivide(string string1, string string2)
        {
            int? value1 = TryIntParse(string1);
            int? value2 = TryIntParse(string2);

            if (value1.HasValue && value2.HasValue)
            {
                return "" + value1.Value / value2.Value;
            }
            else
            {
                return string1 + string2;
            }
        }
        
        private string ApplyAdd(string string1, string string2)
        {
            int? value1 = TryIntParse(string1);
            int? value2 = TryIntParse(string2);

            if (value1.HasValue && value2.HasValue)
            {
                return "" + (value1.Value + value2.Value);
            }
            else
            {
                return string1 + string2;
            }
        }
        
        private string ApplySubstract(string string1, string string2)
        {
            int? value1 = TryIntParse(string1);
            int? value2 = TryIntParse(string2);

            if (value1.HasValue && value2.HasValue)
            {
                return "" + (value1.Value - value2.Value);
            }
            else
            {
                return string1 + string2;
            }
        }

        private string ApplyEquals(string string1, string string2)
        {
            int? value1 = TryIntParse(string1);
            int? value2 = TryIntParse(string2);

            if (value1.HasValue && value2.HasValue)
            {
                return "" + (value1.Value == value2.Value ? 1 : 0);
            }
            else
            {
                return string1 + string2;
            }
        }
        private int? TryIntParse(string string1)
        {
            int? value = null;
            try
            {
                value = Int32.Parse(string1);
            }
            catch { }

            return value;
        }

        private int FindInstructionPosition(List<string> words)
        {
            for (int i = 0; i < words.Count; i++)
            {
                foreach (var kvp in _instructions)
                {
                    if (kvp.Key.Equals(words[i]))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public event Action<string> Output;
        public void PrintToOutput(string text)
        {
            Output?.Invoke(text);
        }

        public event Action Clear;
        public void ClearOutput()
        {
            Clear?.Invoke();
        }
    }

    public CoubehRunnerInstance MainInstance = new CoubehRunnerInstance();
    public void SetCode(string code) => MainInstance.SetCode(code);
    public int Step() => MainInstance.Step();
}
