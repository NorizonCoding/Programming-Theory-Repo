using UnityEngine;
using UnityEngine.InputSystem;

namespace Vehicles
{
    [RequireComponent (typeof(Rigidbody))]
    public abstract class Vehicle : MonoBehaviour
    {
        protected float curSpeed;
        public float Speed { get { return curSpeed; } }

        [SerializeField] protected bool engineEnabled;
        public bool EngineStatus { get { return engineEnabled; } }

        protected Vector3 prevPosition;

        [SerializeField] protected VehicleData vehicleData;

        protected new Rigidbody rigidbody;

        protected InputSystem_Actions inputActions;

        // Start is called once before the first frame Update
        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            //rigidbody.mass = vehicleData.mass;

            prevPosition = transform.position;

            inputActions = new InputSystem_Actions();
        }

        // Called at the start of every fixed update
        private void CalculateSpeed()
        {
            curSpeed = (transform.position - prevPosition).magnitude/Time.fixedDeltaTime;
            prevPosition = transform.position;
        }

        protected virtual void FixedUpdate()
        {
            CalculateSpeed();
        }

        virtual protected void ToggleEngine()
        {
            engineEnabled = !engineEnabled;
        }

        protected abstract void Move();
    }
}
