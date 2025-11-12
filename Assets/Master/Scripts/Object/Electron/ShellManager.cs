using System.Collections.Generic;
using UnityEngine;

public class ShellManager : MonoBehaviour
{
    [Header("Shell Settings")]
    [SerializeField] private List<ShellElement> m_Shells = new List<ShellElement>();
    [SerializeField] private float m_StartRadius = 150f;
    [SerializeField] private float m_RadiusStep = 100f;
    [SerializeField] private float m_SnapThreshold = 50f;
    private void OnEnable() => ElectronDragHandler.OnElectronReleased += HandleElectronReleased;
    private void OnDisable() => ElectronDragHandler.OnElectronReleased -= HandleElectronReleased;
    private void Start() => SetupShells();
    private void SetupShells()
    {
        for (int i = 0; i < m_Shells.Count; i++)
        {
            float radius = m_StartRadius + i * m_RadiusStep;
            m_Shells[i].SetRadius(radius);
        }
    }
    private void HandleElectronReleased(RectTransform releasedElectron)
    {
        ShellElement nearestShell = null;
        float minDistance = float.MaxValue;
        foreach (var shell in m_Shells)
        {
            Vector2 circleCenter = shell.Circle.anchoredPosition;
            float radius = shell.Circle.rect.width / 2f;
            float distance = Vector2.Distance(releasedElectron.anchoredPosition, circleCenter);
            float diff = Mathf.Abs(distance - radius);
            if (diff < minDistance)
            {
                minDistance = diff;
                nearestShell = shell;
            }
        }
        if (nearestShell != null && minDistance <= m_SnapThreshold)
        {
            foreach (var shell in m_Shells)
            {
                if (shell != nearestShell && shell.HasElectron(releasedElectron))
                {
                    shell.RemoveExistingElectron(releasedElectron);
                }
            }
            nearestShell.AddElectronToCircleByMouse(releasedElectron);
            PlayAudioElectronDrag();
        }
        else 
        {
            PlayAudioElectronDrag();
            Destroy(releasedElectron.gameObject);
        }
    }

    private void PlayAudioElectronDrag() => AudioMainManager.Instance.PlayOnShot(SoundType.ElectronDrag);
}
