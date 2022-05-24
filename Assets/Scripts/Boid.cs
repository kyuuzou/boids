using System;
using UnityEngine;

using Random = UnityEngine.Random;

public struct Boid : IEquatable<Boid> {
    public readonly int Identifier;
    public readonly Transform Transform;
    public Vector3 Position;
    public Vector3 Velocity;

    public Boid(int identifier, Transform transform, float speed) {
        this.Identifier = identifier;
        this.Transform = transform;
        this.Position = transform.position;
        this.Velocity = transform.up * speed;

        Color randomYellowish = Random.ColorHSV(0.1f, 0.3f, 0.5f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f);
        transform.GetComponent<MeshRenderer>().material.color = randomYellowish;
        transform.name = $"Boid {identifier}";
    }
        
    public static bool operator ==(Boid left, Boid right) => Equals(left, right);
    public static bool operator !=(Boid left, Boid right) => !Equals(left, right);
        
    public bool Equals(Boid other) {
        return this.Identifier == other.Identifier;

    }
        
    public override bool Equals(object obj) {
        return obj is Boid other && this.Equals(other);
    }

    public override int GetHashCode() {
        return this.Identifier;
    }
}