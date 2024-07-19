using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    [SerializeField, Header("éãç∑å¯â "), Range(0, 1)]
    private float _parallaxEffect;

    private GameObject _camera;
    private float _length;
    private float _lengthY;
    private float _startPosX;
    private float _startPosY;
    // Start is called before the first frame update
    void Start()
    {
        _startPosX = transform.position.x;
        _startPosY = transform.position.y;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
        _lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
        _camera = Camera.main.gameObject;
    }

    void Update()
    {
     
    }

    private void FixedUpdate()
    {
        _ParallaxX();
        _ParallaxY();
    }
    private void _ParallaxX()
    {
        float temp = _camera.transform.position.x * (1 - _parallaxEffect);
        float dist = _camera.transform.position.x * _parallaxEffect;

        transform.position = new Vector3(_startPosX + dist, transform.position.y, transform.position.z);

        if (temp > _startPosX + _length)
        {
            _startPosX += _length;
        }
        else if (temp < _startPosX - _length)
        {
            _startPosX -= _length;
        }
    }
    private void _ParallaxY()
    {
        float temp = _camera.transform.position.y * (1 - _parallaxEffect);
        float dist = _camera.transform.position.y * _parallaxEffect;

        transform.position = new Vector3(transform.position.x, _startPosY + dist, transform.position.z);

        if (temp > _startPosY + _lengthY)
        {
            _startPosY += _lengthY;
        }
        else if (temp < _startPosY - _lengthY)
        {
            _startPosY -= _lengthY;
        }
    }
}
