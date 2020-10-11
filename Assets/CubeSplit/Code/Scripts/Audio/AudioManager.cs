using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioManager : SceneSingleton<AudioManager>
{

    private AudioSource _source;

    [SerializeField] private AudioClip _sliceSound;
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayOnce(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }

    public void PlaySlice()
    {
        PlayOnce(_sliceSound);
    }


}
