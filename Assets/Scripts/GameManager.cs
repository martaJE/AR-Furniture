using UnityEngine;
using System;

/// <summary>
/// GameManager se encarga de controlar los estados de las diferentes vistas
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Variables de tipo Evento que se activan segun la ventana
    /// </summary>
    public event Action OnMainMenu;
    public event Action OnItemsMenu;
    public event Action OnARPosition;
    /// <summary>
    /// Instancia global del GameManager
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Este m�todo asegura de que solo haya una instancia del GameManager
    /// </summary>
    /// <remarks>
    /// En el caso de que haya una esta se destruira
    /// </remarks>
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    /// <summary>
    /// M�todo principal de la aplicacion, nada mas iniciar la aplicacion aplica el m�todo MainMenu()
    /// </summary>
    void Start()
    {
        MainMenu();
    }
    /// <summary>
    /// M�todo que invoca el M�nu Prinicpal de la aplicaci�n y muestra por consola un mensaje
    /// </summary>
    public void MainMenu()
    {
        OnMainMenu?.Invoke();
        Debug.Log("Main menu activado");
    }
    /// <summary>
    /// M�todo que invoca el M�nu de Items de la aplicaci�n y muestra por consola un mensaje
    /// </summary>
    public void ItemsMenu()
    {
        OnItemsMenu?.Invoke();
        Debug.Log("Items menu activado");
    }
    /// <summary>
    /// M�todo que invoca el M�nu de AR Position de la aplicaci�n y muestra por consola un mensaje
    /// </summary>
    public void ARPosition()
    {
        OnARPosition?.Invoke();
        Debug.Log("AR Position activado");
    }
    /// <summary>
    /// M�todo que cierra la aplicacion y muestra un mensaje por la consola
    /// </summary>
    public void CloseApp()
    {
        Application.Quit();
        Debug.Log("Aplicacion cerrada");

    }
}
