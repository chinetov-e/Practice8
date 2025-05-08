using UnityEngine;
using UnityEngine.UI;

public class RelayRace : MonoBehaviour
{
    public Transform[] runners;
    public float speed;
    private float passDistance = 1f;
    private int currentRunnerIndex = 0;
    private int nextRunnerIndex = 1;
    private GameObject baton;

    void Start()
    {
        baton = runners[currentRunnerIndex].Find("Baton").gameObject;
        AttachBaton(runners[currentRunnerIndex]);
    }

    void Update()
    {
        if(runners.Length < 2)
            return;

        Transform currentRunner = runners[currentRunnerIndex];
        Transform nextRunner = runners[nextRunnerIndex];

        currentRunner.position = Vector3.MoveTowards(currentRunner.position, nextRunner.position, speed * Time.deltaTime);
        
        Vector3 direction = (nextRunner.position - currentRunner.position).normalized;
        if(direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            currentRunner.rotation = Quaternion.Slerp(currentRunner.rotation, toRotation, Time.deltaTime * 10f);
        }

        float distance = Vector3.Distance(currentRunner.position, nextRunner.position);
        if(distance <= passDistance)
        {
            currentRunnerIndex = nextRunnerIndex;
            nextRunnerIndex = (nextRunnerIndex + 1) % runners.Length;

            AttachBaton(runners[currentRunnerIndex]);
        }
    }

    void AttachBaton(Transform runner)
    {
        if(baton == null) return;

        baton.transform.SetParent(runner);
        baton.transform.localPosition = new Vector3(0.5f, 0, 0.2f);
        baton.transform.localRotation = Quaternion.identity;
    }
}
