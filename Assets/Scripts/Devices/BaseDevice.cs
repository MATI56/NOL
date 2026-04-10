using UnityEngine;

public abstract class BaseDevice<T> : MonoBehaviour where T : BaseDeviceState
{
    public T CurrentState { get; protected set; }
    public bool IsOn { get; protected set; }
    public virtual bool IsStateCorrect(T state)
    {
        return false;
    }
    public void SetIsOn(bool isOn)
    {
        IsOn = isOn;
    }
}

public abstract class BaseDeviceState
{
    public bool IsRequired = false;
}