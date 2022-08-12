using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    [SerializeField] Material[] materials;
    EBallType type;
    [SerializeField] private List<Ball> aroundBall = new List<Ball>();
    private bool isTrue;
    Rigidbody rb => GetComponent<Rigidbody>();
    private BallSpawner ballSpawner => GameObject.Find("BallSpawner").GetComponent<BallSpawner>();
    public void ThisType(EBallType ballType)
    {
        type = ballType;

        switch (type)
        {
            case EBallType.red:
                gameObject.GetComponent<Renderer>().material = materials[0];
                break;
            case EBallType.green:
                gameObject.GetComponent<Renderer>().material = materials[1];
                break;
            case EBallType.blue:
                gameObject.GetComponent<Renderer>().material = materials[2];
                break;
            case EBallType.yellow:
                gameObject.GetComponent<Renderer>().material = materials[3];
                break;
            case EBallType.orange:
                gameObject.GetComponent<Renderer>().material = materials[4];
                break;
            default:
                print("Dd");
                break;
        }
    }
    private void Update()
    {
        if (transform.position.z > 0 || transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            print("D");
            rb.AddForce(new Vector2(Random.Range(-500,500), 500));
            //ThisType((EBallType)Random.Range(0,5));
        }
    }
    private void OnMouseDown()
    {
        GameManager.Instance.selectBalls.Add(this);
    }
    private void OnMouseEnter()
    {
        if (GameManager.Instance.selectBalls.Count == 0) return;

        foreach (var item in GameManager.Instance.selectBalls)
        {
            if (item == this)
            {
                return;
            }
        }
        foreach (var item in GameManager.Instance.selectBalls[GameManager.Instance.selectBalls.Count - 1].aroundBall)
        {
            if (item == this)
            {
                isTrue = true;
            }
        }

        if (GameManager.Instance.selectBalls[GameManager.Instance.selectBalls.Count - 1]. type == this.type && isTrue)
        {
            GameManager.Instance.selectBalls.Add(this);
        }
        else return;
    }
    private void OnMouseUp()
    {
        GameManager.Instance.ScoreCnt();
        GameManager.Instance.selectBalls.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            aroundBall.Add(collision.gameObject.GetComponent<Ball>());
        }

        if (collision.collider.CompareTag("CollectionBox"))
        {
            ballSpawner.Push(this.GetComponent<Ball>());
            StartCoroutine(ballSpawner.Pop());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            aroundBall.Remove(collision.gameObject.GetComponent<Ball>());
        }
    }
}
