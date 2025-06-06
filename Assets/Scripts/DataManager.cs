
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DataManager va a manejar la creacion de una lista de items 
/// </summary>
public class DataManager : MonoBehaviour
{
    /// <summary>
    /// Variables
    /// </summary>
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private GameObject buttonContainer; //Content (ScrollView)
    [SerializeField] private ItemButtonManager itemButtonManager; //Plantilla buttonItemPrefab

    /// <summary>
    /// Se realiza un evento para la creacion de los botones al iniciar ItemButtonCanva
    /// </summary>
    void Start()
    {
        GameManager.instance.OnItemsMenu += CreateButtons; 
    }
    /// <summary>
    /// Metodo que crea los botones y asigna los datos a las variables
    /// </summary>
    private void CreateButtons()
    {
        foreach (var item in items)
        {
            ItemButtonManager itemButton;
            //Se instancia como hijo del contendor
            itemButton = Instantiate(itemButtonManager, buttonContainer.transform);
            //Se asigna los datos a las variables 
            itemButton.ItemName = item.ItemName;
            itemButton.ItemDescription = item.ItemDescription;
            itemButton.ItemImage = item.ItemImage;
            itemButton.Item3DModel = item.Item3DModel;
            itemButton.name = item.ItemName;
        }
        //Se quita el evento para que no se repitan los botones si se vuelve abrir este menu
        GameManager.instance.OnItemsMenu -= CreateButtons;
    }
}
