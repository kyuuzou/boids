using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourBase : ScriptableObject {

    [field: SerializeField]
    protected Settings Settings { get; private set; }
    
    public abstract Vector2 CalculateVelocity(Boid boid, List<Boid> neighbours);
}
