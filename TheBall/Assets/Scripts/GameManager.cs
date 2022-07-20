using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    #endregion
    public List<Ball> selectBalls = new List<Ball>();

    [SerializeField] BallSpawner spawner;
    [SerializeField] private int score;
    [SerializeField] TextMeshProUGUI scoreTxt;
    public int Score
    {
        get { return score; }
        set 
        {
            score = value;
            scoreTxt.text = $"SCORE {score}";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(spawner.Spawn());
        }
    }

    public void ScoreCnt()
    {
        if (selectBalls.Count > 3)
        {
            Score += selectBalls.Count;
            foreach (var item in selectBalls)
            {
                spawner.Push(item);
            }
            spawner.StartCoroutine(spawner.Pop());

        }
    }
}
