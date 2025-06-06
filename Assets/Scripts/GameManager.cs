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
    /// Este método asegura de que solo haya una instancia del GameManager
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
    /// Método principal de la aplicacion, nada mas iniciar la aplicacion aplica el método MainMenu()
    /// </summary>
    void Start()
    {
        MainMenu();
    }
    /// <summary>
    /// Método que invoca el Ménu Prinicpal de la aplicación y muestra por consola un mensaje
    /// </summary>
    public void MainMenu()
    {
        OnMainMenu?.Invoke();
        Debug.Log("Main menu activado");
    }
    /// <summary>
    /// Método que invoca el Ménu de Items de la aplicación y muestra por consola un mensaje
    /// </summary>
    public void ItemsMenu()
    {
        OnItemsMenu?.Invoke();
        Debug.Log("Items menu activado");
    }
    /// <summary>
    /// Método que invoca el Ménu de AR Position de la aplicación y muestra por consola un mensaje
    /// </summary>
    public void ARPosition()
    {
        OnARPosition?.Invoke();
        Debug.Log("AR Position activado");
    }
    /// <summary>
    /// Método que cierra la aplicacion y muestra un mensaje por la consola
    /// </summary>
    public void CloseApp()
    {
        Application.Quit();
        Debug.Log("Aplicacion cerrada");

    }
}
