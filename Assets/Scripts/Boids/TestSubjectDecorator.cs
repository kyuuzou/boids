using UnityEngine;

using Gizmos = Popcron.Gizmos;

public class TestSubjectDecorator : MonoBehaviour  {
    
    public void Decorate(Boid boid) {
        Gizmos.Circle(boid.Position, 10.0f, Quaternion.identity, Color.yellow, false, 64);
        Gizmos.Line(boid.Position, boid.Position + boid.Velocity.normalized * 5.0f, Color.cyan);
        Gizmos.Line(boid.Position, boid.Position + boid.Acceleration.normalized * 5.0f, Color.yellow);
        boid.SetColor(Color.red);
    }

}
