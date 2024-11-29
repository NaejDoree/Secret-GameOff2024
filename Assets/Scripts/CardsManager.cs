using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private List<Card> _cards = new List<Card>();
    
    [SerializeField] private List<HandSpot> _handSpots = new List<HandSpot>();
    [SerializeField] private CardDropArea _playSpot;

    [SerializeField] private List<Card> _deck = new List<Card>();
    [SerializeField] private List<Card> _discard = new List<Card>();

    [SerializeField] private RectTransform _cardsSpawnPoint;

    public event Action<Card> CardPlayed; 


    private void Start()
    {
        _playSpot.CardReceived += CardReceived_PlaySpot;
        _cards.AddRange(GetComponentsInChildren<Card>(true));
        _deck.Clear();
        _deck.AddRange(_cards);
        foreach (var card in _cards)
        {
            card.gameObject.SetActive(false);
        }

        StartCoroutine(InitialStart());
    }

    private IEnumerator InitialStart()
    {
        yield return new WaitForSeconds(0.1f);
        ShuffleAndPlay();
    }

    public void ShuffleAndPlay()
    {
        ShuffleAll();
        Draw(3);
    }

    public void ShuffleAll()
    {
        DiscardAll();
        _deck.AddRange(_discard);
        _discard.Clear();
        // stupid shuffle
        _deck.Sort((a,b) => Random.Range(-1,1));
    }

    public void DiscardAll()
    {
        _discard.AddRange(_deck);
        _deck.Clear();
        foreach (var spot in _handSpots)
        {
            var card = spot.CardInSpot;
            if(card == null) continue;
            
            card.gameObject.SetActive(false);
            spot.CardInSpot = null;
            _discard.Add(card);
        }
    }

    private void OnDestroy()
    {
        _playSpot.CardReceived -= CardReceived_PlaySpot;
    }

    private void CardReceived_PlaySpot(Card card)
    {
        foreach (var spot in _handSpots)
        {
            if (spot.CardInSpot == card)
            {
                spot.CardInSpot = null;
            }
        }
        card.gameObject.SetActive(false);
        _discard.Add(card);
        CardPlayed?.Invoke(card);
    }


    public bool Draw()
    {
        HandSpot availableSpot = null;
        foreach (var spot in _handSpots)
        {
            if (spot.CardInSpot == null)
            {
                availableSpot = spot;
                break;
            }
        }
        // check if there is room in hand and cards in deck
        if(availableSpot == null || _deck.Count == 0) return false;
        
        // if there is take a card from deck and place it in a hand spots
        var toAdd = _deck[0];
        toAdd.gameObject.SetActive(true);
        //toAdd.GetComponent<RectTransform>().position = availableSpot.GetComponent<RectTransform>().position;
        toAdd.GetComponent<RectTransform>().position = _cardsSpawnPoint.position;

        availableSpot.CardInSpot = toAdd;
        _deck.Remove(toAdd);
        return true;
    }
    
    public void Draw(int cardAmount)
    {
        for (int i = 0; i < cardAmount; i++)
        {
            bool possible = Draw();
            if(possible == false) return;
        }
    }
}
