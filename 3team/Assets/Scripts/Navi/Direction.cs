using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Direction : MonoBehaviour
{

    private List<Vector2Int> _path;

    private Vector2 _previousDirection;

    // ȸ�� �ӵ� ���� �Ű�����
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
                // ���� ����� ���� ������ �ٸ� ��� ȸ���մϴ�.
                if (currentDirection != _previousDirection)
                {
                    Debug.Log("ȸ��");
                    // �� ������ �������� ���� ������Ʈ�� ȸ���մϴ�.
                    Quaternion targetRotation = Quaternion.LookRotation(new Vector3(currentDirection.x, 0, currentDirection.y));
                    // ȸ���� �ε巴�� ó���ϱ� ���� Slerp�� ����մϴ�.
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    // ���� ������ ���� �������� ������Ʈ�մϴ�.
                    _previousDirection = currentDirection;
                }
            }
            yield return new WaitForSeconds(1.5f);
        }
        
    }
}
