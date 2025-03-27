using UnityEngine;

public class ControlStationMonitor : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip monitorStaticNoiseClip;
    [SerializeField] AudioClip monitorActivateClip;
    [SerializeField] MeshRenderer displayMeshRenderer;
    [SerializeField] Material noiseMat;
    [SerializeField] Material desktopMat;
    [SerializeField] ControlStation controlStation;
    private void Awake()
    {
        controlStation.OnAccessGranted.AddListener(SetDisplayToWorking);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetDisplayToStaticNoise();
    }
    private void PlayStaticNoise()
    {
        audioSource.clip = monitorStaticNoiseClip;
        audioSource.loop = true;
        audioSource.spatialBlend = 1f;
        audioSource.Play();
    }
    private void PlayMonitorActivateSound()
    {
        audioSource.Stop();
        audioSource.clip = monitorActivateClip;
        audioSource.loop = false;
        audioSource.spatialBlend = 1f;
        audioSource.Play();
    }
    public void SetDisplayToStaticNoise()
    {
        displayMeshRenderer.material = noiseMat;
        PlayStaticNoise();
    }
    public void SetDisplayToWorking()
    {
        displayMeshRenderer.material = desktopMat;
        PlayMonitorActivateSound();
    }
}
