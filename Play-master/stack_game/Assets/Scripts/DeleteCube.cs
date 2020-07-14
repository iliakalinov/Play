using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// для удаление мусора который падает
/// </summary>
public class DeleteCube : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Visible = true;

    /// <summary>
    /// удаляет объект который упал слишком далеко
    /// </summary>
    /// <param name="collision">объект который попал в область для удаления </param>
    private void OnCollisionEnter(Collision collision)  //удаление лишнего при падении
    {
        Destroy(collision.gameObject);

    }
    void OnBecameVisible()
    {
        Visible = true;
    }
    void OnBecameInvisible()
    {
        Visible = false;

    }
    public bool returnVisible() { return Visible; }
}
