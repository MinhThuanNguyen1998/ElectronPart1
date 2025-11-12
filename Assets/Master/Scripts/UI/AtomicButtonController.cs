using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum AtomicType
{
    Name,
    Symbol,
    Number,
    Mass
}
public class AtomicButtonController : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AtomicType m_AtomicType;
    [SerializeField] private Button m_Button;
    [SerializeField] private GameObject m_Highlight;
    private void Awake()
    {
        if (m_Button == null) return;
            m_Button.onClick.AddListener(OnClick);
        if (m_Highlight != null)
            m_Highlight.SetActive(false);
    }

    private void OnClick()
    {
        AudioMainManager.Instance.StopAtomicSounds();
        switch (m_AtomicType)
        {
            case AtomicType.Name:
                AudioMainManager.Instance.PlayOnShot(SoundType.Atomic_Name);
                break;

            case AtomicType.Symbol:
                AudioMainManager.Instance.PlayOnShot(SoundType.Atomic_Symbol);
                break;

            case AtomicType.Number:
                AudioMainManager.Instance.PlayOnShot(SoundType.Atomic_Number);
                break;

            case AtomicType.Mass:
                AudioMainManager.Instance.PlayOnShot(SoundType.Atomic_Mass);
                break;
        }
        //Debug.Log($"Clicked button with AtomicType: {m_AtomicType}");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_Highlight != null)
            m_Highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_Highlight != null)
            m_Highlight.SetActive(false);
    }
}
