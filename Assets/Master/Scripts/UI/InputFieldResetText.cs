using TMPro;
using UnityEngine;

public class InputFieldResetText : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_InputField;

    private void OnEnable()
    {
        m_InputField.text = "";
    }
    private void OnDisable()
    {
        m_InputField.text = "";
    }
}
