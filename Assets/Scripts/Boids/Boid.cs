using UnityEngine;

using Random = UnityEngine.Random;

public class Boid {
    public int Identifier { get; }
    public Transform Transform { get; }
    public Vector3 Acceleration { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector2Int Cell { get; set; }
    
    private static int LastIdentifier = 0;
    
    public Boid(Transform transform, float speed) {
        Boid.LastIdentifier++;
        
        this.Identifier = Boid.LastIdentifier;
        this.Transform = transform;
        this.Position = transform.position;
        this.Velocity = transform.up * speed;
        this.Acceleration = Vector3.zero;
        this.Cell = new Vector2Int(int.MinValue, int.MinValue);

        Color randomGreenish = Random.ColorHSV(0.2f, 0.46f, 0.5f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f);
        this.SetColor(randomGreenish);

        transform.name = $"Boid {Boid.LastIdentifier}";
    }
        
    public void SetColor(Color color) {
        this.Transform.GetComponent<MeshRenderer>().material.color = color;
    }
}
