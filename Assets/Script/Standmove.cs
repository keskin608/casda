using UnityEngine;

public class SmoothObjectMover : MonoBehaviour
{
    public float moveAmount = 5f; // Hareket miktar�
    public float moveSpeed = 2f; // Hareket h�z�

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }

        if (isMoving)
        {
            // Hareketi ger�ekle�tir
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Hedefe ula�t���nda hareketi durdur
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    void MoveRight()
    {
        if (!isMoving)
        {
            targetPosition += new Vector3(moveAmount, 0, 0);
            isMoving = true;
        }
    }

    void MoveLeft()
    {
        if (!isMoving)
        {
            targetPosition += new Vector3(-moveAmount, 0, 0);
            isMoving = true;
        }
    }
}
