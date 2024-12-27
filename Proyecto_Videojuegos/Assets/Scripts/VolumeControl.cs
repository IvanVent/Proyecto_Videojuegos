using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class VolumeControl : MonoBehaviour
{
    private float safedvolume = 0.5f; // Almacena el volumen actual
    public TextMeshProUGUI volumetxt; // Referencia al TextMeshProUGUI en la UI
    public AudioMixer audioMixer; // Referencia al AudioMixer desde el inspector

    void Start()
    {
        UpdateVolumeUI(); // Actualiza el texto y el AudioMixer al iniciar
    }

    public void AddVolume()
    {
        safedvolume += 0.1f;
        safedvolume = Mathf.Clamp(safedvolume, 0.0001f, 1f);
        UpdateVolumeUI();
    }

    public void LessVolume()
    {
        safedvolume -= 0.1f;
        safedvolume = Mathf.Clamp(safedvolume, 0.0001f, 1f);
        UpdateVolumeUI();
    }

    public void SetVolume(float volume)
    {
        safedvolume = Mathf.Clamp(volume, 0.0001f, 1f);
        UpdateVolumeUI();
    }

    private void UpdateVolumeUI()
    {
        volumetxt.text = (safedvolume * 100).ToString("F0") + "%"; // Mostrar como porcentaje
        audioMixer.SetFloat("MyExposedParam", Mathf.Log10(safedvolume) * 20); // Ajustar el volumen
    }
}
