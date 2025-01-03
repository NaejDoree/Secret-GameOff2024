using System;
using TMPro;
using UnityEngine;

public class CoubehManager : MonoBehaviour
{
    [SerializeField] private float _timeBetweenInstructions = 1;
    [SerializeField] private CoubehRunner _codeRunner;
    [SerializeField] private TMP_InputField _code;
    [SerializeField] private TMP_Text _output;

    private float _timeBeforeTryExec;

    public enum GameState
    {
        Editing,
        Running
    }

    private GameState _state = GameState.Editing;

    public GameState State
    {
        get => _state;
        set
        {
            _code.interactable = value == GameState.Editing;
//            _code.textComponent.color = value == GameState.Editing ? Color.white : Color.magenta;
            _code.textComponent.color = value == GameState.Editing ? new Color32(255, 255, 255, 255) : new Color32(255,0,255,255);
            _state = value;
        }
    }

    private void Awake()
    {
        _codeRunner.MainInstance.Output += (text) => _output.text += text + "\n";
    }

    public void Update()
    {
        if(_state == GameState.Editing) return;
        _timeBeforeTryExec -= Time.deltaTime;
        if (_timeBeforeTryExec <= 0)
        {
            Step();
            _timeBeforeTryExec = _timeBetweenInstructions;
        }
    }

    public void ExecuteCode()
    {
        _output.text = "";
        if (State != GameState.Editing)
        {
            State = GameState.Editing;
            return;
        }
        
        SoundManager.PlayEndTurnSFX();
        //clean slate please
        _codeRunner.MainInstance.Flags.Clear();
        _codeRunner.MainInstance.Memory.Clear();
        
        _codeRunner.SetCode(_code.text);
        State = GameState.Running;
    }

    public void Step()
    {
        if (_codeRunner.MainInstance.InstructionPointer < 0)
        {
            if (State == GameState.Running)
            {
                State = GameState.Editing;
            }
            return;
        }
        _codeRunner.Step();
        _code.text = string.Join("\n", _codeRunner.MainInstance._lines);
        SoundManager.PlayExecutionSFX();
    }
}
