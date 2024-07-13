using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform PlayerTransform;

    public Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;

    public bool RotateAroundPlayer = true;

    public bool RotateMiddleMouseButton = true;

    public bool CursorLocked = true;

    public float RotationsSpeed = 5.0f;

    public float CameraPitchMin = 1.5f;

    public float CameraPitchMax = 6.5f;

    // Use this for initialization
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
        CursorLock(CursorLocked);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CursorLocked = !CursorLocked;
            CursorLock(CursorLocked);
        }
    }

    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        if (RotateAroundPlayer && CursorLocked)
        {

            float h = Input.GetAxis("Mouse X") * RotationsSpeed;
            float v = Input.GetAxis("Mouse Y") * RotationsSpeed;

            Quaternion camTurnAngle = Quaternion.AngleAxis(h, Vector3.up);

            Quaternion camTurnAngleY = Quaternion.AngleAxis(v, transform.right);

            Vector3 newCameraOffset = camTurnAngle * camTurnAngleY * _cameraOffset;

            // Limit camera pitch
            if (newCameraOffset.y < CameraPitchMin || newCameraOffset.y > CameraPitchMax)
            {
                newCameraOffset = camTurnAngle * _cameraOffset;
            }

            _cameraOffset = newCameraOffset;

        }

        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(PlayerTransform);
    }

    public void SetCameraTarget(Transform player)
    {
        PlayerTransform = player;
    }

    private void CursorLock(bool lockMode)
    {
        if(lockMode)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
        Cursor.visible = !lockMode;
    }
}
