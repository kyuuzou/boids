using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour {

    public bool CollisionAvoidance { get; set; } = false;
    public bool FlockCentering { get; set; } = false;
    public bool VelocityMatching { get; set; } = false;
    
    [SerializeField]
    private int totalBoids = 100;

    [SerializeField]
    private Transform boidParent;
    
    [SerializeField]
    private Transform boidPrefab;

    [SerializeField]
    private float maximumVelocity = 1.0f;

    [SerializeField]
    private SpriteRenderer boundingVolume;

    private List<Boid> boids;
    private Bounds bounds;

    private struct Boid {
        public Transform Transform;
        public Vector3 Position;
        public Vector3 Velocity;
    }
    
    private Vector3 CalculateCollisionAvoidance(ref Boid boid) {
        if (! this.CollisionAvoidance) {
            return Vector3.zero;
        }
        
        return Vector3.zero;
    }
    
    private Vector3 CalculateFlockCentering(ref Boid boid) {
        if (! this.FlockCentering) {
            return Vector3.zero;
        }

        return Vector3.zero;
    }
    
    private Vector3 CalculateVelocityMatching(ref Boid boid) {
        if (! this.VelocityMatching) {
            return Vector3.zero;
        }
        
        return Vector3.zero;
    }

    private void MoveBoids() {
        Vector3 avoidance, matching, centering;

        for (int i = 0; i < this.boids.Count; i ++) {
            Boid boid = boids[i];
            
            avoidance = this.CalculateCollisionAvoidance(ref boid);
            matching = this.CalculateVelocityMatching(ref boid);
            centering = this.CalculateFlockCentering(ref boid);
            
            boid.Velocity += (avoidance + matching + centering) * Time.deltaTime;
            boid.Position += boid.Velocity;

            boid.Transform.position = boid.Position;
            boids[i] = boid;
        }
    }
    
    private void SpawnBoids() {
        this.boids = new List<Boid>();
        
        for (int i = 0; i < this.totalBoids; i++) {
            Vector3 position = Random.insideUnitCircle * this.bounds.extents.x;
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
            Transform transform = Object.Instantiate<Transform>(this.boidPrefab, position, rotation, this.boidParent);
            
            Boid boid = new Boid {
                Position = transform.position,
                Transform = transform,
                Velocity = transform.up * this.maximumVelocity * 0.5f
            };
            
            this.boids.Add(boid);
        }
    }
    
    private void Start() {
        this.bounds = this.boundingVolume.bounds;
        this.SpawnBoids();
    }

    private void Update() {
        this.MoveBoids();
    }
}
