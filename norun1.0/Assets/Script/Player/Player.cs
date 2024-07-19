using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource;

    [SerializeField, Header("移動速度")]
    private float _movespeed;
    [SerializeField, Header("ジャンプ速度")]
    private float _jumpSpeed;
    [SerializeField, Header("梯子で上る速度")]
    private float _ladderSpeed;
    public bool IsLadder;
    [Header("踏みつけ判定の高さの割合(%)")] public float stepOnRate;

    #region//プライベート変数
    private string fallFloorTag = "FallFloor";
    private CapsuleCollider2D capcol = null;
    #endregion

    private bool _bjump;
    private bool _bladder;
    private bool _run;
    private Animator _anim;
    private Vector2 _inputDirection;
    private Rigidbody2D _rigid;
    public LayerMask ladderLayer;
    float currentMoveSpeed;


    public bool HasKey;

    Vector3 pos;
    Transform myTransform;
    bool now_kako;
    bool now_mirai;

    GameObject CAMERA;
    int flag = 0;
    public GameObject aka;

    [SerializeField] GameObject TCanvas2Object;
    [SerializeField] GameObject clockObject;
    [SerializeField] GameObject TCanvas;
    [SerializeField] GameObject TCanvas_ima;
    [SerializeField] GameObject TCanvas_mirai;
    [SerializeField] GameObject TCanvas_kako;
    [SerializeField] GameObject Button_kako;
    [SerializeField] GameObject Button_ima;
    [SerializeField] GameObject Button_mirai;

    // Start is called before the first frame update
    void Start()
    {   
        //Componentを取得
        audioSource = GetComponent<AudioSource>();

        Application.targetFrameRate = 60;
        _rigid = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
        _bjump = false;
        _bladder = false;
        _anim = GetComponent<Animator>();
        HasKey = false;
        _run = false;
        IsLadder = false;
        pos.x = 0;
        pos.y = 0;
        pos.z = 0;
        now_kako = false;
        now_mirai = false;
        CAMERA = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        myTransform = this.transform;
        _Move();
        _LookMoveDirect();
        _HitFloor();
        

        if (Input.GetKey(KeyCode.W) && IsLadder && _inputDirection.y != 0.0f)
        {

            _rigid.velocity = new Vector2(0, _inputDirection.y) * _ladderSpeed;
            _rigid.gravityScale = 0;
            _anim.SetBool("ladder", _inputDirection.y != 0.0f);
        }
        else
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _rigid.velocity.y);
            _rigid.gravityScale = 1;

        }

    }


    public void _Move()
    {
        //_movespeed = 8;
        currentMoveSpeed = _movespeed;
        _rigid.velocity = new Vector2(_inputDirection.x * currentMoveSpeed, _rigid.velocity.y);
        if(_inputDirection.x != 0.0f)
        {
            _run = true;
        }
        else
        {
            _run = false;
        }
        _anim.SetBool("run", _run);


    }

    public void _OnMove(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();

    }

    private void _LookMoveDirect()
    {
        if(IsLadder == false){
            if (_inputDirection.x < 0.0f)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (_inputDirection.x > 0.0f)
            {
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }
        }
    }

    public void _OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || _bjump) return;

        _rigid.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);

    }

    private void _HitFloor()
    {
        int layerMask = LayerMask.GetMask("Floor");
        Vector3 rayPos = transform.position - new Vector3(0.0f, transform.lossyScale.y / 0.65f);
        Vector3 raySize = new Vector3(transform.lossyScale.x - 0.3f, 0.1f);
        RaycastHit2D rayHit = Physics2D.BoxCast(rayPos, raySize, 0.0f, Vector2.zero, 0.0f, layerMask);
        if (rayHit.transform == null)
        {
            _movespeed = 3;

            _bjump = true;
            if(!IsLadder)
            {
                _anim.SetBool("jump", _bjump);
            }
            return;
        }

        if (rayHit.transform.tag == "floor" && _bjump)
        {
            _bjump = false;
            _anim.SetBool("jump", _bjump);
        }
        else if (rayHit.transform.tag == "FallFloor" && _bjump)
        {
            _bjump = false;
            _anim.SetBool("jump", _bjump);
        }
        else if (rayHit.transform.tag == "MoveFloor" && _bjump)
        {
            _bjump = false;
            _anim.SetBool("jump", _bjump);
        }
        _movespeed = 8.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ladder")
        {
            IsLadder = true;
            _anim.SetBool("ladder", IsLadder);

        }

        // 接触したオブジェクトのtag名がEnemyの場合は
        if (collision.gameObject.tag == "EnemyController" || collision.gameObject.tag == "DeadSpace"|| collision.gameObject.tag == "toge")
        {
            flag = 1;
      
            // Playerオブジェクトがnullでないかチェック
            if (this.gameObject != null)
            {
                if(collision.gameObject.tag == "EnemyController" || collision.gameObject.tag == "DeadSpace" || collision.gameObject.tag == "toge")
                {
                    // Playerオブジェクトを消去する
                    //Destroy(this.gameObject);
                    Shake();
                    SceneManager.LoadScene("stage01");

                }
            }
            else
            {
                // デバッグログを出力して状況を確認
                Debug.Log("Playerオブジェクトが既に破棄されています");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            IsLadder = false;
            _anim.SetBool("ladder", IsLadder);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // ギズモの色を赤に設定

        // ボックスキャストのパラメーターをシーンビューに描画
        Gizmos.DrawWireCube(transform.position - new Vector3(0.0f, transform.lossyScale.y / 0.65f), new Vector3(transform.lossyScale.x - 0.3f, 0.1f));
    }

    public void _OnLadder(InputAction.CallbackContext context)
    {
        float LadderSpeed = _ladderSpeed;
        _rigid.velocity = new Vector2(_rigid.velocity.x, _inputDirection.y * LadderSpeed);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
    
        bool fallFloor = (collision.collider.tag == fallFloorTag);

        if (fallFloor)
        {
            //踏みつけ判定になる高さ
            float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));

            //踏みつけ判定のワールド座標
            float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;

            foreach (ContactPoint2D p in collision.contacts)
            {
                if (p.point.y < judgePos)
                {
                    if (fallFloor)
                    {
                        ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>();
                        if (o != null)
                        {
                            if (fallFloor)
                            {
                                o.playerStepOn = true;
                            }
                        }
                        else
                        {
                            Debug.Log("ObjectCollisionが付いてないよ!");
                        }

                    }
                }
            }
        }
    }

    public void ChangeStage_kako()
    {
       
        StartCoroutine("Change_time_kako");
        if (now_mirai)
        {
            pos.x = 1000;
        }
        else
        {
            pos.x = 500;
        }
        
        
    }

    public void ChangeStage_ima()
    {

        StartCoroutine("Change_time_ima");
        if (now_mirai)
        {
            pos.x = 500;
        }
        else
        {
            pos.x = -500;
        }
    }

    public void ChangeStage_mirai()
    {

        StartCoroutine("Change_time_mirai");
        if (now_kako)
        {
            pos.x = -1000;
        }
        else
        {
            pos.x = -500;
        }
    }

    public void Shake()
    {
        switch (flag)
        {
            case 1:
                aka.SetActive(true);
                goto case 3;
            case 3:
                CAMERA.transform.Translate(30 * Time.deltaTime, 0, 0);
                if (CAMERA.transform.position.x >= 1.0f)
                    Debug.Log("ケース３");
                    flag++; 

                break;
            case 2:
                CAMERA.transform.Translate(-30 * Time.deltaTime, 0, 0);
                if (CAMERA.transform.position.x <= -1.0f)
                    Debug.Log("ケース2");
                flag++; //aka.SetActive(false);
                break;
            case 4:
                CAMERA.transform.Translate(-30 * Time.deltaTime, 0, 0);
                if (CAMERA.transform.position.x <= 0)
                {
                    Debug.Log("ケース4");

                    flag = 0;
                    aka.SetActive(false);
                }

                break;
        }
    }

    IEnumerator Change_time_kako()
    {
        Debug.Log("Testこるーちん");
        yield return new WaitForSeconds(3.0f);
        Debug.Log("3秒たったぜ");
        myTransform.position += new Vector3(pos.x, pos.y, pos.z);
        Time.timeScale = 1f;
        now_kako = true;
        now_mirai = false;
        Stage01.kako = false;
        TCanvas.SetActive(true);
        TCanvas_ima.SetActive(false);
        TCanvas_kako.SetActive(true);
        TCanvas_mirai.SetActive(false);
        TCanvas2Object.SetActive(false);
        clockObject.SetActive(false);
        Button_kako.SetActive(false);
        Button_ima.SetActive(true);
        Button_mirai.SetActive(true);
    }

    IEnumerator Change_time_ima()
    {
        Debug.Log("Testこるーちん");
        yield return new WaitForSeconds(3.0f);
        Debug.Log("3秒たったぜ");
        myTransform.position += new Vector3(pos.x, pos.y, pos.z);
        Time.timeScale = 1f;
        now_kako = false;
        now_mirai = false;
        Stage01.kako = false;
        TCanvas.SetActive(true);
        TCanvas_ima.SetActive(true);
        TCanvas_kako.SetActive(false);
        TCanvas_mirai.SetActive(false);
        TCanvas2Object.SetActive(false);
        clockObject.SetActive(false);
        Button_kako.SetActive(true);
        Button_ima.SetActive(false);
        Button_mirai.SetActive(true);
    }

    IEnumerator Change_time_mirai()
    {
        Debug.Log("Testこるーちん");
        yield return new WaitForSeconds(3.0f);
        Debug.Log("3秒たったぜ");
        myTransform.position += new Vector3(pos.x, pos.y, pos.z);
        Time.timeScale = 1f;
        now_kako = false;
        now_mirai = true;
        Stage01.kako = false;
        TCanvas.SetActive(true);
        TCanvas_ima.SetActive(false);
        TCanvas_kako.SetActive(false);
        TCanvas_mirai.SetActive(true);
        TCanvas2Object.SetActive(false);
        clockObject.SetActive(false);
        Button_kako.SetActive(true);
        Button_ima.SetActive(true);
        Button_mirai.SetActive(false);
    }
}
