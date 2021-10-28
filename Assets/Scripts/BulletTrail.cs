using UnityEngine;

public class BulletTrail : Hazard
{
/*    [SerializeField] private float _speed;
    private Vector3 _direction;

    public Vector3 Direction {
        get { return _direction; }
        set { _direction = value; }
    }*/
    private void Update() {
        if(Vector3.Distance(transform.position, Direction) > 5f) {
            transform.Translate(Direction * Time.deltaTime * Speed);
        }
    }
}
