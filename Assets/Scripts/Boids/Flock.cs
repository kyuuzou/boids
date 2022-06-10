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

    [SerializeField]
    private Grid grid;
    
    private List<Boid> boids = new ();
    private Dictionary<int, Boid> boidByIdentifier = new ();
    private TestSubjectDecorator testSubjectDecorator;
 
    private void LimitSpeed(Boid boid) {
        float speed = boid.Velocity.magnitude;

        if (speed > this.settings.MaximumSpeed) {
            boid.Velocity = (boid.Velocity / speed) * this.settings.MaximumSpeed;
        } else if (speed < this.settings.MinimumSpeed) {
            boid.Velocity = (boid.Velocity / speed) * this.settings.MinimumSpeed;
        }
    }
    
    private void OnTotalBoidsChanged(int newTotal) {
        int currentTotal = this.boids.Count;
        
        if (newTotal > currentTotal) {
            this.SpawnBoids(newTotal - currentTotal);
        } else if (newTotal < currentTotal) {
            for (int i = newTotal; i < currentTotal; i++) {
                Boid boid = this.boids[i];
                Object.Destroy(boid.Transform.gameObject);
                this.grid.Remove(boid);
                this.boidByIdentifier.Remove(boid.Identifier);
            }
            
            this.boids.RemoveRange(newTotal, currentTotal - newTotal);
        }
    }

    private void SpawnBoids(int total) {
        Bounds bounds = this.settings.Bounds;
        
        for (int i = 0; i < total; i++) {
            Vector2 position = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
            Transform transform = Object.Instantiate<Transform>(this.boidPrefab, position, rotation, this.boidParent);
            float speed = Random.Range(this.settings.MinimumSpeed, this.settings.MaximumSpeed); 
            
            Boid boid = new Boid(transform, speed);
            this.grid.Add(boid);
            this.boids.Add(boid);
            this.boidByIdentifier[boid.Identifier] = boid;
        }
    }
    
    private void Start() {
        this.testSubjectDecorator = this.GetComponent<TestSubjectDecorator>();
        this.SpawnBoids(this.settings.TotalBoids);
        
        this.settings.TotalBoidsChanged += this.OnTotalBoidsChanged;
    }

    private void Update() {
        foreach (Boid boid in this.boids) {
            Vector2 acceleration = Vector2.zero;
            List<Boid> neighbours = this.grid.CalculateNeighbours(boid);
            
            foreach (BehaviourBase behaviour in this.behaviours) {
                acceleration += behaviour.CalculateVelocity(boid, neighbours);
            }

            acceleration *= Time.deltaTime;
            boid.Acceleration = acceleration;
            
            if (!Mathf.Approximately(acceleration.magnitude, 0.0f)) {
                Vector3 velocity = Vector3.RotateTowards(boid.Velocity, acceleration, 5.0f, this.settings.MaximumSpeed);
                velocity.z = 0.0f;
                boid.Velocity += velocity;
            }
            
            this.LimitSpeed(boid);

            boid.Position += boid.Velocity;

            if (this.settings.WrapAroundBoundingVolume) {
                this.WrapAroundBounds(boid);
            }

            boid.Transform.position = boid.Position;
            this.grid.Move(boid);
            
            boid.Transform.up = boid.Velocity.normalized;
        }

        if (this.settings.TestSubject) {
            this.testSubjectDecorator.Decorate(this.boids[0]);
        }
    }    

    private void WrapAroundBounds(Boid boid) {
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