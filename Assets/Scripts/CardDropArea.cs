using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropArea : MonoBehaviour, IDropHandler
{
    public event Action<Card> CardReceived;
    
    public void OnDrop(PointerEventData eventData)
    {
        //var cardTransform = eventData.pointerDrag.transform;
        Card card = eventData.pointerDrag.GetComponent<Card>();
        if (card != null)
        {
            CardReceived?.Invoke(card);
        }
    }
}
