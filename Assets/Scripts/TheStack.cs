using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour
{
    private const float BoundSize = 3.5f;           //블록 크기값
    private const float MovingBoundsSize = 3f;       //블록 쌓기 범위 조절 값
    private const float StackMovingSpeed = 5.0f;      //블록 쌓기 속도값
    private const float BlockMovingSpeed = 3.5f;       //블록 속도값
    private const float ErrorMargin = 0.1f;

    public GameObject originBlock = null;               //게임오브젝트 블럭

    private Vector3 prevBlockPosition;                  //이전 블럭
    private Vector3 desiredPosition;                    //stack을 내리기 위한 변수
    private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);        //스택 범위

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

        prevBlockPosition = Vector3.down;           //첫 블록 생성을 위한 좌표지정

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
        transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);  //현재 위치에서 desiredPosition(블록이 쌓이고 난 뒤의 내려온 stack의 위치)만큼 초당 stackmovingSpeed(델타타임을 넣어서 부드럽게)만큼 따라가게 만듬
    }

    bool Spawn_Block()
    {
        //마지막 블록이 존재하면 그 위치를 저장해서 새 블럭의 기준 위치로 사용
        if (lastBlock != null)
        {
            prevBlockPosition = lastBlock.localPosition;
        }
        GameObject newBlock = null;
        Transform newTrans = null;

        //블럭 프리팹을 기반으로 새로운 블럭 생성
        newBlock = Instantiate(originBlock);

        //블럭 생성에 실패한 경우, 오류 메시지 출력 후 false 반환
        if (newBlock == null)
        {
            Debug.Log("NewBlock Instatiate Failed");
            return false;
        }

        ColorChange(newBlock);

        newTrans = newBlock.transform;                      //새 블럭의 transform 컴포넌트를 가져옴
        newTrans.parent = this.transform;                   //블럭의 부모로 지정함
        newTrans.localPosition = prevBlockPosition + Vector3.up;  //이전 블록의 위치에서 한 칸 위로 쌓음
        newTrans.localRotation = Quaternion.identity;           //블럭의 회전을 초기화
        newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);   //클럭의 크기 설정

        stackCount++;       //쌓은 블럭 수 1 증가

        desiredPosition = Vector3.down * stackCount;     //stack을 내리기 위한 식
        blockTransition = 0f;           //블럭 이동 트랜지션 초기화
        lastBlock = newTrans;           //마지막 블럭을 현재 블럭으로 갱신

        isMovingX = !isMovingX;

        UImanager.Instance.UpdateScore();

        return true;                    //블럭 생성 성공
    }

    Color GetRandomColor()
    {
        //RGB값 랜덤으로 뽑고 유니티 COLOR 범위(0~1)에 맞게 255로 나눔
        float r = Random.Range(100f, 250f) / 255f;
        float g = Random.Range(100f, 250f) / 255f;
        float b = Random.Range(100f, 250f) / 255f;

        // 최종 Color 캑체 반환
        return new Color(r, g, b);
    }

    void ColorChange(GameObject go)
    {
        //블럭 개수에 따라 prevColor -> nextColor로 점진적 변화
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

        //렌더러 컴포넌트 가져오기
        Renderer rn = go.GetComponent<Renderer>();

        //예외 처리
        if (rn == null)
        {
            Debug.Log("Renderer is Null");
            return;
        }

        //블럭 색상 적용
        rn.material.color = applyColor;
        //카메라 배경 색 조정(비슷한 색/조금 어둡게)
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

        // 색상 전환 완료 되면 새로운 nextColor 설정
        if (applyColor.Equals(nextColor) == true)
        {
            prevColor = nextColor;
            nextColor = GetRandomColor();
        }
    }
    void MoveBlock()
    {
        //시간에 따라 이동 기준값 증가(속도 조절)
        blockTransition += Time.deltaTime * BlockMovingSpeed;

        //pingpong으로 왕복 이동값 계산(-BoundSize/2 ~ BoundSize/2 범위)
        float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

        // 움직임 방향이 x축일 경우 -> 좌우 이동
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
        //움직이던 블럭의 위치를 저장
        Vector3 lastPosition = lastBlock.localPosition;

        if (isMovingX)
        {
            float deltaX = prevBlockPosition.x - lastPosition.x;  //이전 블럭과 현재 블럭의 x축 거리 차 계산
            bool isNegativeNum = (deltaX < 0) ? true : false;       //왼쪽으로 틀렸는지 확인


            deltaX = Mathf.Abs(deltaX);  // 절댓값으로 계산
            if (deltaX > ErrorMargin)       //오차가 크면 자르기
            {
                stackBounds.x -= deltaX;    //살아남은 블럭 크기 줄이기
                if (stackBounds.x <= 0)      // 너무 작아지면 게임 오버
                {
                    return false;
                }
                float middle = (prevBlockPosition.x + lastPosition.x) / 2f;  //자르고 난 블럭의 중앙 위치 재계산
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y); // 크기 줄임(대입)

                //가운데 위치로 재정렬
                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.x = middle;
                lastBlock.localPosition = lastPosition = tempPosition;

                //잘려 나간 조각의 크기 반 나누기
                float rubbleHalfScale = deltaX / 2f;

                //잘린 조각 생성 위치 게산(방향에 따라 위치 달라짐)
                CreateRubble(new Vector3(
                    isNegativeNum ?
                    lastPosition.x + stackBounds.x / 2 + rubbleHalfScale        //오른쪽으로 벗어났으면 오른쪽에 조각 생성            //시작점이 아닌 중심 좌표를 생성하는 것
                    : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale      //왼쪽으로 벗어났으면 왼쪽에 조각 생성
                    , lastPosition.y
                    , lastPosition.z
                    ),
                    new Vector3(deltaX, 1, stackBounds.y)       //조각의 크기: 잘려나간 만큼
                    );

                comboCount = 0;
            }
            else
            {
                ComboCheck();
                //거의 정렬되어 있으면 위에 그대로 쌓기
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }

        }
        else // z축 방향 처리
        {
            float deltaZ = prevBlockPosition.z - lastBlock.localPosition.z;
            bool isNegativeNum = (deltaZ < 0) ? true : false; // 압뒤 체크


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
        //현재 블럭 복제(잔해로 쓸 오브젝트)
        GameObject go = Instantiate(lastBlock.gameObject);
        go.transform.parent = this.transform;

        //위치 크기 회전 설정
        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.transform.localRotation = Quaternion.identity;

        //중력 적용
        go.AddComponent<Rigidbody>();
        go.name = "Rubble";         // 이름 설정
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
            Debug.Log("최고 점수 갱신");
            bestScore = stackCount;
            bestCombo = maxCombo;

            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            PlayerPrefs.SetInt(BestComboKey, bestCombo);
        }
    }
    void GameOverEffect()
    {
        int childCount = this.transform.childCount;       //아래의 하위 객체의 개수를 구한다.

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

