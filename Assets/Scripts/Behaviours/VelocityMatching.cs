using System.Collections.Generic;
using UnityEngine;

public class VelocityMatching : BehaviourBase {
    
    public override Vector2 CalculateVelocity(ref Boid boid, List<Boid> neighbours) {
        if (! this.Settings.VelocityMatching) {
            return Vector3.zero;
        }

        Vector3 velocityAverage = Vector3.zero;
        int perceivedBoids = 0;

        foreach (Boid neighbour in neighbours) {
            if (boid == neighbour || ! boid.CanSee(neighbour)) {
                continue;
            }

            velocityAverage += neighbour.Velocity;
            perceivedBoids ++;
        }

        if (perceivedBoids == 0) {
            return Vector3.zero;
        }
        
        velocityAverage /= perceivedBoids;
        
        return velocityAverage - boid.Velocity;
    }
}
