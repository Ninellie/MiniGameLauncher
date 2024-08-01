using DriverGame.Input;
using UnityEngine;

namespace DriverGame
{
    public class Vehicle : MonoBehaviour
    {
        [SerializeField] private VehicleStats stats = new();
        [SerializeField] private VehiclePhysics physics = new();
        [SerializeField] private VehicleGroundDetection groundDetection = new();

        /// <summary>
        /// The input manager collects the input from all input sources, which are on the vehicle
        /// </summary>
        public readonly VehicleInputManager inputManager = new();

        /// <summary>
        /// Gets the stts of the vehicle after all powerUps have been applied to its base stats
        /// </summary>
        public VehicleStats FinalStats => stats;

        /// <summary>
        /// Gets the rigidbody of the vehicle
        /// </summary>
        public Rigidbody Rigidbody { get; private set; }

        /// <summary>
        /// Gets the input applied to the vehicle
        /// </summary>
        public Vector2 Input => inputManager.Input;

        /// <summary>
        /// Returns whether the vehicle is grounded or not
        /// </summary>
        public bool IsGrounded => groundDetection.IsGrounded;

        /// <summary>
        /// Returns whether the vehicle is allowed to move or not
        /// </summary>
        public bool CanMove { get; set; } = true;

        /// <summary>
        /// The amount of the vehicles velocity going forward
        /// </summary>
        private float ForwardSpeed => Vector3.Dot(Rigidbody.velocity, transform.forward);

        /// <summary>
        /// Returns the forward speed relative to the vehicles max speed. The returned value is in range [-1, 1] 
        /// </summary>
        public float NormalizedForwardSpeed
        {
            get => (Mathf.Abs(ForwardSpeed) > 0.1f) ? ForwardSpeed / FinalStats.maxSpeed : 0.0f;
        }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();

            inputManager.Initialize(this);
            groundDetection.Initialize(transform);
        }

        /// <summary>
        /// Apply lateral friction, steering and acceleration
        /// </summary>
        protected virtual void FixedUpdate()
        {
            CollectInput();

            PerformGroundCheck();

            ApplyGravity();

            ApplyLateralFriction();

            ApplySteering();

            ApplyAcceleration();
        }

        /// <summary>
        /// Collect the input from all input sources
        /// </summary>
        protected virtual void CollectInput()
        {
            inputManager.CollectInput();
        }

        /// <summary>
        /// Checks whether the vehicle is grounded or not
        /// </summary>
        protected virtual void PerformGroundCheck()
        {
            groundDetection.CheckGround();
        }

        /// <summary>
        /// Applies gravity to the vehicle
        /// </summary>
        protected virtual void ApplyGravity()
        {
            var factor = groundDetection.IsGrounded ? physics.Gravity : physics.FallGravity;

            Rigidbody.AddForce(-factor * Vector3.up, ForceMode.Acceleration);
        }

        /// <summary>
        /// Applies side friction to the vehicle. 
        /// The amount of added force defines how much the vehicle drifts while turning.
        /// </summary>
        protected virtual void ApplyLateralFriction()
        {
            if (!IsGrounded) return;
            // Calculate how much the vehicle is moving left or right
            var lateralSpeed = Vector3.Dot(Rigidbody.velocity, transform.right);

            //Calculate the desired amount of friction to apply to the side of the vehicle.
            
            var lateralFriction = -transform.right * (lateralSpeed / Time.fixedDeltaTime) * FinalStats.grip;

            Rigidbody.AddForce(lateralFriction, ForceMode.Acceleration);
        }
      
        /// <summary>
        /// Applies the steering to the vehicle by adding rotation force
        /// </summary>
        protected virtual void ApplySteering()
        {
            if (!IsGrounded || !CanMove) return;
            
            var steeringPower = Input.x * FinalStats.steeringPower;

            // The vehicle should only rotate if it is actually moving forward or backwards, therefore we
            // will multiply it by the forward speed and a coefficient
            var speedFactor = ForwardSpeed * 0.075f;

            steeringPower = Mathf.Clamp(steeringPower * speedFactor, -FinalStats.steeringPower, FinalStats.steeringPower);

            var rotationTorque = steeringPower - Rigidbody.angularVelocity.y;

            Rigidbody.AddRelativeTorque(0f, rotationTorque, 0f, ForceMode.VelocityChange);
        }

        /// <summary>
        /// Applies the movement acceleration to the vehicle
        /// </summary>
        protected virtual void ApplyAcceleration()
        {
            if (!IsGrounded || !CanMove) return;
            
            var force = FinalStats.acceleration * Input.y;

            Rigidbody.AddForce(transform.forward * force, ForceMode.Acceleration);
        }

        /// <summary>
        /// Add all required components automatically, when creating a new vehicle
        /// </summary>
        public void Reset()
        {
            // Automatically add and setup a rigidbody if none exists
            if (GetComponent<Rigidbody>() == null)
            {
                var rb = gameObject.AddComponent<Rigidbody>();

                rb.interpolation = RigidbodyInterpolation.None;
                rb.useGravity = false;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                rb.mass = 2500;
                rb.angularDrag = 3;
                rb.drag = 0.05f;
            }

            // Automatically add a player input if no input source exists
            if (GetComponent<VehicleInputSource>() == null)
            {
                gameObject.AddComponent<VehiclePlayerInput>();
            }
        }

        /// <summary>
        /// Draw additional debug info in the scene view when the vehicle is selected
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            groundDetection.OnDrawGizmosSelected(transform);
        }
    }
}
