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

    private readonly float timerCnt = 180;
    private float timerMin;
    private float timerSec;
    [SerializeField] private TextMeshProUGUI timeTxt;
    private float inGameTime;

    public bool isGameStart;
    private float InGameTime
    {
        get { return inGameTime; }
        set 
        { 
            inGameTime = value;
            TimeCalculation(inGameTime);
        }
    }
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
            StartSet();
        });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            plan.transform.DOLocalMoveY(-1, 0.5f).SetLoops(2, LoopType.Yoyo);
        }
        if(isGameStart) Timer();
    }
    
    public void StartSet()
    {
        timerMin = 0;
        timerSec = 0;
        inGameTime = 0;
        Score = 0;
    }
    public void ScoreCnt()
    {
        foreach (var item in selectBalls)
        {
            item.pc.Stop();
        }
        if (selectBalls.Count > 3)
        {
            Score += selectBalls.Count;

            for (int i = 0; i < selectBalls.Count; i++)
            {
                spawner.Push(selectBalls[i]);
                if(i == selectBalls.Count - 1)
                {
                    StartCoroutine(spawner.Pop());
                }
            }
        }
    }

    private void Timer()
    {
        InGameTime += Time.deltaTime;
        if(inGameTime >= timerCnt)
        {
            GameOver();
        }
    }
    
    private void TimeCalculation(float _time) 
    {
        timerSec = (int)_time;

        if (timerSec - (60 * timerMin) > 60)
        {
            timerMin++;
            timerSec = _time - (60 * timerMin);
        }


        if (timerSec > 9) timeTxt.text = $"{timerMin} : {timerSec}";
        else timeTxt.text = $"{timerMin} : 0{timerSec}";
    }

    private void GameOver()
    {
        isGameStart = false;
    }
}
