using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;
public enum SoundType
{
    Button,
    Popup,
    Introduction,
    ElectronDrag,
    Atomic_Number,
    Atomic_Mass,
    Atomic_Symbol,
    Atomic_Name

}
public class AudioMainManager : SingletonMain<AudioMainManager>
{
    [Header("Audio Effects")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioPopupClip;
    [SerializeField] private AudioClip m_AudioButtonClip;
    [SerializeField] private AudioClip m_AudioIntroductionClip;
    [SerializeField] private AudioClip m_AudioElectronDrag;
    [Header("Audio Atomic")]
    [SerializeField] private AudioClip m_Atomic_Number;
    [SerializeField] private AudioClip m_Atomic_Mass;
    [SerializeField] private AudioClip m_Atomic_Symbol;
    [SerializeField] private AudioClip m_Atomic_Name;

    private Dictionary<SoundType, AudioClip> m_SoundMap;
    private void Awake()
    {
        m_SoundMap = new Dictionary<SoundType, AudioClip>
        {
            { SoundType.Button, m_AudioButtonClip },
            { SoundType.Introduction, m_AudioIntroductionClip },
            { SoundType.Popup, m_AudioPopupClip },
            {SoundType.ElectronDrag, m_AudioElectronDrag },
            {SoundType.Atomic_Number, m_Atomic_Number },
            {SoundType.Atomic_Mass, m_Atomic_Mass },
            {SoundType.Atomic_Name, m_Atomic_Name },
            {SoundType.Atomic_Symbol, m_Atomic_Symbol }  
        };
    }
    public void PlayOnShot(SoundType soundType)
    {
        if (m_SoundMap.TryGetValue(soundType, out var clip) && clip != null)
            m_AudioSource.PlayOneShot(clip);
        else
            Debug.LogWarning($"AudioManager: AudioClip for {soundType} is not assigned.");
    }
    public void StopSound(SoundType type) => m_AudioSource?.Stop();
    public void StopAtomicSounds()
    {
        if (m_AudioSource != null && m_AudioSource.isPlaying) m_AudioSource.Stop();
    }
    public void PlayLoop(SoundType soundType)
    {
        if (m_SoundMap.TryGetValue(soundType, out var clip) && clip != null && m_AudioSource != null)
        {
            m_AudioSource.loop = true;
            m_AudioSource.clip = clip;
            m_AudioSource.Play();
        }
    }
    public void PlayAudioIntroduction(AudioClip clip)
    {
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
    }
    public void StopLoop()
    {
        if (m_AudioSource != null)
        {
            m_AudioSource.Stop();
            m_AudioSource.loop = false;
            m_AudioSource.clip = null;
        }
    }
}
