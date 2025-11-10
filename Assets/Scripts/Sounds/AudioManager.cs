using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Mixer")]
    public AudioMixer audioMixer; // ลาก MainAudioMixer ลงที่ inspector

    [Header("Sources")]
    public AudioSource bgmSource; // ให้ Output เป็น MainAudioMixer/BGM
    public AudioSource seSource;  // ให้ Output เป็น MainAudioMixer/SE

    private void Awake()
    {
        // singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // โหลดค่าเสียงจาก PlayerPrefs (default 1)
        float master = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float bgm = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float se = PlayerPrefs.GetFloat("SEVolume", 1f);

        // ตั้ง mixer (ใช้ dB)
        SetMasterVolume(master);
        SetBGMVolume(bgm);
        SetSEVolume(se);
    }

    // ค่าที่รับเข้ามาเป็น 0.0001 - 1.0 (ไม่ให้เป็น 0 เพราะ Log10 จะ error)
    public void SetMasterVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetBGMVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    public void SetSEVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("SEVolume", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("SEVolume", value);
    }

    // เล่น BGM (ใช้เมื่อต้องเปลี่ยนเพลงในแต่ละ scene)
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (bgmSource.clip == clip) return;
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    // เล่น SE แบบ one-shot
    public void PlaySE(AudioClip clip)
    {
        seSource.PlayOneShot(clip);
    }
}
