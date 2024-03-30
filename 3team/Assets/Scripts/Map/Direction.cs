using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Direction : MonoBehaviour
{

    private List<Vector2Int> _path;

    private Vector2 _previousDirection;

    // 회전 속도 조절 매개변수
    public float rotationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDirection(List<Vector2Int> path)
    {
        _path = path;
        StartCoroutine("DecisionDirection");
    }

    IEnumerator DecisionDirection()
    {
        while(true)
        {
            if (_path.Count > 1)
            {
                Vector2 currentDirection = _path[1] - _path[0];
                // 이전 방향과 현재 방향이 다른 경우 회전합니다.
                if (currentDirection != _previousDirection)
                {
                    Debug.Log("회전");
                    // 새 방향을 기준으로 게임 오브젝트를 회전합니다.
                    Quaternion targetRotation = Quaternion.LookRotation(new Vector3(currentDirection.x, 0, currentDirection.y));
                    // 회전을 부드럽게 처리하기 위해 Slerp를 사용합니다.
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    // 이전 방향을 현재 방향으로 업데이트합니다.
                    _previousDirection = currentDirection;
                }
            }
            yield return new WaitForSeconds(1.5f);
        }
        
    }
}
