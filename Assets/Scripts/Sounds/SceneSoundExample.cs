using UnityEngine;

public class SceneSoundExample : MonoBehaviour
{
    public AudioClip stageBGM;
    public AudioClip attackSound;

    void Start()
    {
        // เล่นเพลงประจำฉาก
        AudioManager.Instance.PlayBGM(stageBGM);
    }

    public void OnAttack()
    {
        // เล่นเสียงเอฟเฟกต์
        AudioManager.Instance.PlaySE(attackSound);
    }
}
