using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU : MonoBehaviour
{
    // Start is called before the first frame update
    public GameDirecter _directer;
    public GameManager _manager;
    public CardController _Controller;
    public GameObject hand;
    int max = 0;
    public int AtkCnt = 0;
    int hirouCnt = 0;
    public AttackButton _AttackButton;
    int[] array;
    public bool bans;
    public bool mafin;
    public bool patty;
    //int serach = 0;             //�J�[�h���ʂŎg���h���[�ϐ�

    void Start()
    {
        _directer = GameObject.Find("GameDirecter").GetComponent<GameDirecter>();
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _Controller = GameObject.Find("CardController").GetComponent<CardController>();        
        hand = GameObject.Find("Canvas/enemy_hand");
    }

    //�o�K���[�g�@101 �f�ށ@�o���Y 1 OR �}�t�B���@3 �Ɓ@�p�e�B�@2�@�Ɓ@�s�N���X�@4 �Ɓ@���^�X�@6�@�Ɓ@�g�}�g�@8
    //�`�[�o�K�@104 �f�ށ@�o���Y 1 OR �}�t�B���@3�@�Ɓ@�p�e�B�@2�@�� �`�[�Y�@5
    //�g���o�K�@103 �f�ށ@�o���Y 1 OR �}�t�B���@3�@�Ɓ@�p�e�B�@2�@�Ɓ@�g�}�g�@8
    //�G�O�}�t�@102 �f�ށ@�}�t�B���@3 �Ɓ@�p�e�B�@2�@�� �G�b�O�@7
    //�n�[�t�@105 �f�ށ@�o���Y 1 OR �}�t�B���@3 �Ɓ@�p�e�B�@2
    // Update is called once per frame
    void Update()
    {
        
        if (_directer.playerattack && !_directer.Koukahatudou && _directer.EnemyFieldCardList.Length > 0 && !_directer.enemyblock)
        {
            hirouCnt = 0;
            //max = 0;
            //_AttackButton = GameObject.Find("Attack").GetComponent<AttackButton>();
            Debug.Log("�v���C���[�̍U�������m");
            if(!_directer.Koukahatudou && _directer.EnemyFieldCardList.Length > 0) //
            {
                Debug.Log("Koukahatudou�Ƃ��Ɉ�����������");
                for (int i = 0; i < _directer.EnemyFieldCardList.Length; i++)
                {
                    if (_directer.EnemyFieldCardList[i].hirou)
                    {
                        hirouCnt++;
                    }
                }
                Debug.Log("hirouCnt " + hirouCnt);
                if((_directer.EnemyFieldCardList.Length <= hirouCnt) && _directer.playerattack)     //enemyField�����ׂĔ�J��ԂȂ�
                {
                    //_AttackButton.cardObject.GetComponent<CardController>().attack = false;
                    for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
                    {
                        _directer.playerFieldCardList[i].attack = false;
                    }
                    _directer.playerattack = false;
                    _directer.enemy_life--;
                    
                    Debug.Log("�u���b�N�ł��Ȃ���");
                    Debug.Log(_directer.playerattack);
                    
                }
                else
                {
                    _directer.enemyblock = true;
                    StartCoroutine("Block");
                    Debug.Log("Block�R���[�`��");
                }
            }
        }

        array = new int[_directer.enemyHandCardList.Length];
        for (int i = 0; i < _directer.enemyHandCardList.Length; i++)  //int�^�̔z���enemy�̎�D�J�[�h��ID��ۑ�
        {
            array[i] = _directer.enemyHandCardList[i].view.cardID;
        }
    }

    public IEnumerator Block()      
    {
        yield return new WaitForSeconds(0);
        Debug.Log("Block�R���[�`�����s");
        if (_directer.EnemyFieldCardList.Length > 0)    //CPU�̃t�B�[���h��1�̈ȏヂ���X�^�[������Ƃ�
        {
            for (int i = 0; i < _directer.EnemyFieldCardList.Length; i++)
            {
                if (max < _directer.EnemyFieldCardList[i].GetComponent<CardView>().power && _directer.EnemyFieldCardList[i].hirou == false)   //�����Ƃ��p���[���������T��
                {
                    max = _directer.EnemyFieldCardList[i].GetComponent<CardView>().power;   //�����
                }
            }
            Debug.Log("max" + max);
            for (int i = 0; i < _directer.EnemyFieldCardList.Length; i++)
            {
                if (max == _directer.EnemyFieldCardList[i].GetComponent<CardView>().power && _directer.EnemyFieldCardList[i].hirou == false)  //��ԃp���[�ł������T��
                {
                    _directer.EnemyFieldCardList[i].enemyblock();   //�������炻���Ńu���b�N
                    _directer.playerattack = false;
                    break;
                }
                
            }
        }
        _directer.enemyblock = false;
        max = 0;
    }

    public void Standby()
    {
        for(int i = 0; i< _directer.EnemyKitchenCardList.Length;i++)
        {
            if (_directer.EnemyFieldCardList.Length <= 1)
            {
                _directer.EnemyKitchenCardList[i].gameObject.transform.SetParent(_manager.enemyField);
                _directer.EnemyKitchenCardList = _manager.enemyKitchen.GetComponentsInChildren<CardController>();
                _directer.EnemyFieldCardList = _manager.enemyField.GetComponentsInChildren<CardController>();
            }
            
        }
        for (int i = 0; i < _directer.EnemyFieldCardList.Length; i++)
        {
            _directer.EnemyFieldCardList[i].kaihuku_Enemy();
            _directer.EnemyFieldCardList[i].hirou = false;
        }
        bans = false;
        mafin = false;
        patty = false;
        AtkCnt = 0;
    }

    public void Main()
    {
        //�o�K���[�g�@101 �f�ށ@�o���Y 1 OR �}�t�B���@3 �Ɓ@�p�e�B�@2�@�Ɓ@�s�N���X�@4 �Ɓ@���^�X�@6�@�Ɓ@�g�}�g�@8
        //�`�[�o�K�@104 �f�ށ@�o���Y 1 OR �}�t�B���@3�@�Ɓ@�p�e�B�@2�@�� �`�[�Y�@5
        //�g���o�K�@103 �f�ށ@�o���Y 1 OR �}�t�B���@3�@�Ɓ@�p�e�B�@2�@�Ɓ@�g�}�g�@8
        //�G�O�}�t�@102 �f�ށ@�}�t�B���@3 �Ɓ@�p�e�B�@2�@�� �G�b�O�@7
        //�n�[�t�@105 �f�ށ@�o���Y 1 OR �}�t�B���@3 �Ɓ@�p�e�B�@2


        /*array = new int[_directer.enemyHandCardList.Length];
        for (int i = 0; i < _directer.enemyHandCardList.Length; i++)  //int�^�̔z���enemy�̎�D�J�[�h��ID��ۑ�
        {
            array[i] = _directer.enemyHandCardList[i].view.cardID;
            Debug.Log(array[i]);
        }*/

        if (_directer.EnemyFieldCardList.Length <= 2)       //�t�B�[���h�ɏo����J�[�h�̐���
        {
            
            HandCheck();
            Tyouri();
            Debug.Log("�`�F�b�N�P_directer.EnemyKitchenCardList.Length�@" + _directer.EnemyKitchenCardList.Length);
        }
        StartCoroutine(Change_main(7));                            //���C���^�[���I��    
        
    }
    public void kouka(int serach)
    {
        switch (serach)
        {
            case 1:
                if (!bans)
                {
                    _manager.CreateCard(2, hand.transform);   //�o���Y�̔\�͂𔭓�
                    bans = true;
                }                
                break;

            case 2:
                if (!patty)
                {
                    _manager.CreateCard(1, hand.transform);   //�p�e�B�̔\�͂𔭓�
                    patty = true;
                }
                break;
            case 3:
                if (!mafin)
                {
                    _manager.CreateCard(2, hand.transform);   //�}�t�B���̔\�͂𔭓�
                    mafin = true;
                }
                break;

            default:
                break;
        }
    }

    void Tyouri()
    {
        StartCoroutine(ChiBaga(0.5f));
        StartCoroutine(Harf(1.0f));
    }

    IEnumerator Harf(float wait)
    {
        yield return new WaitForSeconds(wait);
        _directer.EnemyKitchenCardList = _manager.enemyKitchen.GetComponentsInChildren<CardController>();
        for (int a = 0; a < _directer.EnemyKitchenCardList.Length; a++)    //�L�b�`���G���A���݂�
        {

            if (_directer.EnemyKitchenCardList[a].view.cardID == 1 || _directer.EnemyKitchenCardList[a].view.cardID == 3)        //�o���Y������Ƃ����}�t�B��������Ƃ�
            {

                for (int b = 0; b < _directer.EnemyKitchenCardList.Length; b++)
                {
                    if (_directer.EnemyKitchenCardList[b].view.cardID == 2)  //�p�e�B������Ƃ�
                    {
                        /*StartCoroutine(Create(array[a], _manager.enemyKitchen, 1));//�o���Yor�}�t�B��
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Debug.Log(_directer.enemyHandCardList[a].view.cardID);
                        StartCoroutine(Create(array[b], _manager.enemyKitchen, 2));//�p�e�B
                        kouka(_directer.enemyHandCardList[b].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        Destroy(_directer.enemyHandCardList[b].gameObject);*/
                        //StartCoroutine(Create(1, _manager.enemyField, 3));
                        YugouDestroy(105);
                        StartCoroutine(Yugou(105, _manager.enemyField, 0.2f));        //���o�[�K�[����
                        Debug.Log("���o�[�K�[");
                        break;
                    }
                    
                }
                break;
            }

        }
    }

    void Bagamu()
    {
        
        for (int a = 0; a < _directer.EnemyKitchenCardList.Length; a++)    //�L�b�`�����݂�
        {

            if (_directer.EnemyKitchenCardList[a].view.cardID == 8)        //�g�}�g������Ƃ�
            {
                for (int b = 0; b < _directer.EnemyKitchenCardList.Length; b++)
                {
                    if (_directer.EnemyKitchenCardList[b].view.cardID == 6)  //���^�X������Ƃ�
                    {
                        for (int c = 0; c < _directer.EnemyKitchenCardList.Length; c++)
                        {
                            if (_directer.EnemyKitchenCardList[c].view.cardID == 4)     //�s�N���X������Ƃ�
                            {
                                for (int d = 0; d < _directer.EnemyKitchenCardList.Length; d++)
                                {
                                    if (_directer.EnemyKitchenCardList[d].view.cardID == 1 || _directer.EnemyKitchenCardList[d].view.cardID == 3) //�o���Y���}�t�B��������Ƃ�
                                    {
                                        StartCoroutine(Yugou(101, _manager.enemyField, 3));
                                    }
                                }
                            }
                        }

                    }
                }
            }

        }
    }

    IEnumerator ChiBaga(float wait)
    {
        yield return new WaitForSeconds(wait);
        _directer.EnemyKitchenCardList = _manager.enemyKitchen.GetComponentsInChildren<CardController>();
        for (int a = 0; a < _directer.EnemyKitchenCardList.Length; a++)    //�L�b�`���G���A���݂�
        {

            if (_directer.EnemyKitchenCardList[a].view.cardID == 1 || _directer.EnemyKitchenCardList[a].view.cardID == 3 && !bans && !mafin)        //�o���Y������Ƃ����}�t�B��������Ƃ�
            {

                for (int b = 0; b < _directer.EnemyKitchenCardList.Length; b++)
                {
                    if (_directer.EnemyKitchenCardList[b].view.cardID == 2)  //�p�e�B������Ƃ�
                    {
                        for(int c = 0; c < _directer.EnemyKitchenCardList.Length; c++)
                        {
                            if (_directer.EnemyKitchenCardList[c].view.cardID == 5) //�`�[�Y������Ƃ�
                            {
                                YugouDestroy(104);
                                StartCoroutine(Yugou(104, _manager.enemyField, 0.2f));        //�`�[�o�K����

                                break;
                            }
                        }
                        break;
                    }
                }
                break;
            }
            

        }
    }

    void HandCheck()
    {
        for(int a = 0; a < _directer.enemyHandCardList.Length; a++)
        {
            Debug.Log("_directer.EnemyKitchenCardList.Length" + _directer.EnemyKitchenCardList.Length);
            if(_directer.EnemyKitchenCardList.Length < 5)
            {
                //StartCoroutine(wait(a, 1));
                switch (_directer.enemyHandCardList[a].view.cardID)
                {
                    case 1:
                        Create1(1, _manager.enemyKitchen, 1);//�o���Y
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 2:
                        Create1(2, _manager.enemyKitchen, 1);//�p�e�B
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 3:
                        Create1(3, _manager.enemyKitchen, 1);//�}�t�B��
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 4:
                        Create1(4, _manager.enemyKitchen, 1);//�s�N���X
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 5:
                        Create1(5, _manager.enemyKitchen, 1);//�`�[�Y
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 6:
                        Create1(6, _manager.enemyKitchen, 1);//���^�X
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 7:
                        Create1(7, _manager.enemyKitchen, 1);//�G�b�O
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                    case 8:
                        Create1(8, _manager.enemyKitchen, 1);//�g�}�g
                        kouka(_directer.enemyHandCardList[a].view.cardID);
                        Destroy(_directer.enemyHandCardList[a].gameObject);
                        break;
                }
                Debug.Log("�`�F�b�N_directer.EnemyKitchenCardList.Length�@" + _directer.EnemyKitchenCardList.Length);
            }
            

        }
    }

    IEnumerator wait(int a,int wait)
    {
        Debug.Log("wait");
        yield return new WaitForSeconds(wait);
        switch (_directer.enemyHandCardList[a].view.cardID)
        {
            case 1:
                Create1(array[a], _manager.enemyKitchen, 1);//�o���Y
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 2:
                Create1(array[a], _manager.enemyKitchen, 1);//�p�e�B
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 3:
                Create1(array[a], _manager.enemyKitchen, 1);//�}�t�B��
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 4:
                Create1(array[a], _manager.enemyKitchen, 1);//�s�N���X
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 5:
                Create1(array[a], _manager.enemyKitchen, 1);//�`�[�Y
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 6:
                Create1(array[a], _manager.enemyKitchen, 1);//���^�X
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 7:
                Create1(array[a], _manager.enemyKitchen, 1);//�G�b�O
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
            case 8:
                Create1(array[a], _manager.enemyKitchen, 1);//�g�}�g
                kouka(_directer.enemyHandCardList[a].view.cardID);
                Destroy(_directer.enemyHandCardList[a].gameObject);
                break;
        }
    }

    public void battle(int turn)
    {
        
        
        if(_directer.EnemyFieldCardList.Length > 0) //CPU�̃t�B�[���h�ɃJ�[�h���ꖇ�ȏ゠��Ƃ�
        {          
            EnemyAttackJudge();
        }
        else
        {
            _directer.Change_End();
        }
        
        
        /*switch(turn)
        {
            case 1:
                _directer.Change_End();
                break;
                
            case 2:
                for(int i=0;i<_directer.EnemyFieldCardList.Length;i++)
                {
                    if(_directer.EnemyFieldCardList[i].gameObject.GetComponent<CardView>().cardID == 102)
                    {
                        _directer.EnemyFieldCardList[i].enemyattack();
                    }
                }
                break;
        }*/
    }

    public void EnemyAttackJudge()
    {
        Debug.Log("EnemyAttackJudge�̎��s");
        
        if (AtkCnt >= _directer.EnemyFieldCardList.Length || _directer.EnemyFieldCardList.Length <= 0)
        {
            Debug.Log("�G���h�t�F�C�Y�˓�" + AtkCnt);
            _directer.Change_End();
            AtkCnt = 0;
        }

        int P_maxPower = 0; //�v���C���[�̍ł��p���[�̍����J�[�h�̔Ԓn
        if(_directer.playerFieldCardList.Length <= 0)
        {
            //P_maxPower = 100;
        }
        else
        {
            for (int i = 0; i < _directer.playerFieldCardList.Length; i++)
            {

                if (_directer.playerFieldCardList[i].hirou)
                {
                    hirouCnt++;
                    P_maxPower++;

                    continue;
                }

                if(P_maxPower == i) 
                {
                    continue;
                }
                if ((_directer.playerFieldCardList[P_maxPower].view.power < _directer.playerFieldCardList[i].view.power) && _directer.playerFieldCardList[i].hirou == false) //�U���͂��ł������J�[�h��T��
                {
                    P_maxPower = i;   //�o�^                
                }
            }
            
            max = 0;
        }

        int maxPower = 0;       //�ł��p���[�̍����J�[�h�̔Ԓn���i�[����

        
        for (int i = 0; i < _directer.EnemyFieldCardList.Length; i++)   //CPU�̃t�B�[���h�̃J�[�h�����Ă���
        {
            if (_directer.EnemyFieldCardList[maxPower].hirou)
            {
                maxPower++;
                
                continue;
            }
            if ((_directer.EnemyFieldCardList[maxPower].view.power < _directer.EnemyFieldCardList[i].view.power) && _directer.EnemyFieldCardList[i].hirou == false) //�U���͂��ł������J�[�h��T��
            {
                maxPower = i;   //�o�^                
            }

        }


        if (!_directer.EnemyFieldCardList[maxPower].hirou)    //�J�[�h����J��Ԃ���Ȃ��Ȃ�U��
        {
            if(_directer.playerFieldCardList.Length == 0)   //�v���C���[�t�B�[���h���O�̂Ƃ�
            {
                Debug.Log("���C���[�t�B�[���h���O�̂Ƃ�");
                _directer.EnemyFieldCardList[maxPower].enemyattack();
                _directer.EnemyFieldCardList[maxPower].hirou = true;
                AtkCnt++;
                P_maxPower = 0;
                Debug.Log("AtkCnt" + AtkCnt);
            }
            else if (_directer.playerFieldCardList.Length == hirouCnt)   //�v���C���[�t�B�[���h���S�Ĕ�J��Ԃ̂Ƃ�
            {
                Debug.Log("�v���C���[�t�B�[���h���S�Ĕ�J��Ԃ̂Ƃ�");
                _directer.EnemyFieldCardList[maxPower].enemyattack();
                _directer.EnemyFieldCardList[maxPower].hirou = true;
                AtkCnt++;
                P_maxPower = 0;
                Debug.Log("AtkCnt" + AtkCnt);
            }
            else if (_directer.playerFieldCardList[P_maxPower].view.power�@<= _directer.EnemyFieldCardList[maxPower].view.power) //�A�^�b�N���đ��ł��ȏ�̂Ƃ�
            {
                Debug.Log("�A�^�b�N���đ��ł��ȏ�̂Ƃ�");
                _directer.EnemyFieldCardList[maxPower].enemyattack();
                _directer.EnemyFieldCardList[maxPower].hirou = true;
                AtkCnt++;
                Debug.Log("AtkCnt" + AtkCnt);
                maxPower = 0;
                P_maxPower = 0;
            }
            else if(_directer.playerFieldCardList[P_maxPower].view.power > _directer.EnemyFieldCardList[maxPower].view.power)
            {
                Debug.Log("maxPower" + maxPower);
                Debug.Log("P_maxPower" + P_maxPower);
                Debug.Log("_directer.EnemyFieldCardList[maxPower].view.power" + _directer.EnemyFieldCardList[maxPower].view.power);
                Debug.Log("_directer.playerFieldCardList[P_maxPower].view.power" + _directer.playerFieldCardList[P_maxPower].view.power);
                Debug.Log("_directer.playerFieldCardList[P_maxPower].view.hirou" + _directer.playerFieldCardList[P_maxPower].hirou);
                Debug.Log("��");
                maxPower = 0;
                P_maxPower = 0;
                _directer.Change_End();
            }
            
            
        }
        else
        {
            Debug.Log("����... �Ȃ�����...");
            AtkCnt = 0;
            _directer.Change_End();

        }
        maxPower = 0;
        hirouCnt = 0;
    }

    IEnumerator Create(int id,Transform place, int wait)
    {
        yield return new WaitForSeconds(wait);
        _manager.CreateCard(id, place);
    }

    void Create1(int id, Transform place, int wait)
    {
        _manager.CreateCard(id, place);
        _directer.EnemyKitchenCardList = _manager.enemyKitchen.GetComponentsInChildren<CardController>();
        Debug.Log("���˂݁[��������:" + _directer.EnemyKitchenCardList.Length);
    }

    void YugouDestroy(int id)
    {
        switch (id)
        {
            case 105:
                for (int a = 0; a < _directer.EnemyKitchenCardList.Length; a++)
                {
                    if (_directer.EnemyKitchenCardList[a].view.cardID == 1 || _directer.EnemyKitchenCardList[a].view.cardID == 3)
                    {
                        Destroy(_directer.EnemyKitchenCardList[a].gameObject);
                        for (int b = 0; b < _directer.EnemyKitchenCardList.Length; b++)
                        {
                            if (_directer.EnemyKitchenCardList[b].view.cardID == 2)
                            {
                                Destroy(_directer.EnemyKitchenCardList[b].gameObject);
                                break;
                            }
                        }
                        break;
                    }

                }
                break;

            case 104:
                for (int a = 0; a < _directer.EnemyKitchenCardList.Length; a++)
                {
                    if (_directer.EnemyKitchenCardList[a].view.cardID == 1 || _directer.EnemyKitchenCardList[a].view.cardID == 3)
                    {
                        Destroy(_directer.EnemyKitchenCardList[a].gameObject);
                        for (int b = 0; b < _directer.EnemyKitchenCardList.Length; b++)
                        {
                            if (_directer.EnemyKitchenCardList[b].view.cardID == 2)
                            {
                                Destroy(_directer.EnemyKitchenCardList[b].gameObject);
                                for (int c = 0; c < _directer.EnemyKitchenCardList.Length; c++)
                                {
                                    if (_directer.EnemyKitchenCardList[c].view.cardID == 5)
                                    {
                                        Destroy(_directer.EnemyKitchenCardList[c].gameObject);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }

                }
                break;

        }
        _directer.EnemyKitchenCardList = _manager.enemyKitchen.GetComponentsInChildren<CardController>();
        _directer.EnemyFieldCardList = _manager.enemyField.GetComponentsInChildren<CardController>();
    }
    IEnumerator Yugou(int id, Transform place, float wait)
    {   
        yield return new WaitForSeconds(wait);
        /*for (int i = 0; i < _directer.EnemyKitchenCardList.Length; i++)
        {
            Destroy(_directer.EnemyKitchenCardList[i].gameObject);
        }*/
        _manager.CreateCard(id, place);
    }

    IEnumerator Change_main(int time)
    {
        yield return new WaitForSeconds(time);
        _directer.Change_Battle();
    }

    IEnumerator enemy_attack()
    {
        _directer.enemyattack = true;
        yield return new WaitForSeconds(0.1f);
    }
}
