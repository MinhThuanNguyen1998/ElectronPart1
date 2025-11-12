using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManage : MonoBehaviour
{
    public static event Action OnStartButton;
    public static event Action OnCheckButton;
    public static event Action OnIntroductionButton;
    public static event Action OnBackButton;
    public static event Action OnDocButton;
    [SerializeField] private GameObject m_GroupHome;
    private void OnEnable() => UIButton.OnButtonClicked += HandleButtonClicked;
    private void OnDisable() => UIButton.OnButtonClicked -= HandleButtonClicked;

    private void Start() => m_GroupHome.gameObject.SetActive(true);
   

    private void HandleButtonClicked(UIButtonType type)
    {
        switch (type)
        {
            case UIButtonType.Start:
                OnStartButton?.Invoke();
                m_GroupHome.gameObject.SetActive(false);
                MainScene.Instance.LoadView(ViewID.SC01);
                break;
            case UIButtonType.Doc:
                MainScene.Instance.LoadView(ViewID.SC02);
                OnDocButton?.Invoke();  
                break;
            case UIButtonType.Introduction:
                m_GroupHome.gameObject.SetActive(false);
                MainScene.Instance.LoadView(ViewID.SC03);
                break;
            case UIButtonType.Check:
                OnCheckButton?.Invoke();
                break;
            case UIButtonType.Back:
                OnBackButton?.Invoke();
                MainScene.Instance.LoadView(ViewID.Home);
                AudioMainManager.Instance.StopAtomicSounds();
                m_GroupHome.gameObject.SetActive(true);
                break;
           
            case UIButtonType.Quit:
                MainScene.Instance.QuitApp();
                break;
            
        }
    }
}
