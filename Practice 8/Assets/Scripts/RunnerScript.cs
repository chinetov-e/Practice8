using UnityEngine;

public class RunnerMovement : MonoBehaviour
{
    public Vector3[] targets;
    public float movementSpeed = 5f;
    private bool forward = true;
    private int currentIndex;
    void Start()
    {
        targets = new Vector3[] 
        {
            new Vector3(0, 1, 0),
            new Vector3(0, 1, 15),
            new Vector3(15, 1, 15),
            new Vector3(15, 1, 25),
            new Vector3(-15, 1, 25),
            new Vector3(-15, 1, 15),
        };     
    }

    void Update()
    {
        if(targets == null || targets.Length == 0)
            return;

        Vector3 direction = (targets[currentIndex] - transform.position).normalized;
        if(direction != Vector3.zero)
        {
            Quaternion RotationTo = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, RotationTo, Time.deltaTime * 15f);
        }

        transform.position = Vector3.MoveTowards(transform.position, targets[currentIndex], movementSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, targets[currentIndex]) < 0.01f)
        {
            if(forward)
            {
                currentIndex++;
                if(currentIndex >= targets.Length)
                {
                    currentIndex = targets.Length - 2;
                    forward = false;
                }
            }
            else
            {
                currentIndex--;
                if(currentIndex < 0)
                {
                    currentIndex = 1;
                    forward = true;
                }
            }
        }
    }
}
