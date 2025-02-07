using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Coubeh_InstrSkibidi : CoubehInstruction
{
    public override void Run(CoubehRunner.CoubehRunnerInstance coubehRunnerInstance, List<string> parameters)
    {
        var line = string.Join(" ", parameters);

        // Evaluate anything that is between "toilet()" flags, at first level only

        string toiletExpr = "toilet(";
        int index = line.IndexOf(toiletExpr);
        while (index >= 0) {
            int nestingLevel = 1;
            int nextStartIndex = index + toiletExpr.Length;
            for (int i = index + toiletExpr.Length; i < line.Length; i++) {
                if (line[i] == '(') {
                    nestingLevel++;
                } else if (line[i] == ')') {
                    nestingLevel--;
                    if (nestingLevel == 0) {
                        string toiletContent = line.Substring(index + toiletExpr.Length, i - index - toiletExpr.Length);
                        string toiletResult = String.Join(" ", coubehRunnerInstance.ExecuteOperators(toiletContent.Split(' ').ToList()));
                        line = line.Substring(0, index) + toiletResult + line.Substring(i + 1);
                        nextStartIndex = index + toiletResult.Length;
                        break;
                    }
                }
            }

            if (nestingLevel != 0 || nextStartIndex >= line.Length) {
                break;
            }

            index = line.IndexOf(toiletExpr, nextStartIndex);
        }

        coubehRunnerInstance._lines.Add(line);
    }
}