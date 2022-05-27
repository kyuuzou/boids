using System.Collections.Generic;
using UnityEngine;

public class BoundsAvoidance : BehaviourBase {
    
    public override Vector2 CalculateVelocity(ref Boid boid, List<Boid> neighbours) {
        if (this.Settings.WrapAroundBoundingVolume) {
            return Vector3.zero;
        }
        
        float aversion = 0.5f;
        float margin = 20.0f;
        Vector3 velocity = Vector3.zero;
        Bounds bounds = this.Settings.Bounds;
        
        if (boid.Position.x < bounds.min.x + margin) {
            velocity.x = aversion;
        } else if (boid.Position.x > bounds.max.x - margin) {
            velocity.x = -aversion;
        } 

        if (boid.Position.y < bounds.min.y + margin) {
            velocity.y = aversion;
        } else if (boid.Position.y > bounds.max.y - margin) {
            velocity.y = -aversion;
        } 

        return velocity;
    }
}
