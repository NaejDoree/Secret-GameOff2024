using TMPro;
using UnityEngine;

public class InstructionPointerDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _display;

    [SerializeField] private CodeRunner _context;

    // Update is called once per frame
    void Update()
    {
        _display.text = "";
        if (_context.InstructionPointer >= 0)
        {
            for (int i = 0; i < _context.InstructionPointer; i++)
            {
                _display.text += "\n";
            }
            _display.text += ">";
        }
    }
}
