using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : BehaviourBase {
    
    public override Vector2 CalculateVelocity(ref Boid boid, List<Boid> neighbours) {
        if (! this.Settings.CollisionAvoidance) {
            return Vector3.zero;
        }

        Vector3 close = Vector3.zero;

        foreach (Boid neighbour in neighbours) {
            if (boid == neighbour) {
                continue;
            }

            if (Vector2.Distance(boid.Position, neighbour.Position) < 3.0f) {
                close += boid.Position -neighbour.Position;
            }
        }

        return close;
    }
}
