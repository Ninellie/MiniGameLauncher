using UnityEngine;

namespace DriverGame
{
    [System.Serializable]
    public class VehicleGroundDetection
    {
        [Tooltip("Which layers should be handled as ground")]
        [SerializeField] private LayerMask groundLayers = Physics.DefaultRaycastLayers;

        [Tooltip("How far to raycast when checking for ground")]
        [SerializeField] private float raycastDist = 0.25f;

        private Transform _vehicleTransform;

        /// <summary>
        /// Gets whether the vehicle has been grounded the last time CheckGround was called.
        /// </summary>
        public bool IsGrounded { get; private set; }

        public void Initialize(Transform vehicleTransform)
        {
            _vehicleTransform = vehicleTransform;
        }

        /// <summary>
        /// Checks whether the vehicle is grounded and store the value in the IsGrounded property
        /// </summary>
        public void CheckGround()
        {
            // Create a ray pointing downwards (relative to the vehicle)
            var transform = _vehicleTransform.transform;
            var ray = new Ray(transform.position, -transform.up);

            // If the ray hits a ground layer, the vehicle is grounded, otherwise not
            IsGrounded = Physics.Raycast(ray, raycastDist, groundLayers);
        }

        /// <summary>
        /// Draws a ray for debugging purposes (only in editor)
        /// </summary>
        /// <param name="transform"></param>
        public void OnDrawGizmosSelected(Transform transform)
        {
#if UNITY_EDITOR
            var direction = -transform.up;
            var length = raycastDist;

            Debug.DrawRay(transform.position, direction * length, Color.magenta);
#endif
        }
    }
}
