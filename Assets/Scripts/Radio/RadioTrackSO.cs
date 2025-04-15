using UnityEngine;

[CreateAssetMenu(fileName = "RadioTrack", menuName = "ScriptableObjects/RadioTrackSO", order = 1)]
public class RadioTrackSO : ScriptableObject
{
    [SerializeField] private float radioStationFrequency;
    [SerializeField] private string trackName;
    [SerializeField] AudioClip audioClip;

    [SerializeField] private float volume = 0.5f;

    public float RadioStationFrequency => radioStationFrequency;
    public string TrackName => trackName;
    public AudioClip AudioClip => audioClip;
    public float Volume => volume;
}
