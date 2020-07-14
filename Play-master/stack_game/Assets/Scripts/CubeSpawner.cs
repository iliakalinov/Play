using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// работа с создание кубов/платформ
/// </summary>
public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCube cubePrefab;  //префаб платформы

    [SerializeField]
    private MoveDirection moveDirection;    //направление смещения

   
    /// <summary>
    /// Создать объект префаба в нужном месте
    /// </summary>
    public void SpawnCube()
    {
        if (GameManager.GameStatus == true)
        {
            var cube = Instantiate(cubePrefab);


            if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("Start"))//Направление движения

            {
                float x = moveDirection == MoveDirection.Left ? transform.position.x : MovingCube.LastCube.transform.position.x;
                float z = moveDirection == MoveDirection.Right ? transform.position.z : MovingCube.LastCube.transform.position.z;
                if (moveDirection == MoveDirection.Up)
                {
                    x = -transform.position.x;
                }
                if (moveDirection == MoveDirection.Down)
                {
                    z = +transform.position.z;
                }


                cube.transform.position = new Vector3(x,
                    MovingCube.LastCube.transform.position.y + cubePrefab.transform.localScale.y,
                    z);

            }
            else
            {
                cube.transform.position = transform.position;
            }

            cube.MoveDirection = moveDirection;
        }

    }

    /// <summary>
    /// отобразить место создания кубов/платформ
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale) ;

    }
   


}
