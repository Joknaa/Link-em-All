using System;
using UnityEngine;

namespace LinkEmAll.Scripts {
    public class CameraController : MonoBehaviour {
        [SerializeField] private Transform playGround;
        [SerializeField] private bool shouldRotate;
        [SerializeField] private float rotationSpeed;
        [SerializeField] [Range(0.01f, 1.0f)] private float smoothing;

        private Vector3 _cameraOffset;

        private void Start() {
            _cameraOffset = transform.position - playGround.position;
        }

        private void Update() {
            if (Input.GetKey(KeyCode.Mouse1)) {
                Quaternion rotationAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
                _cameraOffset = rotationAngle * _cameraOffset;
            }

            Vector3 newPosition = playGround.position + _cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPosition, smoothing);

            transform.LookAt(playGround);
        }
    }
}