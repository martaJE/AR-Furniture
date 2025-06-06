using UnityEngine;

/// <summary>
/// Esta clase va a representa un objeto, con datos.
/// Va a contener un nombre, una imagen, una descripcion y un modelo 3D
/// </summary>
[CreateAssetMenu] //Permite crear un ScriptableObject en el menu de Unity
public class Item : ScriptableObject
{
    /// <summary>
    /// Variables que va a contener el item
    /// </summary>
    public string ItemName;
    public Sprite ItemImage;
    public string ItemDescription;
    public GameObject Item3DModel;
}
