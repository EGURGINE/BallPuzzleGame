using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BtnManagers : MonoBehaviour
{
    [SerializeField]
    private Button shakeBtn;

    private void Start()
    {
        shakeBtn.onClick.AddListener(() =>
        {
            foreach (var item in BallSpawner.Instance.allBalls)
            {
                item.ShakeBall();
            } 
        });
    }
}
