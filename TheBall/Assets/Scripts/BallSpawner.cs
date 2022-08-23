using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EBallType
{
    red,
    green,
    blue,
    puple,
    orange
}
public class BallSpawner : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static BallSpawner instance;

    public static BallSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BallSpawner>();
            }
            return instance;
        }
    }
    #endregion
    const int MAX_BALL = 50;
    [SerializeField] Ball ball;
    public List<Ball> allBalls = new List<Ball>();
    Stack<Ball> balls = new Stack<Ball>();

    private void Start()
    {
        instance = this;
        Time.timeScale = 100;
        for (int i = 0; i < MAX_BALL; i++)
        {
            Ball theBall = Instantiate(ball);
            allBalls.Add(theBall);
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
            theBall.GetComponent<Rigidbody>().AddForce(new Vector2(Random.Range(-200, -50), 100));
            theBall.ThisType((EBallType)Random.Range(0, 5));

            if (i == MAX_BALL - 1)
            {
                Time.timeScale = 1;
                GameManager.Instance.isGameStart = true;
            }
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
            theBall.GetComponent<Rigidbody>().AddForce(new Vector2(Random.Range(-200,-50), 100));
            theBall.ThisType((EBallType)Random.Range(0, 5));
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
