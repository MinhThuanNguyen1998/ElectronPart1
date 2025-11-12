using UnityEngine;
using UnityEngine.UI;

public class Electron_ChangeImageColor : MonoBehaviour
{
    [SerializeField] bool m_GetStartingColorFromMaterial;
    public FlexibleColorPicker m_FlexibleColorPicker;
    public Image m_Image;

    private void Start()
    {
        if (m_GetStartingColorFromMaterial)
            m_FlexibleColorPicker.color = m_Image.color;

        m_FlexibleColorPicker.onColorChange.AddListener(OnChangeColor);
    }
    private void OnChangeColor(Color co)
    {
        m_Image.color = co;
    }
}
