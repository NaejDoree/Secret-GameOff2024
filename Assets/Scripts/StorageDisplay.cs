using System;
using TMPro;
using UnityEngine;

public class StorageDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _display;
    [SerializeField] private CodeRunner _context;

    public void Update()
    {
        _display.text = "";
        foreach (var kvp in _context.Memory)
        {
            _display.text += $"{kvp.Key} | {kvp.Value} \n";
        }
    }
}
