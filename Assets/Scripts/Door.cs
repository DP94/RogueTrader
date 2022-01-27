using Mirror;
using UnityEngine;

public class Door : NetworkBehaviour
{
    [SerializeField] private DoorState _DoorState = DoorState.Finished;

    [SerializeField] private float _doorElevation;

    [SerializeField] private float _doorOpeningTransition = 2;

    [SerializeField] private float _doorTimeElapsed;

    private Vector3 _originalDoorPosition;
    private Vector3 _target;

    private void Awake()
    {
        var position = transform.position;
        _originalDoorPosition = position;
        _target = new Vector3(position.x, position.y + _doorElevation,
            position.z);
    }

    private void Update()
    {
        if (_DoorState is DoorState.Opening or DoorState.Closing)
        {
            var position = transform.position;
            var target = _DoorState == DoorState.Opening ? _target : _originalDoorPosition;
            transform.position = Vector3.Lerp(position, target, _doorTimeElapsed / _doorOpeningTransition);
            _doorTimeElapsed += Time.deltaTime;
            if (HasFinishedMoving(transform.position, target))
            {
                _DoorState = DoorState.Finished;
                _doorTimeElapsed = 0f;
            }
        }
    }

    public DoorState DoorState
    {
        get => _DoorState;
        set => _DoorState = value;
    }

    private bool HasFinishedMoving(Vector3 currentPosition, Vector3 target)
    {
        var currentRounded = Mathf.Round(currentPosition.y);
        var targetRounded =  Mathf.Round(target.y);
        return _DoorState == DoorState.Opening && currentRounded == targetRounded ||
               _DoorState == DoorState.Closing && currentRounded == targetRounded;
    }
}