using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Работа с камерой
/// </summary>
public class MovingCamera : MonoBehaviour
{
    //позиция для смещения камеры
    public Vector3 EndPosition;

    private bool ShowAllTower;

    public  DeleteCube firstCube;


    /// <summary>
    ///смена позиции камеры при тапе 
    /// </summary>
    /// <param name="new_y">параметр Y смещения камеры</param>

    private void Start()
    {
        EndPosition = this.transform.position;
        ShowAllTower = false; 
    }
    public void Next_Position(float new_y)      //смена позиции для камеры при тапе
    {
        EndPosition += new Vector3(0f, new_y, 0f);

    }

    private void Update()
    {
        if (ShowAllTower == false)
        {
            transform.position = Vector3.Lerp(transform.position, EndPosition, 0.05f);
        }
        else
        {
            if (firstCube.returnVisible() == false)
            {
                transform.position += new Vector3(2.2f, 0.5f, -2.2f) * Time.deltaTime;
            }
        }
    }


    public void ShowTower()
    {
        ShowAllTower = true;
    }


}
