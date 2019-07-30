using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _cameraPosition;
    [SerializeField] private int _maxYPosition, _minYPosition, _minZPosition;
    
    void Start()
    {
        _transform = GetComponent<Transform>();
        _cameraPosition = _transform.position;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") !=0)
        {
            _cameraPosition.y -= Input.GetAxis("Mouse ScrollWheel")*20f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _cameraPosition.x -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _cameraPosition.x += 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            _cameraPosition.z += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _cameraPosition.z -= 1f;
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position,_cameraPosition) > 0.05f)
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 _vectorNewCameraPosition = new Vector3();
        _vectorNewCameraPosition.x = Mathf.SmoothStep(_transform.position.x, _cameraPosition.x, 5 * Time.fixedDeltaTime);
        _vectorNewCameraPosition.z = Mathf.SmoothStep(_transform.position.z, _cameraPosition.z, 5 * Time.fixedDeltaTime);
        _vectorNewCameraPosition.y = Mathf.SmoothStep(_transform.position.y, _cameraPosition.y, 5* Time.fixedDeltaTime);

        float borderCamera = (40 * (_maxYPosition - _transform.position.y)) / (_maxYPosition - _minYPosition);

        SetBoundary(_maxYPosition, _minYPosition, (int)vectorSetBoundary.UpDown);
        SetBoundary(_minZPosition, borderCamera, (int)vectorSetBoundary.BackForth);
        SetBoundary(-borderCamera, borderCamera, (int)vectorSetBoundary.LeftRight);
        _transform.position = _vectorNewCameraPosition;
    }
    
    private void SetBoundary(float leftBoundary, float rightBoundary, int vectorSet)
    {
        Vector3 newPositionCamera = _transform.position;
        if (vectorSet == (int)vectorSetBoundary.UpDown)
        {
            if (_transform.position.y > leftBoundary)
            {
                newPositionCamera.y = leftBoundary;
                _cameraPosition.y = leftBoundary;
            }
            else if (_transform.position.y < rightBoundary)
            {
                newPositionCamera.y = rightBoundary;
                _cameraPosition.y = rightBoundary;
            }
        }
        else if (vectorSet == (int)vectorSetBoundary.LeftRight)
        {
            if (_transform.position.x < leftBoundary)
            {
                newPositionCamera.x = leftBoundary;
                _cameraPosition.x = leftBoundary;
            }
            else if (_transform.position.x > rightBoundary)
            {
                newPositionCamera.x = rightBoundary;
                _cameraPosition.x = rightBoundary;
            }
        }
        else if (vectorSet == (int)vectorSetBoundary.BackForth)
        {
            if (_transform.position.z < leftBoundary)
            {
                newPositionCamera.z = leftBoundary;
                _cameraPosition.z = leftBoundary;
            }
            else if (_transform.position.z > rightBoundary)
            {
                newPositionCamera.z = rightBoundary;
                _cameraPosition.z = rightBoundary;
            }
        }
        _transform.position = newPositionCamera;
    }

    enum vectorSetBoundary
    {
        UpDown,
        LeftRight,
        BackForth
    }
}