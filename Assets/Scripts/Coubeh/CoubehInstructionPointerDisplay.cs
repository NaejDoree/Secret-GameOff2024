using TMPro;
using UnityEngine;

public class CoubehInstructionPointerDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _display;

    [SerializeField] private CoubehRunner _context;

    // Update is called once per frame
    void Update()
    {
        _display.text = "";
        if (_context.MainInstance.InstructionPointer >= 0)
        {
            for (int i = 0; i < _context.MainInstance.InstructionPointer; i++)
            {
                _display.text += "\n";
            }
            _display.text += ">";
        }
    }
}
