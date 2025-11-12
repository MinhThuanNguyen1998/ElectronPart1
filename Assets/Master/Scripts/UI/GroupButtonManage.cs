using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;
public class GroupButtonManage : MonoBehaviour
{
    [Header("Groups")]
    [SerializeField] private GameObject m_GroupStep;
    [SerializeField] private GameObject m_GroupCheckStep;
    [SerializeField] private GameObject m_GroupChangeColor;

    [Header("Function Buttons")]
    [SerializeField] private List<Button> m_ListButtonFunction;

    [Header("Function ButtonStep")]
    [SerializeField] private Button m_ButtonStep;
    [SerializeField] private Button m_ButtonChangeColor;

    [Header("Close Buttons")]
    [SerializeField] private Button m_ButtonCloseGroupStep;
    [SerializeField] private Button m_ButtonCloseGroupCheckStep;
    [SerializeField] private Button m_ButtonHint;

    [Header("Button Sprites")]
    [SerializeField] private Sprite m_SpriteNormal;
    [SerializeField] private Sprite m_SpritePressed;
    [SerializeField] private Image m_ButtonStepImage;
    [SerializeField] private Image m_ButtonChangeImage;

    private void Awake()
    {
        HideAllGroups();
        foreach (var btn in m_ListButtonFunction)
        {
            btn.onClick.AddListener(HideAllGroups);
        }
        m_ButtonStep.onClick.AddListener(() => ToggleGroup(m_GroupStep));
        m_ButtonChangeColor.onClick.AddListener(() => ToggleGroup(m_GroupChangeColor));

        m_ButtonCloseGroupStep.onClick.AddListener(() => HideGroup(m_GroupStep));
        m_ButtonHint.onClick.AddListener(() => HideGroup(m_GroupStep));
        m_ButtonHint.onClick.AddListener(() => HideGroup(m_GroupChangeColor));
        m_ButtonCloseGroupCheckStep.onClick.AddListener(() => HideGroup(m_GroupCheckStep));
    }
    private void HideAllGroups()
    {
        HideGroup(m_GroupStep);
        HideGroup(m_GroupCheckStep);
        HideGroup(m_GroupChangeColor);
    }
    private void HideGroup(GameObject group)
    {
        if (group != null)
        {
            group.SetActive(false);
            if (group == m_GroupStep && m_ButtonStepImage != null)
                m_ButtonStepImage.sprite = m_SpriteNormal;

            if (group == m_GroupChangeColor && m_ButtonChangeImage != null)
                m_ButtonChangeImage.sprite = m_SpriteNormal;
        }
    }
    private void ToggleGroup(GameObject group)
    {
        if (group == null) return;

        bool newState = !group.activeSelf;

        HideAllGroups();
        group.SetActive(newState);

        if (group == m_GroupStep && m_ButtonStepImage != null)
            m_ButtonStepImage.sprite = newState ? m_SpritePressed : m_SpriteNormal;

        if (group == m_GroupChangeColor && m_ButtonChangeImage != null)
            m_ButtonChangeImage.sprite = newState ? m_SpritePressed : m_SpriteNormal;
    }
}

