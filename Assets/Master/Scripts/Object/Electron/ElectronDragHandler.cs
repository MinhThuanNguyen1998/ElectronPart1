using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElectronDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform m_ElectronOrigin;
    [SerializeField] private Canvas m_Canvas;
    private RectTransform m_CurrentDraggedElectron;
    public static event Action<RectTransform> OnElectronReleased;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (CompareTag("ElectronOrigin"))
        {
            m_CurrentDraggedElectron = Instantiate(m_ElectronOrigin, m_ElectronOrigin.parent);
            m_CurrentDraggedElectron.tag = "ElectronSpawner";
            m_CurrentDraggedElectron.gameObject.SetActive(true);
            m_CurrentDraggedElectron.SetAsLastSibling();
        }
        else if (CompareTag("ElectronSpawner"))
        {
            m_CurrentDraggedElectron = transform as RectTransform;
        }
        else return;
        LimitPosElectron(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (m_CurrentDraggedElectron != null) LimitPosElectron(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //if (m_CurrentDraggedElectron == null) return;
        OnElectronReleased?.Invoke(m_CurrentDraggedElectron);
    }
    private void LimitPosElectron(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            m_Canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 pos))
        {
            RectTransform canvasRect = m_Canvas.transform as RectTransform;
            Vector2 electronSize = m_CurrentDraggedElectron.rect.size * 0.5f;
            float minX = -canvasRect.rect.width / 2f + electronSize.x;
            float maxX = canvasRect.rect.width / 2f - electronSize.x;
            float minY = -canvasRect.rect.height / 2f + electronSize.y;
            float maxY = canvasRect.rect.height / 2f - electronSize.y;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            m_CurrentDraggedElectron.anchoredPosition = pos;
        }
    }
}
