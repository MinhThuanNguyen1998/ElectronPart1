using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ElementChecker : MonoBehaviour
{
    [SerializeField] private List<TMP_InputField> m_InputFieldShell;
    [SerializeField] private TextMeshProUGUI m_TextKernel;
    private List<ElementData> m_ListElements;
    public static event Action<string, string, string, string> OnElementSelected;
    public static event Action<int> OnGotoState;
    private void OnEnable()
    {
        ShellElement.OnShellValueChanged += OnShellSubmit;
        SC01.OnUnActiveGroupRing += OnShellSubmit;
        UIManage.OnCheckButton += OnCheckElement;
        UIManage.OnBackButton += ResetText;
        UIManage.OnDocButton += ResetText;
    }
    private void OnDisable() 
    {
        ShellElement.OnShellValueChanged -= OnShellSubmit;
        SC01.OnUnActiveGroupRing -= OnShellSubmit;
        UIManage.OnCheckButton -= OnCheckElement;
        UIManage.OnBackButton -= ResetText;
        UIManage.OnDocButton -= ResetText; 
    }
    private void OnShellSubmit()
    {
        int total = 0;
        foreach (var input in m_InputFieldShell)
        {
            if (int.TryParse(input.text, out int value)) total += value;
        }
        m_TextKernel?.SetText(Config.Text_Kernel + total.ToString());
    }
    private void Start() 
    {
        LoadElementDataFromFileJson();
        ResetText();
        CheckInputFieldValueToShowStep();
    }
    private void CheckInputFieldValueToShowStep()
    {
        foreach (var input in m_InputFieldShell) input.onValueChanged.AddListener(OnInputSubmit);
    }
    private void OnInputSubmit(string arg0)
    {
        if (m_InputFieldShell.Any(i => !string.IsNullOrEmpty(i.text.Trim()))) OnGotoState?.Invoke(2);
        else OnGotoState?.Invoke(1);
    }
    private void ResetText() => m_TextKernel.text = string.Empty;
    private void LoadElementDataFromFileJson()
    {
        string path = Path.Combine(Config.StreamingAssetsPath, Config.ElementsData);
        m_ListElements = JsonLoader.LoadDataFromJson<ElementData>(path);
    }
    public void OnCheckElement()
    {
        if (m_ListElements == null || m_ListElements.Count == 0)
        {
            Debug.LogError("Element list is empty!");
            return;
        }
        List<int> userShells = new List<int>();
        foreach (var input in m_InputFieldShell)
        {
            if (input == null || string.IsNullOrWhiteSpace(input.text)) break;
            if (int.TryParse(input.text, out int value)) userShells.Add(value);
        }
        ElementData foundElement = m_ListElements.FirstOrDefault(e => CompareShells(e.shells, userShells));
        if (foundElement != null)
        {
            //Debug.Log($"Nguyên tố: {foundElement.name} ({foundElement.symbol})\n" + $"Số hiệu nguyên tử: {foundElement.atomicNumber}\n" +$"Khối lượng nguyên tử: {foundElement.atomicMass}\n" +$"Cấu hình e: {string.Join("-", foundElement.shells)}");
            OnElementSelected?.Invoke(foundElement.atomicNumber, foundElement.symbol, foundElement.name, foundElement.atomicMass);
        }
        else PopUpManager.Instance.ShowPopUp(PopupType.Notification, Config.Text_Atom, Config.Button_Yes, Config.Button_No, null, null);
    }
    private bool CompareShells(List<int> a, List<int> b)
    {
        if (a == null || b == null || a.Count != b.Count) return false;
        for (int i = 0; i < a.Count; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }
}
