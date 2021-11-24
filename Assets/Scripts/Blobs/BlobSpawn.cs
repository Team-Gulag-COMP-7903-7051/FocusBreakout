using System;
using UnityEngine;

// Represents a 3D space where blobs can spawn.
// BlobSpawn's scale cannot be negative and its rotation must be (0,0,0).
// Please keep in mind that parts of a blob may spawn outside of these bounds
// becuase a blob's spawn point does not account for its radius. 
public class BlobSpawn : MonoBehaviour {
    private float _volume;
    private Vector3 _position;
    private Vector3 _scale;
    private Vector3 _upperBound;
    private Vector3 _lowerBound;
    void Start() {
        CheckTranform();
        _position = transform.position;
        _scale = transform.localScale;
        _volume = _scale.x * _scale.y * _scale.z;
        _upperBound = GetUpperBound();
        _lowerBound = GetLowerBound();
    }

    public Vector3 Position {
        get { return _position; }
    }

    public Vector3 Scale {
        get { return _scale; }
    }

    public float Volume {
        get { return _volume; }
    }

    public Vector3 UpperBound {
        get { return _upperBound; }
    }

    public Vector3 LowerBound {
        get { return _lowerBound; }
    }

    // Should be called during Start()/Awake()
    // Gets the north "upper-right" vertex
    private Vector3 GetUpperBound() {
        float x = _position.x + _scale.x / 2f;
        float y = _position.y + _scale.y / 2f;
        float z = _position.z + _scale.z / 2f;
        return new Vector3(x, y, z);
    }

    // Should be called during Start()/Awake()
    // Gets the south "bottom-left" vertex
    private Vector3 GetLowerBound() {
        float x = _position.x - _scale.x / 2f;
        float y = _position.y - _scale.y / 2f;
        float z = _position.z - _scale.z / 2f;
        return new Vector3(x, y, z);
    }

    // Should be the first method that's called in this class
    // Ensures proper transform values
    private void CheckTranform() {
        if (transform.localScale.x < 0 || transform.localScale.y < 0 || transform.localScale.z < 0) {
            throw new ArgumentException("BlobSpawn's scale cannot be negative.");
        }

        if (transform.rotation.x != 0 || transform.rotation.y != 0 || transform.rotation.z != 0) {
            throw new ArgumentException("BlobSpawn's rotation must be (0,0,0).");
        }
    }
}
