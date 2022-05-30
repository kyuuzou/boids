using System.Collections.Generic;
using UnityEngine;

public class BoundsAvoidance : BehaviourBase {
    
    [field: SerializeField]
    public float Aversion { get; set; } = 0.5f;

    [field: SerializeField]
    public float Margin { get; set; } = 20.0f;
    
    [field: SerializeField]
    public float Weight { get; set; } = 1.0f;

    public override Vector2 CalculateVelocity(ref Boid boid, List<Boid> neighbours) {
        if (Mathf.Approximately(this.Weight, 0.0f)) {
            return Vector3.zero;
        }
        
        Vector3 velocity = Vector3.zero;
        Bounds bounds = this.Settings.Bounds;
        
        if (boid.Position.x < bounds.min.x + this.Margin) {
            velocity.x = this.Aversion;
        } else if (boid.Position.x > bounds.max.x - this.Margin) {
            velocity.x = -this.Aversion;
        } 

        if (boid.Position.y < bounds.min.y + this.Margin) {
            velocity.y = this.Aversion;
        } else if (boid.Position.y > bounds.max.y - this.Margin) {
            velocity.y = -this.Aversion;
        } 

        return velocity * this.Weight;
    }
}
