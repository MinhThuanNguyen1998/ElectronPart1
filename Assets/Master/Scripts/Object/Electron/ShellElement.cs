using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class ShellElement : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private RectTransform m_Circle;
    [SerializeField] private RectTransform m_ElectronPrefab;
    [SerializeField] private RectTransform m_Canvas;
    [SerializeField] private TMP_InputField m_InputField;
    [SerializeField] private int m_MaxElectron = 8;
    [SerializeField] private float m_RadiusOffset = 0f;
    [SerializeField] private List<RectTransform> m_ListElectrons = new();

    public static event Action OnShellValueChanged;
    public static event Action<bool> OnUnActiveAtomicGroup;
    private float m_Radius;
    public RectTransform Circle => m_Circle;
    public int MaxElectron => m_MaxElectron;
    private void OnEnable()
    {
        UIManage.OnBackButton += ClearElectronsInList;
        SC01.OnUnActiveGroupRing += ClearElectronsInList;
    }
    private void OnDisable()
    {
        UIManage.OnBackButton -= ClearElectronsInList;
        SC01.OnUnActiveGroupRing -= ClearElectronsInList;
    }
    private void Start()
    {
        m_Radius = m_Circle.rect.width * 0.5f;
        m_InputField?.onEndEdit.AddListener(OnElectronCountSubmit);
    }
    private void OnDestroy() => m_InputField?.onEndEdit.RemoveListener(OnElectronCountSubmit);
    private void Update()
    {
        if (m_ListElectrons.Exists(e => e == null)) RemoveElectronIfNull();
    }
    private void OnElectronCountSubmit(string value)
    {
        if (!int.TryParse(value, out int count) || count < 0)
        {
            ShowInvalidPopup(Config.Text_InValid_Value);
            ResetInputAndShell();
            return;
        }
        if (count > m_MaxElectron)
        {
            ShowInvalidPopup(Config.Text_InValid_Electron_Value);
            ResetInput();
            return;
        }
        AddElectronToCircleByInputField(count);
        NotifyShellChange();
    }
    private void ShowInvalidPopup(string message) => PopUpManager.Instance.ShowPopUp(PopupType.Notification, message, Config.Button_Yes, null);
    private void ResetInput() => m_InputField.text = "";
    private void ResetInputAndShell()
    {
        ResetInput();
        AddElectronToCircleByInputField(0);
        NotifyShellChange();
    }
    private void NotifyShellChange()
    {
        OnShellValueChanged?.Invoke();
        OnUnActiveAtomicGroup?.Invoke(false);
    }
    public void SetRadius(float radius)
    {
        m_Radius = radius;
        m_Circle.sizeDelta = Vector2.one * (radius * 2f);
        UpdateElectronPositions();
    }
    public void AddElectronToCircleByMouse(RectTransform newElectron)
    {
        if (m_Circle == null || !m_Circle.gameObject.activeInHierarchy || m_ListElectrons.Count >= m_MaxElectron)
        {
            Destroy(newElectron.gameObject);
            return;
        }
        if (!m_ListElectrons.Contains(newElectron))
        {
            m_ListElectrons.Add(newElectron);
            RefreshUI();
        }
        else UpdateElectronPositions();
    }
    public void AddElectronToCircleByInputField(int count)
    {
        ClearElectronsInList();
        for (int i = 0; i < count; i++)
        {
            RectTransform electron = Instantiate(m_ElectronPrefab, m_Canvas);
            electron.tag = "ElectronSpawner";
            m_ListElectrons.Add(electron);
        }
        UpdateElectronPositions();
        m_InputField.text = count > 0 ? count.ToString() : "";
    }
    private void UpdateElectronPositions()
    {
        int count = m_ListElectrons.Count;
        if (count == 0) return;
        float angleStep = 360f / count;
        float radius = m_Radius + m_RadiusOffset;
        for (int i = 0; i < count; i++)
        {
            float angle = Mathf.Deg2Rad * (angleStep * i);
            m_ListElectrons[i].anchoredPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }
    }
    private void ClearElectronsInList()
    {
        foreach (var e in m_ListElectrons)
            if (e != null) Destroy(e.gameObject);
        m_ListElectrons.Clear();
    }
    private void RemoveElectronIfNull()
    {
        int before = m_ListElectrons.Count;
        m_ListElectrons.RemoveAll(e => e == null);
        if (m_ListElectrons.Count != before)
        {
            m_InputField.text = m_ListElectrons.Count.ToString();
            OnShellValueChanged?.Invoke();
        }
    }
    public bool HasElectron(RectTransform electron) => m_ListElectrons.Contains(electron);
    public void RemoveExistingElectron(RectTransform electron)
    {
        if (m_ListElectrons.Contains(electron))
        {
            m_ListElectrons.Remove(electron);
            RefreshUI();
        }
    }
    private void RefreshUI()
    {
        UpdateElectronPositions();
        m_InputField.text = m_ListElectrons.Count > 0 ? m_ListElectrons.Count.ToString() : "";
        OnShellValueChanged?.Invoke();
    }
}
