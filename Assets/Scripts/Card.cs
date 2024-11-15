using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Image _raycastTarget;
    [SerializeField] private CypheredText _cardText;
    private RectTransform m_DraggingPlane;
    public CypheredText Effect => _cardText;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        m_DraggingPlane = transform as RectTransform;

        _raycastTarget.raycastTarget = false;
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }
    
    
    private void SetDraggedPosition(PointerEventData data)
    {
        if (data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;
        
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            transform.position = globalMousePos;
            transform.rotation = m_DraggingPlane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _raycastTarget.raycastTarget = true;

        // Get if we're in a drop area
        //if we are, activate corresponding behaviour
        // if we're not, go back to hand
    }
}
