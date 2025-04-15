using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Radio : MonoBehaviour
{
    [SerializeField] private RadioDisplay display;
    [SerializeField] private RadioTrackSO[] tracks;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RadioInteractionHandler radioInteractionHandler;
    private int currentTrackIndex = 0;

    private void Start()
    {
        Focus();
        UpdateDisplay();
    }

    public void NextFM()
    {
        currentTrackIndex++;
        if (currentTrackIndex >= tracks.Length)
        {
            currentTrackIndex = 0;
        }
        RefreshTrack();
    }
    public void PreviousFM()
    {
        currentTrackIndex--;
        if (currentTrackIndex < 0)
        {
            currentTrackIndex = tracks.Length - 1;
        }
        RefreshTrack();
    }

    private void RefreshTrack()
    {
        UpdateDisplay();
        PlayCurrentTrack();
    }

    private void UpdateDisplay()
    {
        display.UpdateDisplay(tracks[currentTrackIndex].TrackName, tracks[currentTrackIndex].RadioStationFrequency);
    }

    public void Focus()
    {
        radioInteractionHandler.enabled = true;
        PlayCurrentTrack();
    }

    private void PlayCurrentTrack()
    {
        audioSource.Stop();
        audioSource.clip = tracks[currentTrackIndex].AudioClip;
        audioSource.volume = tracks[currentTrackIndex].Volume;
        audioSource.Play();
    }

    private void StopCurrentTrack()
    {
        audioSource.Stop();
    }
    public void Unfocus()
    {
        radioInteractionHandler.enabled = false;
        StopCurrentTrack();
    }
}
