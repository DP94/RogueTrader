using Mirror;
using UnityEngine;

public class Door : NetworkBehaviour
{
    [SyncVar] [SerializeField] private DoorState _DoorState = DoorState.Closed;

    [SerializeField] private float _doorElevation;

    [SerializeField] private float _doorOpeningTransition = 2;

    [SyncVar] [SerializeField] private float _doorTimeElapsed;

    [SerializeField]
    private GameObject _leftDoor;
    
    [SerializeField]
    private GameObject _rightDoor;

    [SerializeField]
    [Tooltip("Determines if the door should open on the Z or X axis")]
    private bool _zOpen;
    
    private AudioSource _doorSound;

    private Vector3 _originalLeftDoorPosition;
    private Vector3 _leftTarget;
    private Vector3 _originalRightDoorPosition;
    private Vector3 _rightTarget;

    private void Awake()
    {
        var leftPosition = _leftDoor.transform.position;
        _originalLeftDoorPosition = leftPosition;
        var rightPosition = _rightDoor.transform.position;
        _originalRightDoorPosition = rightPosition;

        if (_zOpen)
        {
            _leftTarget = new Vector3(leftPosition.x, leftPosition.y,
                leftPosition.z + _doorElevation);
            _rightTarget = new Vector3(rightPosition.x, rightPosition.y,
                rightPosition.z - _doorElevation);
        }
        else
        {
            _leftTarget = new Vector3(leftPosition.x + _doorElevation, leftPosition.y,
                leftPosition.z);
            _rightTarget = new Vector3(rightPosition.x - _doorElevation, rightPosition.y,
                rightPosition.z);
        }

        _doorSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_DoorState is DoorState.Opening or DoorState.Closing)
        {
            var leftPosition = _leftDoor.transform.position;
            var target = _DoorState == DoorState.Opening ? _leftTarget : _originalLeftDoorPosition;
            _leftDoor.transform.position = Vector3.Lerp(leftPosition, target, _doorTimeElapsed / _doorOpeningTransition);
            
            var rightPosition = _rightDoor.transform.position;
            var rightTarget = _DoorState == DoorState.Opening ? _rightTarget : _originalRightDoorPosition;
            _rightDoor.transform.position = Vector3.Lerp(rightPosition, rightTarget, _doorTimeElapsed / _doorOpeningTransition);            
            
            _doorTimeElapsed += Time.deltaTime;
            if (_leftDoor.transform.position == target && _rightDoor.transform.position == rightTarget)
            {
                _DoorState = _DoorState == DoorState.Opening ? DoorState.Opened : DoorState.Closed;
                _doorTimeElapsed = 0f;
            }
        }
    }

    public DoorState DoorState
    {
        get => _DoorState;
        set
        {
            _DoorState = value;
            _doorSound.Play();
        }
    }
}