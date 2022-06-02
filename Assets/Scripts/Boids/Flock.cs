using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TestSubjectDecorator))]
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
    private TestSubjectDecorator testSubjectDecorator;
    
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
        this.testSubjectDecorator = this.GetComponent<TestSubjectDecorator>();
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
            boid.Acceleration = acceleration;
            
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

        if (this.settings.TestSubject) {
            this.testSubjectDecorator.Decorate(this.boids[0]);
        }
    }    

    private void WrapAroundBounds(ref Boid boid) {
        if (! this.settings.Bounds.Contains(boid.Position)) {
            Bounds bounds = this.settings.Bounds;
            Vector3 position = boid.Position;

            if (position.x < bounds.min.x || position.x > bounds.max.x) {
                position.x -= bounds.center.x;
                position.x = (bounds.extents.x - 0.025f) * Mathf.Sign(position.x) * -1.0f;
                position.x += bounds.center.x;
            }

            if (position.y < bounds.min.y || position.y > bounds.max.y) {
                position.y -= bounds.center.y;
                position.y = (bounds.extents.y - 0.025f) * Mathf.Sign(position.y) * -1.0f;
                position.y += bounds.center.y;
            }

            boid.Position = position;
        }
    }
}