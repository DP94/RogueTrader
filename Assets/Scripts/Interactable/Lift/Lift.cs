using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Lift : NetworkBehaviour
{
    [SerializeField]
    private List<Transform> _liftTargets;
    
    private Transform _targetPosition;
    [SerializeField]
    private int _index;
    private bool _ascend;

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private LiftState _liftState;
    
    [SyncVar] [SerializeField] private float _liftTimeElapsed;
    
    private AudioSource _liftSound;

    private void Awake()
    {
        _index = 0;
        _liftSound = GetComponent<AudioSource>();
    }

    public void MoveToNextTarget()
    {
        if (_index >= _liftTargets.Count - 1)
        {
            _ascend = false;
        }
        else if (_index <= _liftTargets.Count - 1 && _index <= 0)
        {
            _ascend = true;
        }

        if (_ascend)
        {
            _index++;
        }
        else
        {
            _index--;
        }
        _targetPosition = _liftTargets[_index];
        _liftState = LiftState.Moving;
        _liftSound.Play();
    }

    private void Update()
    {
        if (_targetPosition != null && _liftState == LiftState.Moving)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, _targetPosition.position, _liftTimeElapsed / _moveSpeed);
            _liftTimeElapsed += Time.deltaTime;
            
            if (Vector3.Distance(_targetPosition.position, transform.position) < 0.1f)
            {
                _liftState = LiftState.Finished;
                _liftTimeElapsed = 0f;
                _liftSound.Stop();
            }
        }
    }
}
