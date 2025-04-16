using UnityEngine;

public class FerrisWheel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private bool isWorking;
    [SerializeField] private Rigidbody axel;
    public void Fix()
    {
        isWorking = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isWorking)
        {
            return;
        }
        
        axel.MoveRotation(axel.rotation * Quaternion.Euler(rotationSpeed * Time.fixedDeltaTime, 0, 0));
    }
}
