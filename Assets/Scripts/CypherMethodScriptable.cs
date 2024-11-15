using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CypherMethodScriptable", menuName = "Scriptable Objects/CypherMethodScriptable")]
public class CypherMethodScriptable : ScriptableObject
{
    [Serializable]
    public class LetterSwap
    {
        public char OriginalLetter;
        public char ReplacementLetter;

        public char Swap(char original)
        {
            if (original == OriginalLetter)
            {
                return ReplacementLetter;
            }
            else if(original == ReplacementLetter)
            {
                return OriginalLetter;
            }
            else
            {
                return original;
            }
        }
    }
    
    [SerializeField] private List<LetterSwap> _replacements;

    public string CypherString(string original)
    {
        original = original.ToUpper();
        string endString = "";
        foreach (var letter in original)
        {
            LetterSwap swap = null;
            foreach (var currentReplacer in _replacements)
            {
                if (currentReplacer.OriginalLetter == letter || currentReplacer.ReplacementLetter == letter)
                {
                    swap = currentReplacer;
                    break;
                }
            }
            if (swap != null)
            {
                endString += swap.Swap(letter);
            }
            else
            {
                endString += letter;
            }
        }

        return endString;
    } 
}
