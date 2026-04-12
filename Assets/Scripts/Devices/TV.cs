using DG.Tweening;
using TMPro;
using UnityEngine;

public class TV : BaseDevice<TVState>
{
    [SerializeField] private TextMeshProUGUI _channelDisplay;
    [SerializeField] private MeshRenderer _screenRenderer;
    [SerializeField] private Material _defaultScreenMaterial;
    [SerializeField] private Material _powerOffMaterial;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _powerOnAudioClip;
    [SerializeField] private AudioClip _powerOffAudioClip;
    [SerializeField] private AudioClip _defaultAudioClip;

    [SerializeField] private TVChannel[] _channels;

    [SerializeField] private AudioClip _knobSound;
    [SerializeField] private Light _screenLight;

    private bool _isPoweredOn = false;
    public void Start()
    {
        CurrentState = new TVState();
    }
    public override bool IsStateCorrect(TVState state)
    {
        Debug.Log($"Comparing states: Current - {CurrentState.IsAntennaUp}, {CurrentState.Channel} | Target - {state.IsAntennaUp}, {state.Channel}");
        return CurrentState.IsAntennaUp == state.IsAntennaUp && CurrentState.Channel == state.Channel;
    }
    public void SetPower(bool isOn)
    {
        _isPoweredOn = isOn;
        _screenLight.DOIntensity(isOn ? 0.01f : 0, 0.1f);
        _channelDisplay.gameObject.SetActive(_isPoweredOn);
        if (_isPoweredOn)
        {
            AudioSource.PlayClipAtPoint(_powerOnAudioClip, transform.position);
            CheckChannel();
        }
        else
        {
            _audioSource.Stop();
            _audioSource.clip = null;
            AudioSource.PlayClipAtPoint(_powerOffAudioClip, transform.position);
            _screenRenderer.material = _powerOffMaterial;
        }

    }
    public void SetAntenna(bool isUp)
    {
        CurrentState.IsAntennaUp = isUp;
        if(_isPoweredOn)
        {
            CheckChannel();
        }
    }
    public void SetChannel(float channel)
    {
        if(!_isPoweredOn) return;
        if (CurrentState.Channel == Mathf.RoundToInt(Mathf.Lerp(1, 12, channel))) return;

        CurrentState.Channel = Mathf.RoundToInt(Mathf.Lerp(1, 12, channel));
        _channelDisplay.SetText(CurrentState.Channel.ToString());
        AudioManager.Instance.PlaySoundRandomPitch(_knobSound);

        CheckChannel();
    }
    private void CheckChannel()
    {
        foreach (TVChannel tvChannel in _channels)
        {
            if (tvChannel.channel == CurrentState.Channel)
            {
                if (CurrentState.IsAntennaUp == tvChannel.RequiresAntennaUp)
                {
                    _audioSource.clip = tvChannel.Clip;
                    _audioSource.Play();
                    _screenRenderer.material = tvChannel.ScreenMaterial;
                    return;
                }
            }
        }
   
        if(_screenRenderer.material != _defaultScreenMaterial)
        {
            Debug.Log("No matching channel found, reverting to default.");
            _audioSource.clip = _defaultAudioClip;
            _audioSource.Play();
            _screenRenderer.material = _defaultScreenMaterial;
        }
    }
    [System.Serializable]
    private class TVChannel
    {
        [Range(1, 12)]
        public int channel = 1;
        public bool RequiresAntennaUp = false;
        public AudioClip Clip;
        public Material ScreenMaterial;
    }
}
[System.Serializable]
public class TVState : BaseDeviceState
{
    public bool IsAntennaUp;
    [Range(1, 12)]
    public int Channel;
    public TVState()
    {
        IsAntennaUp = false;
        Channel = 1;
    }
}