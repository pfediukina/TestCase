using UnityEngine;

public class Parallax : MonoBehaviour 
{
    [SerializeField] private float _parallaxEffect;

    private float _startPos;
    private float _length;

    private void Start () 
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate () 
    {
        float dist = (Camera.main.transform.position.x * _parallaxEffect);
        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

        if (dist > _startPos + _length) _startPos += _length;
        else if (dist < _startPos - _length) _startPos -= _length;
    }
}