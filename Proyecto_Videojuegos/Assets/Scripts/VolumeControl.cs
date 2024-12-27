using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    private static float safedvolume = 0.5f;

    void Start()
    {
        AudioManager.instance.audioMixer.SetFloat("MyExposedParam", Mathf.Log10(safedvolume) * 20);
    }

    public static void addVolume()
    {
        safedvolume += 0.1f;
        safedvolume = Mathf.Clamp(safedvolume, 0.0001f, 1f); // Asegura que esté en el rango válido
        AudioManager.instance.audioMixer.SetFloat("MyExposedParam", Mathf.Log10(safedvolume) * 20);
    }

    public static void lessVolume()
    {
        safedvolume -= 0.1f;
        safedvolume = Mathf.Clamp(safedvolume, 0.0001f, 1f); // Asegura que esté en el rango válido
        AudioManager.instance.audioMixer.SetFloat("MyExposedParam", Mathf.Log10(safedvolume) * 20);
    }

    public static void SetVolume(float volume)
    {
        safedvolume = Mathf.Clamp(volume, 0.0001f, 1f); // Asegura que esté en el rango válido
        AudioManager.instance.audioMixer.SetFloat("MyExposedParam", Mathf.Log10(safedvolume) * 20);
    }
}
