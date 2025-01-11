using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    private float safedvolume = 0.5f; // Almacena el volumen actual
    private float sfxvolume=0.5f;
    public Text volumetxt; // Referencia al TextMeshProUGUI en la UI
    public Text sfxtxt;
    public AudioMixer audioMixer; // Referencia al AudioMixer desde el inspector
    public AudioMixer sfx;

    void Start()
    {
        if(PlayerPrefs.HasKey("volmusic")){
            safedvolume=PlayerPrefs.GetFloat("volmusic");
        }
        else{
            PlayerPrefs.SetFloat("volmusic",safedvolume);
        }
        if(PlayerPrefs.HasKey("volsfx")){
            sfxvolume=PlayerPrefs.GetFloat("volsfx");
        }
        else{
            PlayerPrefs.SetFloat("volsfx",sfxvolume);
        }

        UpdateVolumeUI(); // Actualiza el texto y el AudioMixer al iniciar
        UpdateSFx();
    }

    public void AddVolume()
    {
        safedvolume += 0.1f;
        safedvolume = Mathf.Clamp(safedvolume, 0.0001f, 1f);
        PlayerPrefs.SetFloat("volmusic",safedvolume);
        UpdateVolumeUI();
    }

    public void LessVolume()
    {
        safedvolume -= 0.1f;
        safedvolume = Mathf.Clamp(safedvolume, 0.0001f, 1f);
        PlayerPrefs.SetFloat("volmusic",safedvolume);
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
    public void AddSfx(){
        sfxvolume += 0.1f;
        sfxvolume = Mathf.Clamp(sfxvolume, 0.0001f, 1f);
        PlayerPrefs.SetFloat("volsfx",sfxvolume);
        UpdateSFx();
    }
    public void LessSfx(){
        sfxvolume -= 0.1f;
        sfxvolume = Mathf.Clamp(sfxvolume, 0.0001f, 1f);
        PlayerPrefs.SetFloat("volsfx",sfxvolume);
        UpdateSFx();
    }
    private void UpdateSFx(){
        sfxtxt.text = (sfxvolume * 100).ToString("F0") + "%"; // Mostrar como porcentaje
        sfx.SetFloat("MyExposedParam", Mathf.Log10(sfxvolume) * 20); // Ajustar el volumen
    }
}
