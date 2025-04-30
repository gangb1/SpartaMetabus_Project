using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour
{
    private const float BoundSize = 3.5f;           //��� ũ�Ⱚ
    private const float MovingBoundsSize = 3f;       //��� �ױ� ���� ���� ��
    private const float StackMovingSpeed = 5.0f;      //��� �ױ� �ӵ���
    private const float BlockMovingSpeed = 3.5f;       //��� �ӵ���
    private const float ErrorMargin = 0.1f;

    public GameObject originBlock = null;               //���ӿ�����Ʈ ��

    private Vector3 prevBlockPosition;                  //���� ��
    private Vector3 desiredPosition;                    //stack�� ������ ���� ����
    private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);        //���� ����

    Transform lastBlock = null;
    float blockTransition = 0f;
    float secondaryPosition = 0f;

    int stackCount = -1;
    public int Score { get { return stackCount; } }
    int comboCount = 0;

    public int combo { get { return comboCount; } }

    private int maxCombo = 0;
    public int MaxCombo { get { return maxCombo; } }

    public Color prevColor;
    public Color nextColor;

    bool isMovingX = true;

    int bestScore = 0;
    public int BestScore { get { return bestScore; } }

    int bestCombo = 0;
    public int BestCombo { get { return bestCombo; } }

    private const string BestScoreKey = "BestScore";
    private const string BestComboKey = "BestCombo";

    private bool isGameOver = true;

    // Start is called before the first frame update
    void Start()
    {
        if (originBlock == null)
        {
            Debug.Log("OriginalBlock is Null");
            return;
        }

        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        bestCombo = PlayerPrefs.GetInt(BestComboKey, 0);

        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        prevBlockPosition = Vector3.down;           //ù ��� ������ ���� ��ǥ����

        Spawn_Block();
        Spawn_Block();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (PlaceBlock())
            {
                Spawn_Block();
            }
            else
            {
                Debug.Log("GameOver");
                UpdateScore();
                isGameOver = true;
                GameOverEffect();
                UImanager.Instance.SetScoreUI();
            }
        }
        MoveBlock();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);  //���� ��ġ���� desiredPosition(����� ���̰� �� ���� ������ stack�� ��ġ)��ŭ �ʴ� stackmovingSpeed(��ŸŸ���� �־ �ε巴��)��ŭ ���󰡰� ����
    }

    bool Spawn_Block()
    {
        //������ ����� �����ϸ� �� ��ġ�� �����ؼ� �� ���� ���� ��ġ�� ���
        if (lastBlock != null)
        {
            prevBlockPosition = lastBlock.localPosition;
        }
        GameObject newBlock = null;
        Transform newTrans = null;

        //�� �������� ������� ���ο� �� ����
        newBlock = Instantiate(originBlock);

        //�� ������ ������ ���, ���� �޽��� ��� �� false ��ȯ
        if (newBlock == null)
        {
            Debug.Log("NewBlock Instatiate Failed");
            return false;
        }

        ColorChange(newBlock);

        newTrans = newBlock.transform;                      //�� ���� transform ������Ʈ�� ������
        newTrans.parent = this.transform;                   //���� �θ�� ������
        newTrans.localPosition = prevBlockPosition + Vector3.up;  //���� ����� ��ġ���� �� ĭ ���� ����
        newTrans.localRotation = Quaternion.identity;           //���� ȸ���� �ʱ�ȭ
        newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);   //Ŭ���� ũ�� ����

        stackCount++;       //���� �� �� 1 ����

        desiredPosition = Vector3.down * stackCount;     //stack�� ������ ���� ��
        blockTransition = 0f;           //�� �̵� Ʈ������ �ʱ�ȭ
        lastBlock = newTrans;           //������ ���� ���� ������ ����

        isMovingX = !isMovingX;

        UImanager.Instance.UpdateScore();

        return true;                    //�� ���� ����
    }

    Color GetRandomColor()
    {
        //RGB�� �������� �̰� ����Ƽ COLOR ����(0~1)�� �°� 255�� ����
        float r = Random.Range(100f, 250f) / 255f;
        float g = Random.Range(100f, 250f) / 255f;
        float b = Random.Range(100f, 250f) / 255f;

        // ���� Color Ĵü ��ȯ
        return new Color(r, g, b);
    }

    void ColorChange(GameObject go)
    {
        //�� ������ ���� prevColor -> nextColor�� ������ ��ȭ
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

        //������ ������Ʈ ��������
        Renderer rn = go.GetComponent<Renderer>();

        //���� ó��
        if (rn == null)
        {
            Debug.Log("Renderer is Null");
            return;
        }

        //�� ���� ����
        rn.material.color = applyColor;
        //ī�޶� ��� �� ����(����� ��/���� ��Ӱ�)
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

        // ���� ��ȯ �Ϸ� �Ǹ� ���ο� nextColor ����
        if (applyColor.Equals(nextColor) == true)
        {
            prevColor = nextColor;
            nextColor = GetRandomColor();
        }
    }
    void MoveBlock()
    {
        //�ð��� ���� �̵� ���ذ� ����(�ӵ� ����)
        blockTransition += Time.deltaTime * BlockMovingSpeed;

        //pingpong���� �պ� �̵��� ���(-BoundSize/2 ~ BoundSize/2 ����)
        float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

        // ������ ������ x���� ��� -> �¿� �̵�
        if (isMovingX)
        {
            lastBlock.localPosition = new Vector3(movePosition * MovingBoundsSize, stackCount, secondaryPosition);
        }
        else
        {
            lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * MovingBoundsSize);
        }
    }

    bool PlaceBlock()
    {
        //�����̴� ���� ��ġ�� ����
        Vector3 lastPosition = lastBlock.localPosition;

        if (isMovingX)
        {
            float deltaX = prevBlockPosition.x - lastPosition.x;  //���� ���� ���� ���� x�� �Ÿ� �� ���
            bool isNegativeNum = (deltaX < 0) ? true : false;       //�������� Ʋ�ȴ��� Ȯ��


            deltaX = Mathf.Abs(deltaX);  // �������� ���
            if (deltaX > ErrorMargin)       //������ ũ�� �ڸ���
            {
                stackBounds.x -= deltaX;    //��Ƴ��� �� ũ�� ���̱�
                if (stackBounds.x <= 0)      // �ʹ� �۾����� ���� ����
                {
                    return false;
                }
                float middle = (prevBlockPosition.x + lastPosition.x) / 2f;  //�ڸ��� �� ���� �߾� ��ġ ����
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y); // ũ�� ����(����)

                //��� ��ġ�� ������
                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.x = middle;
                lastBlock.localPosition = lastPosition = tempPosition;

                //�߷� ���� ������ ũ�� �� ������
                float rubbleHalfScale = deltaX / 2f;

                //�߸� ���� ���� ��ġ �Ի�(���⿡ ���� ��ġ �޶���)
                CreateRubble(new Vector3(
                    isNegativeNum ?
                    lastPosition.x + stackBounds.x / 2 + rubbleHalfScale        //���������� ������� �����ʿ� ���� ����            //�������� �ƴ� �߽� ��ǥ�� �����ϴ� ��
                    : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale      //�������� ������� ���ʿ� ���� ����
                    , lastPosition.y
                    , lastPosition.z
                    ),
                    new Vector3(deltaX, 1, stackBounds.y)       //������ ũ��: �߷����� ��ŭ
                    );

                comboCount = 0;
            }
            else
            {
                ComboCheck();
                //���� ���ĵǾ� ������ ���� �״�� �ױ�
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }

        }
        else // z�� ���� ó��
        {
            float deltaZ = prevBlockPosition.z - lastBlock.localPosition.z;
            bool isNegativeNum = (deltaZ < 0) ? true : false; // �е� üũ


            deltaZ = Mathf.Abs(deltaZ);
            if (deltaZ > ErrorMargin)
            {
                stackBounds.y -= deltaZ;
                if (stackBounds.y <= 0)
                {
                    return false;
                }
                float middle = (prevBlockPosition.z + lastPosition.z) / 2f;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.z = middle;
                lastBlock.localPosition = lastPosition = tempPosition;

                float rubbleHalfScale = deltaZ / 2f;
                CreateRubble(
                    new Vector3(
                        lastPosition.x,
                        lastPosition.y,
                        isNegativeNum
                        ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                        : lastPosition.z - stackBounds.y - rubbleHalfScale),
                    new Vector3(stackBounds.x, 1, deltaZ)
                    );

                comboCount = 0;
            }
            else
            {
                ComboCheck();
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }

        secondaryPosition = (isMovingX) ? lastBlock.localPosition.x : lastBlock.localPosition.z;

        return true;
    }
    void CreateRubble(Vector3 pos, Vector3 scale)
    {
        //���� �� ����(���ط� �� ������Ʈ)
        GameObject go = Instantiate(lastBlock.gameObject);
        go.transform.parent = this.transform;

        //��ġ ũ�� ȸ�� ����
        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.transform.localRotation = Quaternion.identity;

        //�߷� ����
        go.AddComponent<Rigidbody>();
        go.name = "Rubble";         // �̸� ����
    }

    void ComboCheck()
    {
        comboCount++;

        if (comboCount > maxCombo)
        {
            maxCombo = comboCount;
            if ((comboCount % 5) == 0)
            {
                Debug.Log("5 Combo Success!!");
                stackBounds += new Vector3(0.5f, 0.5f);
                stackBounds.x =
                    (stackBounds.x > BoundSize) ? BoundSize : stackBounds.x;
                stackBounds.y =
                    (stackBounds.y > BoundSize) ? BoundSize : stackBounds.y;
            }
        }
    }
    void UpdateScore()
    {
        if (bestScore < stackCount)
        {
            Debug.Log("�ְ� ���� ����");
            bestScore = stackCount;
            bestCombo = maxCombo;

            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            PlayerPrefs.SetInt(BestComboKey, bestCombo);
        }
    }
    void GameOverEffect()
    {
        int childCount = this.transform.childCount;       //�Ʒ��� ���� ��ü�� ������ ���Ѵ�.

        for (int i = 1; i < 20; i++)
        {
            if (childCount < i) break;

            GameObject go = transform.GetChild(childCount - i).gameObject;

            if (go.name.Equals("Rubble")) continue;

            Rigidbody rigid = go.AddComponent<Rigidbody>();

            rigid.AddForce(
                (Vector3.up * Random.Range(0, 10f) + Vector3.right * (Random.Range(0, 10f) - 5f)) * 100f
                );

        }
    }
    public void Restart()
    {
        int childCount = transform.childCount;

        for(int i = 0; i< childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        isGameOver = false;

        lastBlock = null;
        desiredPosition = Vector3.zero;
        stackBounds = new Vector3(BoundSize, BoundSize);

        stackCount = -1;
        isMovingX = true;
        blockTransition = 0f;
        secondaryPosition = 0f;

        comboCount = 0;
        maxCombo = 0;

        prevBlockPosition = Vector3.down;

        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        Spawn_Block();
        Spawn_Block();
    }
}

