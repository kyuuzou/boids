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

        Vector3 perceivedCenter = Vector3.zero;

        // TODO: this will not scale, needs to take into account only the closest neighbours (maybe a quadtree)
        foreach (Boid otherBoid in this.boids) {
            if (boid.Transform != otherBoid.Transform) {
                perceivedCenter += otherBoid.Position;
            }
        }

        perceivedCenter /= (this.boids.Count - 1);
        return (perceivedCenter - boid.Position) / 100.0f;
    }
    
    private Vector3 CalculateVelocityMatching(ref Boid boid) {
        if (! this.VelocityMatching) {
            return Vector3.zero;
        }
        
        return Vector3.zero;
    }

    private void LimitSpeed(ref Boid boid) {
        if (Mathf.Abs(boid.Velocity.magnitude) > this.maximumVelocity) {
            boid.Velocity = boid.Velocity.normalized * this.maximumVelocity;
        }
    }
    
    private void MoveBoids() {
        Vector3 avoidance, matching, centering;

        for (int i = 0; i < this.boids.Count; i ++) {
            Boid boid = boids[i];
            
            avoidance = this.CalculateCollisionAvoidance(ref boid);
            matching = this.CalculateVelocityMatching(ref boid);
            centering = this.CalculateFlockCentering(ref boid);
            
            boid.Velocity += (avoidance + matching + centering) * Time.deltaTime;
            this.LimitSpeed(ref boid);
            boid.Position += boid.Velocity;

            this.WrapAroundBounds(ref boid);

            boid.Transform.position = boid.Position;
            boid.Transform.up = boid.Velocity;
            boids[i] = boid;
        }
    }
    
    private void SpawnBoids() {
        this.boids = new List<Boid>();
        
        for (int i = 0; i < this.totalBoids; i++) {
            Vector3 position = Random.insideUnitCircle * this.bounds.extents.x;
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
            Transform transform = Object.Instantiate<Transform>(this.boidPrefab, position, rotation, this.boidParent);
            
            Color randomYellowish = Random.ColorHSV(0.1f, 0.3f, 0.5f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f);
            transform.GetComponent<MeshRenderer>().material.color = randomYellowish;

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

    private void WrapAroundBounds(ref Boid boid) {
        if (! this.bounds.Contains(boid.Position)) {
            Vector3 position = boid.Position;

            if (position.x < this.bounds.min.x || position.x > this.bounds.max.x) {
                position.x = (this.bounds.extents.x - 0.01f) * Mathf.Sign(position.x) * -1.0f;
            }

            if (position.y < this.bounds.min.y || position.y > this.bounds.max.y) {
                position.y = (this.bounds.extents.y - 0.01f) * Mathf.Sign(position.y) * -1.0f;
            }

            boid.Position = position;
        }
    }
}
