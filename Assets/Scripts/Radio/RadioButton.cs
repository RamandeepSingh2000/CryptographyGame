using UnityEngine;
using UnityEngine.Events;

public class RadioButton : MonoBehaviour
{
    [SerializeField] private RadioButtonAction action;
    public UnityEvent<RadioButtonAction> onButtonPressed;

    public void PerformAction()
    {
        onButtonPressed?.Invoke(action);
    }
}
public enum RadioButtonAction
{
    Next,
    Previous
}
