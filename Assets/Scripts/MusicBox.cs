using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{

    [SerializeField] private float _volumeRaisePerParticle;
    [SerializeField] private float _volumeDecayPerSecond;
    [SerializeField] private float _volumeDecayDelay;
    
    [SerializeField] private MeshRenderer[] _volumeBars;
    [SerializeField] private Color _enabledColor;
    [SerializeField] private Color _disabledColor;

    private float _volume;
    private float _startDecayTime;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Simulating the volume decay after a certain time span
        if(Time.time > _startDecayTime)
        {
           _volume = Mathf.Clamp01(_volume - _volumeDecayPerSecond * Time.deltaTime);
        }

        _audioSource.volume = _volume;

        UpdateRenderers();

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _volume += _volumeRaisePerParticle;

        //clamp volume between two values (0,1)
        //_volume = Mathf.Clamp(_volume + _volumeRaisePerParticle, 0, 1);

        //same as above, only shorter
        _volume = Mathf.Clamp01(_volume + _volumeRaisePerParticle);

        //set a maximum volume of 1 via Mathf.Min method-->
        //_volume = Mathf.Min(_volume + _volumeRaisePerParticle, 1);

        /*
        set volume to 1 automatically when it goes above the threshhold-->
        
        if (_volume > 1)
        {
            _volume = 1;
        }
        */

        _startDecayTime = Time.time + _volumeDecayDelay;
    }

    private void UpdateRenderers()
   {

       int barsToEnable = Mathf.FloorToInt(_volumeBars.Length * _volume);

        for (int i = 0; i < _volumeBars.Length; i++)
        {
            if(i < barsToEnable)
            {
                _volumeBars[i].material.color = _enabledColor;
            }
            else
            {
                _volumeBars[i].material.color = _disabledColor;
            }
            
        }

   }

}
