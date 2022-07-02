using Utils;
using UnityEngine;

namespace Game.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterRotation : MonoBehaviour
    {
        [SerializeField] private float friction = 0.75f;
        [SerializeField] private float offset = 0.5f;
        [SerializeField] private Transform playerModel;

        private float _force;
        private float _clampedValue;
        private float _yRotation;
        private Rigidbody _rigidbody;


        public void BeginStretch()
        {
            _clampedValue = transform.eulerAngles.y.FixAngle();
        }


        public void Stretch(float force)
        {
            if (!_rigidbody.isKinematic) return;
            _force = Mathf.Clamp(_force + force, -offset,offset);
            playerModel.transform.localPosition = new Vector3(_force, 0, playerModel.transform.localPosition.z);
        }


        public void AddForce()
        {
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.AddTorque(0, _force / friction, 0, ForceMode.Impulse);
        }

        private Quaternion ClampRotationAroundYAxis(Quaternion quat)
        {
            quat.x /= quat.w;
            quat.y /= quat.w;
            quat.z /= quat.w;
            quat.w = 1.0f;

            _yRotation = 2.0f * Mathf.Rad2Deg * Mathf.Atan(quat.y);
            _yRotation = Mathf.Clamp(_yRotation, _clampedValue - offset.FixAngle(),
                (_clampedValue + offset).FixAngle());
            quat.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * _yRotation);
            return quat;
        }
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}