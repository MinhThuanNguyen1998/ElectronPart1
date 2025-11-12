using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TableUILayout : MonoBehaviour
{
    [Header("TMP Fields")]
    [SerializeField] private TMP_Text sttText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text symbolText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private TMP_Text methodText;
    [SerializeField] private TMP_Text atomicNumberText;
    [SerializeField] private TMP_Text atomicMassText;
    [SerializeField] private TMP_Text periodText;
    [SerializeField] private TMP_Text groupText;

    [Header("Image Field")]
    [SerializeField] private Image elementImage;
    
    public void SetData(FullDElementDetails data)
    {
        sttText.text = data.stt.ToString();
        nameText.text = data.name;
        symbolText.text = data.symbol;
        typeText.text = data.type;
        methodText.text = data.method;
        atomicNumberText.text = data.atomicNumber.ToString();
        atomicMassText.text = data.atomicMass.ToString();
        periodText.text = data.period.ToString();
        groupText.text = data.group;

        // Load hình ảnh
        LoadImage(data.imagePath);
    }

    private void LoadImage(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath))
        {
            Debug.LogWarning("No imagePath!!!");
            return;
        }

        string path = Config.StreamingAssetsPath;
        string pngPath = Path.Combine(path, imagePath + ".png");
        string jpgPath = Path.Combine(path, imagePath + ".jpg");
        string fullPath = null;

        if (File.Exists(pngPath))
            fullPath = pngPath;
        else if (File.Exists(jpgPath))
            fullPath = jpgPath;
        if (!string.IsNullOrEmpty(fullPath))
        {
            byte[] bytes = File.ReadAllBytes(fullPath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            elementImage.sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
        }
        else Debug.LogWarning($"Cannot find image (.png or .jpg): {imagePath}");
    }
}
