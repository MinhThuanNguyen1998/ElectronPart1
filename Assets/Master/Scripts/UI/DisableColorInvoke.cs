using UnityEngine;

// This script prevents event reset color from package
public class DisableColorInvoke : MonoBehaviour
{
    [SerializeField] FlexibleColorPicker m_ColorPicker;
    
    void OnDisable()
    { 
        m_ColorPicker.onColorChange.RemoveAllListeners();
    }
}
