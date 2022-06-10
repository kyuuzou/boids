using System.Collections.Generic;
using UnityEngine;

public class FlockCentering : BehaviourBase {
    
    [field: SerializeField]
    public float Attraction { get; set; } = 0.02f;

    [field: SerializeField]
    public float Weight { get; set; } = 1.0f;

    public override Vector2 CalculateVelocity(Boid boid, List<Boid> neighbours) {
        if (Mathf.Approximately(this.Weight, 0.0f)) {
            return Vector3.zero;
        }

        Vector3 perceivedCenter = Vector3.zero;
        int perceivedBoids = 0;

        foreach (Boid neighbour in neighbours) {
            perceivedCenter += neighbour.Position;
            perceivedBoids ++;
        }

        if (perceivedBoids == 0) {
            return Vector3.zero;
        }
        
        perceivedCenter /= (perceivedBoids);
        
        return (perceivedCenter - boid.Position) * this.Attraction * this.Weight;
    }
}
