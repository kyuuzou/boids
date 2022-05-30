using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : BehaviourBase {

    [field: SerializeField]
    public float Distance { get; set; } = 3.0f;

    [field: SerializeField]
    public float Weight { get; set; } = 1.0f;
    
    public override Vector2 CalculateVelocity(ref Boid boid, List<Boid> neighbours) {
        if (Mathf.Approximately(this.Weight, 0.0f)) {
            return Vector3.zero;
        }

        Vector3 close = Vector3.zero;

        foreach (Boid neighbour in neighbours) {
            if (boid == neighbour) {
                continue;
            }

            if (Vector2.Distance(boid.Position, neighbour.Position) < this.Distance) {
                close += boid.Position -neighbour.Position;
            }
        }

        return close * this.Weight;
    }
}
