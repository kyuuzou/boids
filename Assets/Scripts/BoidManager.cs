using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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
    private float maximumSpeed = 0.25f;

    [SerializeField]
    private float minimumSpeed = 0.2f;

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

        Vector3 close = Vector3.zero;

        foreach (Boid otherBoid in this.boids) {
            if (boid.Transform == otherBoid.Transform) {
                continue;
            }

            if (Vector2.Distance(boid.Position, otherBoid.Position) < 3.0f) {
                close += boid.Position -otherBoid.Position;
            }
        }

        return close;
    }
    
    private Vector3 CalculateFlockCentering(ref Boid boid) {
        if (! this.FlockCentering) {
            return Vector3.zero;
        }

        Vector3 perceivedCenter = Vector3.zero;
        int perceivedBoids = 0;

        // TODO: this will not scale, needs to take into account only the closest neighbours (maybe a quadtree)
        foreach (Boid otherBoid in this.boids) {
            if (boid.Transform == otherBoid.Transform || !IsWithinVisibleDistance(boid, otherBoid)) {
                continue;
            }
            
            perceivedCenter += otherBoid.Position;
            perceivedBoids ++;
        }

        if (perceivedBoids == 0) {
            return Vector3.zero;
        }
        
        perceivedCenter /= (perceivedBoids);
        return (perceivedCenter - boid.Position) / 50.0f;
    }
    
    private Vector3 CalculateVelocityMatching(ref Boid boid) {
        if (! this.VelocityMatching) {
            return Vector3.zero;
        }

        Vector3 velocityAverage = Vector3.zero;
        int perceivedBoids = 0;

        foreach (Boid otherBoid in this.boids) {
            if (boid.Transform == otherBoid.Transform || ! this.IsWithinVisibleDistance(boid, otherBoid)) {
                continue;
            }

            velocityAverage += otherBoid.Velocity;
            perceivedBoids ++;
        }

        if (perceivedBoids == 0) {
            return Vector3.zero;
        }
        
        velocityAverage /= perceivedBoids;
        
        return velocityAverage - boid.Velocity;
    }

    private bool IsWithinVisibleDistance(Boid boid, Boid otherBoid) {
        return Vector2.Distance(boid.Position, otherBoid.Position) < 10.0f;
    }
    
    private void LimitSpeed(ref Boid boid) {
        float speed = boid.Velocity.magnitude;

        if (speed > this.maximumSpeed) {
            boid.Velocity = (boid.Velocity / speed) * this.maximumSpeed;
        } else if (speed < this.minimumSpeed) {
            boid.Velocity = (boid.Velocity / speed) * this.minimumSpeed;
        }
    }
    
    private void MoveBoids() {
        Vector3 avoidance, matching, centering;

        for (int i = 0; i < this.boids.Count; i ++) {
            Boid boid = boids[i];
            
            avoidance = this.CalculateCollisionAvoidance(ref boid);
            matching = this.CalculateVelocityMatching(ref boid);
            centering = this.CalculateFlockCentering(ref boid);

            Vector2 acceleration = (avoidance + matching + centering) * Time.deltaTime;
            
            if (!Mathf.Approximately(acceleration.magnitude, 0.0f)) {
                boid.Velocity += Vector3.RotateTowards(boid.Velocity, acceleration, 5.0f, this.maximumSpeed);
                boid.Velocity.z = 0.0f;
            }
            
            this.LimitSpeed(ref boid);

            boid.Position += boid.Velocity;

            this.WrapAroundBounds(ref boid);

            boid.Transform.position = boid.Position;
            boid.Transform.up = boid.Velocity.normalized;
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
                Velocity = transform.up * Random.Range(this.minimumSpeed, this.maximumSpeed)
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
