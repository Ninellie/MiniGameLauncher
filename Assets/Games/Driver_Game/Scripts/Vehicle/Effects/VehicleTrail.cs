using UnityEngine;

namespace DriverGame.Effects
{
    [RequireComponent(typeof(TrailRenderer))]
    public class VehicleTrail : VehicleComponent
    {
        private TrailRenderer _trailRenderer;

        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            if (Vehicle.IsGrounded != _trailRenderer.emitting)
            {
                _trailRenderer.emitting = Vehicle.IsGrounded;
            }
        }
    }
}