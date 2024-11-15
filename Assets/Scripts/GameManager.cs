using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timeBetweenInstructions = 1;
    [SerializeField] private CodeRunner _codeRunner;
    [SerializeField] private TMP_Text _code;

    [SerializeField] private CardDropArea _cardDropArea;

    private float _timeBeforeTryExec;

    public void Awake()
    {
        _cardDropArea.cardReceived += OnCardLaunched;
    }

    public void Update()
    {
        _timeBeforeTryExec -= Time.deltaTime;
        if (_timeBeforeTryExec <= 0)
        {
            Step();
            _timeBeforeTryExec = _timeBetweenInstructions;
        }
    }

    public void OnDestroy()
    {
        _cardDropArea.cardReceived -= OnCardLaunched;
    }

    public void OnCardLaunched(Card card)
    {
        card.gameObject.SetActive(false);
        _code.text = card.Effect.CurrentText;
        _codeRunner.SetInstructions(card.Effect.OriginalText);
        _timeBeforeTryExec = _timeBetweenInstructions;
    }
    
    public void SetCode()
    {
        _codeRunner.SetInstructions(_code.text);
    }

    public void Step()
    {
        if (_codeRunner.InstructionPointer < 0)
        {
            return;
        }
        _codeRunner.Step();
    }
}
