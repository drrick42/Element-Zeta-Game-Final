using UnityEngine;
using System.Collections;

public interface ILightEmitter {

    Transform Transform { get; }
    bool Emitting { get; }
    bool CheckHit(Transform obj);
}
