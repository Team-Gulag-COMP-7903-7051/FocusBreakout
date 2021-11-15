using UnityEngine;

public class RotateKey : MonoBehaviour {

    private float _speed = 30f;

    void Update() {
        float rotAmount = _speed * Time.deltaTime;
        transform.Rotate(0, rotAmount, 0, Space.Self);
    }
}
