using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Player _player;
    private Vector3 _initPos;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _initPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        _FollowPlayer();
    }
    private void _FollowPlayer()
    {
        // _playerがnullでないかチェック
        if (_player != null)
        {
            float y = _player.transform.position.y;
            float x = _player.transform.position.x;
            /*x = Mathf.Clamp(x, _initPos.x, Mathf.Infinity);
            y = Mathf.Clamp(y, _initPos.y, Mathf.Infinity);*/
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
