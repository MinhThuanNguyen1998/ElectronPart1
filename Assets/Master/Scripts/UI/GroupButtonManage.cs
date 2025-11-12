using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class GroupButtonManage : MonoBehaviour
{
    [Header("Groups")]
    [SerializeField] private GameObject m_GroupStep;
    [SerializeField] private GameObject m_GroupCheckStep;

    [Header("Function Buttons")]
    [SerializeField] private List<Button> m_ListButtonFunction;

    [Header("Function ButtonStep")]
    [SerializeField] private Button m_ButtonStep;

    [Header("Close Buttons")]
    [SerializeField] private Button m_ButtonCloseGroupStep;
    [SerializeField] private Button m_ButtonCloseGroupCheckStep;
    [SerializeField] private Button m_ButtonHint;

    [Header("Button Sprites")]
    [SerializeField] private Sprite m_SpriteNormal;
    [SerializeField] private Sprite m_SpritePressed;
    [SerializeField] private Image m_ButtonStepImage;

    private void Awake()
    {
        HideAllGroups();
        foreach (var btn in m_ListButtonFunction)
        {
            btn.onClick.AddListener(HideAllGroups);
        }
        m_ButtonStep.onClick.AddListener(() => ToggleGroup(m_GroupStep));

        m_ButtonCloseGroupStep.onClick.AddListener(() => HideGroup(m_GroupStep));
        m_ButtonHint.onClick.AddListener(() => HideGroup(m_GroupStep));
        m_ButtonCloseGroupCheckStep.onClick.AddListener(() => HideGroup(m_GroupCheckStep));
    }
    private void HideAllGroups()
    {
        HideGroup(m_GroupStep);
        HideGroup(m_GroupCheckStep);
    }
    private void HideGroup(GameObject group)
    {
        if (group != null)
        {
            group.SetActive(false);

            // Nếu group bị tắt mà là GroupStep -> đổi lại sprite normal
            if (group == m_GroupStep && m_ButtonStepImage != null) m_ButtonStepImage.sprite = m_SpriteNormal;
        }
    }
    private void ToggleGroup(GameObject group)
    {
        if (group != null)
        {
            bool newState = !group.activeSelf;
            group.SetActive(newState);

            // Luôn ẩn group còn lại
            HideGroup(m_GroupCheckStep);

            // Nếu là GroupStep thì đổi sprite tương ứng
            if (group == m_GroupStep && m_ButtonStepImage != null) m_ButtonStepImage.sprite = newState ? m_SpritePressed : m_SpriteNormal;
        }
    }
       
}

