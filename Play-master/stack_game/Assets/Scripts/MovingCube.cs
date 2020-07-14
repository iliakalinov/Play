using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


/// <summary>
/// Логика кубов
/// </summary>
public class MovingCube : MonoBehaviour
{

    public static MovingCube CurrentCube { get; private set; } //Текущий куб
    public static MovingCube LastCube { get; private set; } //следующий куб
    public MoveDirection MoveDirection { get;  set; } //Направление движения

    private ScoreText text;             //объект хранящий ресурсы максимального набранного значения 
    [SerializeField]
    private float moveSpeed = 1f;       //скорость кубов 

    private float stepcos;             //шаг cos для зацикленного движения

   // public GameObject Fire;             //система частиц
    /// <summary>
    /// Первичные настройки 
    /// </summary>
    private void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        }
        text= GameObject.Find("ScoreText").GetComponent<ScoreText>();

        GetComponent<Renderer>().material.color = GetRandemColor();
        CurrentCube = this;

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);

    }

    /// <summary>
    /// Для выбора рандомного цвета кубам/платформам
    /// </summary>
    /// <returns>новый цвет </returns>
    private Color GetRandemColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    /// <summary>
    /// Решение игра закончена или нет
    /// </summary>
    internal void Stop()
    {

        moveSpeed = 0;
        float hangover = GetHangover();

        float max = (MoveDirection == MoveDirection.Right|| MoveDirection == MoveDirection.Down) ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;

        //        Debug.Log(hangover);

        if (Mathf.Abs(hangover) >= max)
        {

            Save_score();
            LastCube = null;
            CurrentCube = null;
            //   SceneManager.LoadScene(0);
            //GameManager.GameStatus = false;
            GameManager.EndGame();
            Debug.Log("endGame");
            EndGame();
            this.gameObject.AddComponent<Rigidbody>();

            


        }
        else
        {
            
                float direction = hangover > 0 ? 1f : -1f;

                if (MoveDirection == MoveDirection.Right || MoveDirection == MoveDirection.Down)
                {
                    SplitCUbeOnZ(hangover, direction);
                }

                else
                {
                    SplitCUbeOnX(hangover, direction);

                }

                LastCube = this;
            }
        
        
    }

    /// <summary>
    /// сохранение данных игры при проигрыше
    /// </summary>
    public void Save_score()
    {
          int score = 0;

            if (PlayerPrefs.HasKey("Max_count"))
            {
                score = PlayerPrefs.GetInt("Max_count");//считываем мах прогресс

            }
            if (score < text.score)
            {                  // перезапись если больше

           // PlayerPrefs.DeleteKey("Max_count");    
            PlayerPrefs.SetInt("Max_count", text.score);


            }
       // PlayerPrefs.DeleteKey("LastGame");
        PlayerPrefs.SetInt("LastGame", text.score);



    }

    /// <summary>
    /// Выбор смещения/ость для платформы
    /// </summary>
    /// <returns></returns>
    private float GetHangover()
    {


        switch (MoveDirection)
        {
            case MoveDirection.Right:
                return transform.position.z - LastCube.transform.position.z;
                break;
            case MoveDirection.Up:
                return transform.position.x - LastCube.transform.position.x;
                break;
            case MoveDirection.Left:
                return transform.position.x - LastCube.transform.position.x;
                break;
            case MoveDirection.Down:
                return transform.position.z - LastCube.transform.position.z;
                break;
        }

        return ( 0f);
    }

    /// <summary>
    /// Смещение платформы по X оси
    /// </summary>
    /// <param name="hangover"> Ось смещения </param>
    /// <param name="direction"> направление</param>
    private void SplitCUbeOnX(float hangover, float direction)
    {

        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = LastCube.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize,transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition,transform.position.y, transform.position.z );

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPositino = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockXPositino, fallingBlockSize);
    }
    /// <summary>
    /// Смещение платформы по Z оси
    /// </summary>
    /// <param name="hangover"> Ось смещения </param>
    /// <param name="direction"> направление</param>
    private void SplitCUbeOnZ(float hangover,float direction)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;
            
        float newZPosition = LastCube.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f* direction);
        float fallingBlockZPositino = cubeEdge + fallingBlockSize / 2f * direction  ;

        SpawnDropCube(fallingBlockZPositino, fallingBlockSize);
    }


    /// <summary>
    /// Создание 2-го куба(который упадет)
    /// </summary>
    /// <param name="fallingBlockZPosition"> позиция Z для падения       </param>
    /// <param name="fallingBlockSize">   размер блока  </param>
    public void SpawnDropCube(float fallingBlockZPosition,float fallingBlockSize)
    {
    
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (MoveDirection == MoveDirection.Right || MoveDirection == MoveDirection.Down)

        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);
        }
       
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize,transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockZPosition,transform.position.y, transform.position.z);

        }
   
        cube.AddComponent<Rigidbody>();
        cube.AddComponent<BoxCollider>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f);

    }



    /// <summary>
    ///Обновление позиции блока
    /// </summary>
    void Update()
    {

        
            stepcos += Time.deltaTime;


            switch (MoveDirection)
            {
                case MoveDirection.Right:
                    transform.position += transform.forward * Time.deltaTime * moveSpeed * Mathf.Cos(stepcos);
                    break;
                case MoveDirection.Up:
                    transform.position -= transform.right * Time.deltaTime * moveSpeed * Mathf.Cos(stepcos);
                    break;
                case MoveDirection.Left:
                    transform.position += transform.right * Time.deltaTime * moveSpeed * Mathf.Cos(stepcos);
                    break;
                case MoveDirection.Down:
                    transform.position -= transform.forward * Time.deltaTime * moveSpeed * Mathf.Cos(stepcos);
                    break;
            }
        

    }

    private void EndGame()
    {

    }
}
