using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Collections;

public class SC02 : MonoBehaviour
{
    [SerializeField] private GameObject m_GameObjectRowAtomic;
    [SerializeField] private GameObject m_GroupLoading;
    [SerializeField] private Transform m_ContentParent;
    [SerializeField] private List<FullDElementDetails> m_ListElements;
    private float m_TimeToLoadData = 3f;

    private void Start()
    {
        StartCoroutine(LoadDataCoroutine());
    }
    private IEnumerator LoadDataCoroutine()
    {
        ShowGroupLoading(true);
        yield return new WaitForSeconds(m_TimeToLoadData);
        LoadDataFromFileJson();
        ShowGroupLoading(false);
    }
    private void ShowGroupLoading(bool isShow)
    {
        if (m_GroupLoading != null) m_GroupLoading.SetActive(isShow);
    }
    private void LoadDataFromFileJson()
    {
        string path = Path.Combine(Config.StreamingAssetsPath, Config.FullElementDetails);
        m_ListElements = JsonLoader.LoadDataFromJson<FullDElementDetails>(path);
        if (m_ListElements != null) DisplayElement();
    }
    private void DisplayElement()
    {
        foreach (var element in m_ListElements)
        {
            GameObject row = Instantiate(m_GameObjectRowAtomic, m_ContentParent);
            TableUILayout layout = row.GetComponent<TableUILayout>();
            layout.SetData(element);
        }
    }
}

