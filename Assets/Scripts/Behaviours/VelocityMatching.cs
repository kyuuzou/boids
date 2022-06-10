using System.Collections.Generic;
using UnityEngine;

public class VelocityMatching : BehaviourBase {
    
    [field: SerializeField]
    public float Weight { get; set; } = 1.0f;

    public override Vector2 CalculateVelocity(Boid boid, List<Boid> neighbours) {
        if (Mathf.Approximately(this.Weight, 0.0f)) {
            return Vector3.zero;
        }

        Vector3 velocityAverage = Vector3.zero;
        int perceivedBoids = 0;

        foreach (Boid neighbour in neighbours) {
            velocityAverage += neighbour.Velocity;
            perceivedBoids ++;
        }

        if (perceivedBoids == 0) {
            return Vector3.zero;
        }
        
        velocityAverage /= perceivedBoids;
        
        return (velocityAverage - boid.Velocity) * this.Weight;
    }
}
