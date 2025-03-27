using UnityEngine;

public class ControlStationTester : MonoBehaviour
{
    [SerializeField] ControlStation controlStation;
    [SerializeField] int secretCode = 12345;
    private void Start()
    {
        controlStation.EnterFocus();
        controlStation.SecretCode = secretCode;
    }
}
