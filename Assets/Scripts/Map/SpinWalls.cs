using UnityEngine;

public class SpinWalls : MonoBehaviour {

    [SerializeField] private float _rotatoPotatoSpeed = 0.35f;
    [SerializeField] private float _rotatoPotatoDegree = 1;

    void Update() {
        transform.Rotate(0, _rotatoPotatoDegree * _rotatoPotatoSpeed, 0 * Time.deltaTime);
    }
}
