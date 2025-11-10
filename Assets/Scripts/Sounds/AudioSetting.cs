using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSetting : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider seSlider;

    public TMP_Text masterText;
    public TMP_Text bgmText;
    public TMP_Text seText;

    void Start()
    {

    }

    // เรียกจาก OnValueChanged ของ Slider
    public void OnMasterChanged(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
        masterText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void OnBGMChanged(float value)
    {
        AudioManager.Instance.SetBGMVolume(value);
        bgmText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void OnSEChanged(float value)
    {
        AudioManager.Instance.SetSEVolume(value);
        seText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void UpdateTexts()
    {
        masterText.text = Mathf.RoundToInt(masterSlider.value * 100) + "%";
        bgmText.text = Mathf.RoundToInt(bgmSlider.value * 100) + "%";
        seText.text = Mathf.RoundToInt(seSlider.value * 100) + "%";
    }

    // ปุ่ม Reset Default
    public void ResetDefault()
    {
        float def = 1f;
        masterSlider.value = def;
        bgmSlider.value = def;
        seSlider.value = def;

        OnMasterChanged(def);
        OnBGMChanged(def);
        OnSEChanged(def);
    }
}
