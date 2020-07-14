using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Контроль игрового процесса
/// </summary>
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static event Action OnCubeSpawned = delegate{};

    private CubeSpawner[] spawners; //место рождения кубов/платформ
    private int spawnerIndex;       //номер спауна кубов
    private CubeSpawner currentSpawner; //текущий спаунер

    [SerializeField]
    public MovingCamera camera; //камера следящая за сценой 

    static public bool GameStatus;     //можно играть 


   
    /// <summary>
    /// Найти все спаунеры кубов/платформ
    /// </summary>
    private void Awake()
    {
        GameStatus = true;
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    // Update is called once per frame

    /// <summary>
    /// Старт игры/запуск
    /// </summary>
    private void Start()
    {
        StartGame();


    }

    /// <summary>
    /// Отслеживания нажатий
    /// </summary>
    void Update()
    {
        if (GameStatus == true)
        {
            if (Input.touchCount > 0)   //нажатие по экрану
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    // что-то делать
                    StartGame();
                }
            }
            else
                if (Input.GetMouseButtonDown(0))//клик мыши
            {
               // GameStatus = false;
                Debug.Log("mause");
               StartGame();
            }
        }
        else {
            camera.ShowTower();
        }
    }

    /// <summary>
    /// Старт игры / рождение платформы
    /// </summary>
    private void StartGame()
    {
      
            Debug.Log(GameStatus.ToString());
            if (MovingCube.CurrentCube != null)
            {
                MovingCube.CurrentCube.Stop();
            }
            currentSpawner = spawners[UnityEngine.Random.Range(0, spawners.Length)];

            currentSpawner.SpawnCube();
            OnCubeSpawned();
            camera.Next_Position(currentSpawner.transform.localScale.y);
    }

    static public void EndGame()
    {
        GameStatus = false;
        
    }


}
