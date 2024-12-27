using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton para asegurar que solo haya uno
    public AudioMixer audioMixer;

    void Awake()
    {
        // Asegurarse de que el objeto no se destruya entre escenas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
