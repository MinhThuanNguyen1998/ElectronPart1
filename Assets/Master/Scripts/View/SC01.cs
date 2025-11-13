using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SC01 : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_InputFieldRingCount;
    [SerializeField] private List<GameObject> m_ListGameObjectElectronCount;
    [SerializeField] private GroupButtonManage m_GroupButtonManage;
    public static event Action<int> OnRingCountUpdated;
    public static event Action OnUnActiveGroupRing;
    public static event Action<bool> OnUnActiveAtomicGroup;
    public static event Action<int> OnGotoState;


    private void OnEnable()
    {
        m_InputFieldRingCount.onEndEdit.AddListener(OnRingCountChanged);
        ResetInputFielData();
        ActiveElectronInputFields(false);
        AudioMainManager.Instance.StopLoop();
       
    }
    private void OnDisable()
    {
        m_InputFieldRingCount.onEndEdit.RemoveListener(OnRingCountChanged);
        ResetInputFielData();
        ActiveElectronInputFields(false);
    }
    private void OnRingCountChanged(string value)
    {
        if (int.TryParse(value, out int Count))
        {
            if (Count > 0 && Count <= m_ListGameObjectElectronCount.Count)
            {
                UpdateElectronInputFields(Count);
                OnRingCountUpdated?.Invoke(Count);
                OnGotoState?.Invoke(1);
                m_GroupButtonManage.InteractiveButtonChangeColor(true);
                
            }
            else
            {
                PopUpManager.Instance.ShowPopUp(PopupType.Notification, Config.Text_InValid_Value, Config.Button_Yes, null);
                ResetInputFielData();
                OnRingCountUpdated?.Invoke(0);
                OnGotoState?.Invoke(0);
                m_GroupButtonManage.InteractiveButtonChangeColor(false);
            }
        }
        
    }
    private void UpdateElectronInputFields(int count)
    {
        for (int i = 0; i < m_ListGameObjectElectronCount.Count; i++)
        {
            bool active = i < count;
            m_ListGameObjectElectronCount[i].SetActive(active);
        }
    }
    private void ActiveElectronInputFields(bool isActive)
    {
        foreach (var gameObjectElectron in m_ListGameObjectElectronCount) gameObjectElectron.gameObject.SetActive(isActive);
    }
    private void ResetInputFielData()
    { 
        m_InputFieldRingCount.text = string.Empty;
        UpdateElectronInputFields(0);
        OnUnActiveGroupRing?.Invoke();
        OnUnActiveAtomicGroup?.Invoke(false);
    }
   
}
