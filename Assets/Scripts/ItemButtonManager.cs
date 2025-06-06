using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ItemButtonManager se va a encargar de la funcinalidad del item
/// Va a mostrar la informacion del boton y generar un modelo 3D al pulsar el boton
/// </summary>
public class ItemButtonManager : MonoBehaviour
{
    /// <summary>
    /// Variables
    /// </summary>
    private string itemName;
    private string itemDescription;
    private Sprite itemImage;
    private GameObject item3DModel;
    /// <summary>
    /// Instancia de ArInteractionManager
    /// </summary>
    private ARInteractionManager interactionManager;
    /// <summary>
    /// Properties para establecer los datos en cada variable
    /// </summary>
    public string ItemName 
    { 
        set
        {
            itemName = value;
        }
    }
    public string ItemDescription
    {
        set => itemDescription = value;
    }
    public Sprite ItemImage
    {
        set => itemImage = value;
    }
    public GameObject Item3DModel
    {
        set => item3DModel = value;
    }
    /// <summary>
    /// Inicializa el boton mostrando los datos del item y realiza un evento onClick
    /// </summary>
    void Start()
    {
        //Se define el nombre del item
        transform.GetChild(0).GetComponent<Text>().text = itemName;
        //Se asigna una Imagen 
        transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
        //Se establece una descripcion 
        transform.GetChild(2).GetComponent<Text>().text = itemDescription;
        var button = GetComponent<Button>();
        //Al pulsar se dirige a la vista de ARPositionCanva
        button.onClick.AddListener(GameManager.instance.ARPosition);
        //Al pulsar crea un modelo 3D
        button.onClick.AddListener(Create3DModel);
        //Se crea una referencia con ARInteractionManager
        interactionManager = FindObjectOfType<ARInteractionManager>();
    }
    /// <summary>
    /// Crea una instancia del modelo 3D del item y lo asigna al ARIteractionManager
    /// </summary>
    private void Create3DModel()
    {
        interactionManager.Item3DModel = Instantiate(item3DModel);
      
    }

}
