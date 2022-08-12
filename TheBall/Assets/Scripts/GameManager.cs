using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
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
    [SerializeField] GameObject titleSet;
    [SerializeField] GameObject ingameSet;
    [SerializeField] private Button startBtn;
    [SerializeField] BallSpawner spawner;
    [SerializeField] private int score;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] GameObject plan;

    public int Score
    {
        get { return score; }
        set 
        {
            score = value;
            scoreTxt.text = $"SCORE {score}";
        }
    }
    private void Start()
    {
        startBtn.onClick.AddListener(() =>
        {
            titleSet.SetActive(false);
            ingameSet.SetActive(true);
            StartCoroutine(spawner.Spawn());
        });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            plan.transform.DOLocalMoveY(-1, 0.5f).SetLoops(2, LoopType.Yoyo);
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
            StartCoroutine(spawner.Pop());

        }
    }
}
