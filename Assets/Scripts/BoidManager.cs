using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class BoidManager : MonoBehaviour {

    [SerializeField]
    private int totalBoids = 100;

    [SerializeField]
    private float areaRadius = 100;

    [SerializeField]
    private Transform boidParent;
    
    [SerializeField]
    private Boid boidPrefab;
    
    private List<Boid> boids;

    private void SpawnBoids() {
        this.boids = new List<Boid>();
        
        for (int i = 0; i < this.totalBoids; i++) {
            Vector3 position = Random.insideUnitCircle * this.areaRadius;
            this.boids.Add(Object.Instantiate<Boid>(this.boidPrefab, position, Quaternion.identity, this.boidParent));
        }
    }
    
    private void Start() {
        this.SpawnBoids();
    }

    private void OnDrawGizmos() {
        Handles.DrawWireDisc(Vector3.zero, Vector3.forward, this.areaRadius);
    }
}
