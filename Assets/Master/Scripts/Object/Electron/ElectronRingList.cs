using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ElectronRingList : MonoBehaviour
{
    [SerializeField] private List<Image> m_ListImages;
    [SerializeField] private List<GameObject> m_ListElectron_Text;
    [SerializeField] private GameObject m_CanvasElectronRing;
    private void OnEnable()
    {
        SC01.OnRingCountUpdated += UpdateRingList;
        SC01.OnUnActiveGroupRing += UnActiveGroup;
        UIManage.OnBackButton += UnActiveGroup;
    }
    private void OnDisable()
    {
        SC01.OnRingCountUpdated -= UpdateRingList;
        SC01.OnUnActiveGroupRing -= UnActiveGroup;
        UIManage.OnBackButton -= UnActiveGroup;
    }
    private void Start() => UnActiveGroup();
    public void UpdateRingList(int count)
    {   
        if (count > 0)
        {
            SetActive(m_ListElectron_Text, true);
            for (int i = 0; i < m_ListImages.Count; i++)
                m_ListImages[i].gameObject.SetActive(i < count);
        }
        else SetActive(m_ListElectron_Text, false);
    }
    public void UnActiveGroup()
    {
        SetActive(m_ListElectron_Text, false);
        SetActive(m_ListImages, false);
    }
    private void SetActive(object list, bool isActive)
    {
        switch (list)
        {
            case IEnumerable<Image> images:
                foreach (var img in images)
                    img.gameObject.SetActive(isActive);
                break;
            case IEnumerable<GameObject> objects:
                foreach (var obj in objects)
                    obj.SetActive(isActive);
                break;
            default:
                Debug.LogWarning($"Unsupported type passed to SetActive: {list.GetType()}");
                break;
        }
    }
}
