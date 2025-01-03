using TMPro;
using UnityEngine;

public class RizzDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _display;
    [SerializeField] private CoubehRunner _context;

    public void Update()
    {
        _display.text = "";
        foreach (var kvp in _context.MainInstance.Memory)
        {
            _display.text += $"{kvp.Key} : {kvp.Value} \n";
        }
    }
}
