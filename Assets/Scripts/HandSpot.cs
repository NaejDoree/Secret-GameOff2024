using System;
using Unity.VisualScripting;
using UnityEngine;

public class HandSpot : MonoBehaviour
{
    [SerializeField] private CardDropArea _cardDropArea;
    public Card CardInSpot;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (CardInSpot != null && !CardInSpot.Dragged)
        {
            
            MoveCardTowardsThis();
        }
    }

    private void MoveCardTowardsThis()
    {
        RectTransform cardTransform = CardInSpot.GetComponent<RectTransform>();
        Vector2 cardPos = cardTransform.position;
        Vector2 destination = _rectTransform.position;
        Vector2 fullMovementVector = destination - cardPos;
        Vector2 movement = fullMovementVector.normalized * Time.deltaTime * 50f;

        if (fullMovementVector.sqrMagnitude < movement.sqrMagnitude)
        {
            //snap to endPos
            cardTransform.position = destination;
            return;
        }
        
        cardTransform.position = cardPos + movement;
    }
}
