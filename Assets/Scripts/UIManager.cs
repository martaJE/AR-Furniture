using UnityEngine;
using DG.Tweening;
using System;
/// <summary>
/// Clase UIManager se envarga de hacer las transiciones entres las vistas de la aplicacion.
/// Ademas de que utiliza una pequeña animacion con la libreria DOTween
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Variables privadas que hacen referencias a las vistas
    /// </summary>
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject itemsMenuCanvas;
    [SerializeField] private GameObject ARPositionCanvas;

    /// <summary>
    /// Método principal que porporciona funcionalidad a los eventos del GameManager
    /// </summary>
    void Start()
    {
        GameManager.instance.OnMainMenu += ActivateMainMenu;
        GameManager.instance.OnItemsMenu += ActivateItemMenu;
        GameManager.instance.OnARPosition += ActivateARPosition;
    }
    /// <summary>
    /// Metodo que muestra la pantalla de ARPositionCanvas y oculta las demas
    /// </summary>
    private void ActivateARPosition()
    {
        //Oculta los elementos de MainMenuCanvas al decir que la escala es 0, ademas de una animacion minima en la transicion
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        //Oculta los elementos de ItemMenuCanva al decir que la escala es 0, ademas de una animacion minima en la transicion
        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, 0.3f);
        //Muestra los elementos de ARPositioncanva
        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), 0.3f);

    }
    /// <summary>
    /// Metodo que muestra la pantalla de ItemMenuCanva y oculta las demas
    /// </summary>
    private void ActivateItemMenu()
    {
        //Oculata los elementos del MainMenuCanva 
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);
        //Muestra los elementos de ItemMenuCanva y el 3 Child va a hacer una pequeña animacion de entrada
        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), 0.3f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(300, 0.3f);
        //Oculta los elementos de ARPositionCanva
        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
    }
    private void ActivateMainMenu()
    {
        //Muestra los elementos de MainMenucanvas
        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        //Oculta los elementos de ItemMenuCanva 
        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, 0.3f);
        //Oculta los elementos de ARPositionCanva
        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
    }
}
