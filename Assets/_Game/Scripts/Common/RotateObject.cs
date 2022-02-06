using UnityEngine;

namespace Fps.Common
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] protected float _spinSpeed = 1f;
        [SerializeField] protected Vector3 _rotationDirection = new Vector3();
        [SerializeField] bool IsDependOnParent = true;

        public float SpinSpeed { get { return _spinSpeed; } set { _spinSpeed = value; } }
        public Vector3 RotationDirection { get { return _rotationDirection; } set { _rotationDirection = value; } }

        public bool DependOnParent
        {
            set { IsDependOnParent = value; }
            get { return IsDependOnParent; }
        }
        private Quaternion currentRotation;
        private void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _rotationDirection.Normalize();
            currentRotation = transform.rotation;
        }

        private void Update()
        {
            if (IsDependOnParent)
            {
                transform.Rotate(_rotationDirection * _spinSpeed);
            }
            else
            {
                currentRotation *= Quaternion.Euler(_rotationDirection * Time.deltaTime * _spinSpeed);

                transform.rotation = currentRotation;
            }
        }
    }
}