using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EBallType
{
    red,
    green,
    blue,
}
public class BallSpawner : MonoBehaviour
{
    const int MAX_BALL = 180;
    [SerializeField] Ball ball;

    Stack<Ball> balls = new Stack<Ball>();

    private void Start()
    {
        Time.timeScale = 100;
        for (int i = 0; i < MAX_BALL; i++)
        {
            Ball theBall = Instantiate(ball);
            balls.Push(theBall);
            theBall.transform.position = transform.position;
            theBall.transform.parent = transform;
            theBall.gameObject.SetActive(false);
        }
    }
    public IEnumerator Spawn()
    {
        for (int i = 0; i < MAX_BALL; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Ball theBall = balls.Pop();
            theBall.gameObject.SetActive(true);
            theBall.transform.parent = null;
            theBall.GetComponent<Rigidbody>().AddForce(new Vector2(-100, 100));
            theBall.ThisType((EBallType)Random.Range(0, 3));

            if(i == MAX_BALL -1) Time.timeScale = 1;
        }
    }
    public IEnumerator Pop()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Ball theBall = balls.Pop();
            theBall.gameObject.SetActive(true);
            theBall.transform.parent = null;
            theBall.GetComponent<Rigidbody>().AddForce(new Vector2(-500, 100));
            theBall.ThisType((EBallType)Random.Range(0, 3));
        }
    }

    public void Push(Ball ball)
    {
        Ball theBall = ball;
        balls.Push(theBall);
        theBall.transform.parent = transform;
        theBall.transform.position = transform.position;
        theBall.gameObject.SetActive(false);
    }
}
