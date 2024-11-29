using System;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _storageDisplay;
    [SerializeField] private GameObject _endConDisplay;
    [SerializeField] private CardDropArea _cardDropArea;
    
    [SerializeField] private GameObject _introPanel;
    [SerializeField] private GameObject _playCardPanel;
    [SerializeField] private GameObject _endConTuto;


    private void Start()
    {
        _introPanel.gameObject.SetActive(true);
    }

    public void PlayCardPanel()
    {
        _introPanel.gameObject.SetActive(false);
        _playCardPanel.SetActive(true);
        _cardDropArea.CardReceived += CardDropArea_Received;
    }

    private void CardDropArea_Received(Card card)
    {
        _cardDropArea.CardReceived -= CardDropArea_Received;
        _playCardPanel.SetActive(false);
        
        _storageDisplay.SetActive(true);
        _endConDisplay.SetActive(true);
        
        _endConTuto.SetActive(true);
    }
}
