using System;
using TMPro;
using UnityEngine;

public class VoiceDropDownList : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown m_DropDownVoice;
    public static int CurrentVoiceIndex { get; private set; }
    private void Start()
    {
        m_DropDownVoice.onValueChanged.AddListener(OnChangeVoice);
        CurrentVoiceIndex = m_DropDownVoice.value;
    }
    private void Destroy()
    {
        m_DropDownVoice.onValueChanged.RemoveListener(OnChangeVoice);
    }
    private void OnChangeVoice(int index)
    {
        CurrentVoiceIndex = index;
      
    }
}
