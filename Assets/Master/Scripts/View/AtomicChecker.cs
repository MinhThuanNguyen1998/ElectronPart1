using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AtomicChecker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextAtomicNumber;
    [SerializeField] private TextMeshProUGUI m_TextSymbol;
    [SerializeField] private TextMeshProUGUI m_TextName;
    [SerializeField] private TextMeshProUGUI m_TextMass;
    [SerializeField] private List<GameObject> m_GroupAtomicInfor;
    private void OnEnable() 
    {
        ElementChecker.OnElementSelected += OnElementSelected;
        SC01.OnUnActiveAtomicGroup += ActiveGroupAtomicInfor;
        ShellElement.OnUnActiveAtomicGroup += ActiveGroupAtomicInfor;
    }
   
    private void OnDisable()
    {
        ActiveGroupAtomicInfor(false);
        ResetText();
        ElementChecker.OnElementSelected -= OnElementSelected;
        SC01.OnUnActiveAtomicGroup -= ActiveGroupAtomicInfor;
        ShellElement.OnUnActiveAtomicGroup -= ActiveGroupAtomicInfor;
    }
    private void Start() => ActiveGroupAtomicInfor(false);
    private void OnElementSelected(string atomic, string symbol, string name, string mass)
    {
        m_TextAtomicNumber.text = atomic;
        m_TextSymbol.text = symbol;
        m_TextName.text = name;
        m_TextMass.text = mass;
        ActiveGroupAtomicInfor(true);
    }
    private void ActiveGroupAtomicInfor(bool isActive)
    {
        foreach(var g  in m_GroupAtomicInfor) g.gameObject.SetActive(isActive);
    }
    private void ResetText()
    {
        foreach (var textField in new[] { m_TextAtomicNumber, m_TextMass, m_TextName, m_TextSymbol })
            textField.text = string.Empty;
    }
}
