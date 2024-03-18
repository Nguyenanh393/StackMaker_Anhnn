using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Direct direct;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float speed;
    [SerializeField] private GameObject playerBrickPrefab;
    [SerializeField] private GameObject animTranform;
    
    RaycastHit hit;
    private bool isMoving;
    private static bool canMove = true; 
    private static bool isFinish;
    
    public static bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }
    
    public static bool IsFinish
    {
        get => isFinish;
        set => isFinish = value;
    }

    private Stack<PlayerBrick> playerBricks = new Stack<PlayerBrick>();

    private void Awake()
    {
        PlayerPrefs.SetInt("Level", 1);
    }

    private void Start()
    {
        GameManager.Instance.OnEvent += OnEvent;
    }

    void Update()
    {
        GetInput();
        Move(targetPosition, speed);
        CheckStop();
    }

    void OnInit()
    {
        transform.position = new Vector3(0, 0f, 0);
        speed = 10f;
        isMoving = false;
        canMove = true;
        isFinish = false;
        targetPosition = transform.position;
    }
    
    private void OnEvent(EventID eventID)
    {
        switch (eventID)
        {
            case EventID.OnNextLevel:
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                break;
            case EventID.OnCompleteLevel:
                ResetLevel();
                break;
            case EventID.OnRestartLevel:
                ResetLevel();
                break;
        }
    }
    
    private enum Direct 
    {
        Forward,
        Backward,
        Left,
        Right,
        None
     }

    private void GetInput()
    {
        if (isMoving)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        { 
            startPosition = Input.mousePosition;
        }
    
        if (Input.GetMouseButtonUp(0))
        {
            endPosition = Input.mousePosition;
            direction = endPosition - startPosition;
            if (direction.magnitude < 10f)
            {
                return;
            }
            direct = GetDirection();
            targetPosition = GetTargetPosition();
            isMoving = true;
        }
        
    }

    private void Move(Vector3 targetPosition, float speed)
    {
        if (!canMove)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    private Vector3 GetTargetPosition()
    {
        Vector3 moveStep = GetDirectVector();
        Vector3 targetVector = transform.position;
        
        // Debug.DrawRay(targetVector, Vector3.down * 5f, Color.red, 1f);
        
        while (Physics.Raycast(targetVector, Vector3.down, out hit, 5f, layerMask))
        {
            targetVector += moveStep;
            //Debug.Log(hit.collider.name);
        }
        return targetVector - moveStep;
    }
    private Vector3 GetDirectVector()
    {
        switch (direct)
        {
            case Direct.Forward:
                direction = Vector3.forward;
                break;
            case Direct.Backward:
                direction = Vector3.back;
                break;
            case Direct.Left:
                direction = Vector3.left;
                break;
            case Direct.Right:
                direction = Vector3.right;
                break;
            case Direct.None:
                direction = Vector3.zero;
                break;
        }
        return direction;
    }
    
    private Direct GetDirection()
    {
        float angle = Vector3.Angle(Vector3.up, direction);
        
        if (angle < 45f)
        {
            return Direct.Forward;
        }
        else if (angle > 135f)
        {
            return Direct.Backward;
        }
        else if (direction.x > 0)
        {
            return Direct.Right;
        }
        else if (direction.x < 0)
        {
            return Direct.Left;
        }
        else
        {
            return Direct.None;
        }
    }

    public void AddBrick()
    {
        canMove = true;
        animTranform.transform.position += Vector3.up * Constance.BRICK_HEIGHT;
        GameObject playerBrick = Instantiate(playerBrickPrefab, transform);
        playerBrick.transform.position += Vector3.up * Constance.BRICK_HEIGHT;
        playerBrick.transform.SetParent(animTranform.transform);
        
        playerBricks.Push(playerBrick.GetComponent<PlayerBrick>());
        Debug.Log(playerBricks.Count);
    }

    public void RemoveBrick()
    {
        if (playerBricks.Count == 0)
        {
            if (!isFinish)
            {
                canMove = false;
            }
            else
            {
                return;
            }
        }
        animTranform.transform.position -= Vector3.up * Constance.BRICK_HEIGHT;
        GameObject playerBrick = playerBricks.Pop().GameObject();
        Destroy(playerBrick);
        Debug.Log(playerBricks.Count);
    }

    public void ClearBrick()
    {
        while (playerBricks.Count > 0)
        {
            RemoveBrick();
        }
    }

    private void CheckStop()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }
    
    private void ResetLevel()
    {
        OnInit();
        ClearBrick();
    }
}
