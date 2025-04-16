using System;
using Cameras;
using UnityEngine;


public class BackButtonController : MonoBehaviour
{
    [SerializeField] private ControlStation[] _controlStations;
    [SerializeField] private CamerasManager _camerasManager;
    [SerializeField] private MenuController _menuController;
    private ControlStation _currentStation = null;

    private void Start()
    {
        foreach (var controlStation in _controlStations)
            controlStation.OnEnterFocus.AddListener((() => SelectStation(controlStation)));
    }

    private void SelectStation(ControlStation station)
    {
        _currentStation = station;
    }

    public void Back()
    {
        if (_currentStation != null)
        {
            _camerasManager.ChangeToMainGameplayCamera();
        }
        else
        {
            _camerasManager.ChangeToMainMenuCamera();
            _menuController.OpenMenu();
        }
    }
}