using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    EBallType type;
    [SerializeField] Material[] materials;
    [SerializeField] private List<Ball> aroundBall = new List<Ball>();
    private bool isTrue;
    Rigidbody rb => GetComponent<Rigidbody>();
    private BallSpawner ballSpawner => GameObject.Find("BallSpawner").GetComponent<BallSpawner>();

    public Light lightObj;

    private void Start()
    {
        lightObj.gameObject.SetActive(false);
    }
    public void ThisType(EBallType ballType)
    {
        lightObj.gameObject.SetActive(false);
        type = ballType;
        isTrue = false;
        gameObject.GetComponent<Renderer>().material = materials[(int)type];
    }
    private void FixedUpdate()
    {
        if (transform.position.z > 0 || transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
    public void ShakeBall()
    {
        rb.AddForce(new Vector2(Random.Range(-500, 500), 500));
    }
    private void OnMouseDown()
    {
        GameManager.Instance.selectBalls.Add(this);
        lightObj.gameObject.SetActive(true);

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
            if (item == this && GameManager.Instance.selectBalls[
                GameManager.Instance.selectBalls.Count - 1].type == this.type)
            {
                isTrue = true;
            }
        }

        if (isTrue)
        {
            GameManager.Instance.selectBalls.Add(this);
            lightObj.gameObject.SetActive(true);
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
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            aroundBall.Remove(collision.gameObject.GetComponent<Ball>());
            print("dma");
        }
    }
}
