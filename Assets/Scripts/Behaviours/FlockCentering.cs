using System.Collections.Generic;
using UnityEngine;

public class FlockCentering : BehaviourBase {
    
    public override Vector2 CalculateVelocity(ref Boid boid, List<Boid> neighbours) {
        if (! this.Settings.FlockCentering) {
            return Vector3.zero;
        }

        Vector3 perceivedCenter = Vector3.zero;
        int perceivedBoids = 0;

        foreach (Boid neighbour in neighbours) {
            if (boid == neighbour || !boid.CanSee(neighbour)) {
                continue;
            }
            
            perceivedCenter += neighbour.Position;
            perceivedBoids ++;
        }

        if (perceivedBoids == 0) {
            return Vector3.zero;
        }
        
        perceivedCenter /= (perceivedBoids);
        return (perceivedCenter - boid.Position) / 50.0f;
    }
}
