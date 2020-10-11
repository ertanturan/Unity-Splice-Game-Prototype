using UnityEngine;

using LightDev;

namespace MeshSlice
{
  public class SoundManager : MonoBehaviour
  {
    public AudioSource source;

    [Header("Clips")]
    public AudioClip sliceClip;

    private void Awake()
    {
      Events.SuccessfulSlice += OnSuccessfulSlice;
    }

    private void OnDestroy()
    {
      Events.SuccessfulSlice -= OnSuccessfulSlice;
    }

    private void OnSuccessfulSlice(int left, int right) => PlaySound(sliceClip);

    private void PlaySound(AudioClip clip)
    {
      if (source == null || clip == null) return;

      source.clip = clip;
      source.Play();
    }
  }
}
