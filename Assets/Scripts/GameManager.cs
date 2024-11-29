using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timeBetweenInstructions = 1;
    [SerializeField] private CodeRunner _codeRunner;
    [SerializeField] private TMP_Text _code;
    
    [SerializeField] private CardsManager _cardsManager;

    [SerializeField] private CypheredText _endTurnActions;

    [SerializeField] private GameObject _winScreen;

    private float _timeBeforeTryExec;


    public enum GameState
    {
        Idle,
        EndTurn,
        Locked
    }

    private GameState _state = GameState.Idle;

    public void Awake()
    {
        _cardsManager.CardPlayed += ExecuteCard;
        _codeRunner.UnlockCalled += UnlockCalled;
        _codeRunner.SetCardsManager(_cardsManager);
    }
    
    public void Update()
    {
        if(_state == GameState.Locked) return;
        _timeBeforeTryExec -= Time.deltaTime;
        if (_timeBeforeTryExec <= 0)
        {
            Step();
            _timeBeforeTryExec = _timeBetweenInstructions;
        }
    }

    public void OnDestroy()
    {
        _cardsManager.CardPlayed -= ExecuteCard;
    }

    public void ExecuteEndTurn()
    {
        if (_state != GameState.Idle)
        {
            // play deny sound
            return;
        }
        
        SoundManager.PlayEndTurnSFX();
        _cardsManager.DiscardAll();
        _code.text = _endTurnActions.CurrentText;
        _codeRunner.SetInstructions(_endTurnActions.OriginalText);
        _state = GameState.EndTurn;
    }
    public void ExecuteCard(Card card)
    {
        _code.text = card.Effect.CurrentText;
        _codeRunner.SetInstructions(card.Effect.OriginalText);
        _timeBeforeTryExec = _timeBetweenInstructions;
        SoundManager.PlayExecutionSFX();
    }

    public void UnlockCalled()
    {
        _state = GameState.Locked;
        _winScreen.SetActive(true);
        SoundManager.PlayEndTurnSFX();
    }
    
    public void SetCode()
    {
        _codeRunner.SetInstructions(_code.text);
    }

    public void Step()
    {
        if (_codeRunner.InstructionPointer < 0)
        {
            if (_state == GameState.EndTurn)
            {
                _state = GameState.Idle;
                _cardsManager.ShuffleAndPlay();
            }

            _code.text = "WAITING _";
            return;
        }
        _codeRunner.Step();
        SoundManager.PlayExecutionSFX();
    }
}
