using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;

public class Flock : MonoBehaviour {

    [Header("Behaviours")]
    [SerializeField]
    private List<BehaviourBase> behaviours;
    
    [Header("Settings")]
    [SerializeField]
    private Settings settings;

    [Header("Structure")]
    [SerializeField]
    private Transform boidParent;
    
    [SerializeField]
    private Transform boidPrefab;
    
    private List<Boid> boids;
    
    private void LimitSpeed(ref Boid boid) {
        float speed = boid.Velocity.magnitude;

        if (speed > this.settings.MaximumSpeed) {
            boid.Velocity = (boid.Velocity / speed) * this.settings.MaximumSpeed;
        } else if (speed < this.settings.MinimumSpeed) {
            boid.Velocity = (boid.Velocity / speed) * this.settings.MinimumSpeed;
        }
    }
    
    private void SpawnBoids() {
        this.boids = new List<Boid>();
        
        for (int i = 0; i < this.settings.TotalBoids; i++) {
            Vector3 position = Random.insideUnitCircle * this.settings.Bounds.extents.x;
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
            Transform transform = Object.Instantiate<Transform>(this.boidPrefab, position, rotation, this.boidParent);
            float speed = Random.Range(this.settings.MinimumSpeed, this.settings.MaximumSpeed); 
            
            Boid boid = new Boid(i, transform, speed);
            this.boids.Add(boid);
        }
    }
    
    private void Start() {
        this.SpawnBoids();
    }

    private void Update() {
        for (int i = 0; i < this.boids.Count; i ++) {
            Boid boid = boids[i];
            Vector2 acceleration = Vector2.zero;
            
            foreach (BehaviourBase behaviour in this.behaviours) {
                acceleration += behaviour.CalculateVelocity(ref boid, this.boids);
            }

            acceleration *= Time.deltaTime;
            
            if (!Mathf.Approximately(acceleration.magnitude, 0.0f)) {
                boid.Velocity += Vector3.RotateTowards(boid.Velocity, acceleration, 5.0f, this.settings.MaximumSpeed);
                boid.Velocity.z = 0.0f;
            }
            
            this.LimitSpeed(ref boid);

            boid.Position += boid.Velocity;

            if (this.settings.WrapAroundBoundingVolume) {
                this.WrapAroundBounds(ref boid);
            }

            boid.Transform.position = boid.Position;
            boid.Transform.up = boid.Velocity.normalized;
            boids[i] = boid;
        }
    }    

    private void WrapAroundBounds(ref Boid boid) {
        if (! this.settings.Bounds.Contains(boid.Position)) {
            Vector3 position = boid.Position;

            if (position.x < this.settings.Bounds.min.x || position.x > this.settings.Bounds.max.x) {
                position.x = (this.settings.Bounds.extents.x - 0.01f) * Mathf.Sign(position.x) * -1.0f;
            }

            if (position.y < this.settings.Bounds.min.y || position.y > this.settings.Bounds.max.y) {
                position.y = (this.settings.Bounds.extents.y - 0.01f) * Mathf.Sign(position.y) * -1.0f;
            }

            boid.Position = position;
        }
    }
}
