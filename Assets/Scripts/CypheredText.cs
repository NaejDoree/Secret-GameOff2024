using TMPro;
using UnityEngine;

public class CypheredText : MonoBehaviour
{
    [SerializeField] private bool _cypheredByDefault = true;
    [SerializeField] private CypherMethodScriptable _cypherMethod;
    private string _originalText;
    private TMP_Text _textDisplay;

    private bool _cyphered;
    public bool Cyphered => _cyphered;
    public string OriginalText => _originalText;
    public string CurrentText => _textDisplay.text;
    
    void Start()
    {
        Setup();
        if (_cypheredByDefault)
        {
            Cypher();
        }
    }

    private void Setup()
    {
        if (_textDisplay == null)
        {
            _textDisplay = GetComponent<TMP_Text>();
        }

        if (string.IsNullOrEmpty(_originalText))
        {
            _originalText = _textDisplay.text.ToUpper();
        }
    }

    public void Cypher(bool cyphered = true)
    {
        _textDisplay.text = cyphered ? _cypherMethod.CypherString(_originalText) : _originalText;
        _cyphered = cyphered;
    }
}
