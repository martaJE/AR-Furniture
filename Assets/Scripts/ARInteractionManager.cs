
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

/// <summary>
/// ARInteractionManager se encarga con la interaccion con el usuario al tocar la pantalla en ARPositionCanva, 
/// es decir al mover, rotar y seleccionar el modelo 3D
/// </summary>
public class ARInteractionManager : MonoBehaviour
{
    /// <summary>
    /// Variables
    /// </summary>
    [SerializeField] private Camera arCamera;
    [SerializeField] private float rotationSensitivity = 1.0f;
    private ARRaycastManager aRRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit> ();

    private GameObject aRPointer;
    private GameObject item3DModel;
    private GameObject itemSelect;

    private bool isInitialPosition;
    private bool isOverUI;
    private bool isOver3DModel;

    private Vector2 initialTouchPos; 
    /// <summary>
    /// Asigna un modelo 3D para colocarlo en la escena
    /// Y lo posiciona sobre un ARPointer
    /// </summary>
    public GameObject Item3DModel
    {
        set
        {
            item3DModel = value;
            item3DModel.transform.position = aRPointer.transform.position;
            item3DModel.transform.parent = aRPointer.transform;
            isInitialPosition = true;
        }
    }
    /// <summary>
    /// Inicializa y realiza un evento SetItemPosition
    /// </summary>
    void Start()
    {
        aRPointer = transform.GetChild(0).gameObject;
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        GameManager.instance.OnMainMenu += SetItemPosition;
    }
    /// <summary>
    /// Se vuelve a restablecer el ARPointer y vuelve null el modelo 3D al volver al menu prinical 
    /// </summary>
    private void SetItemPosition()
    {
       if (item3DModel != null)
        {
            item3DModel.transform.parent = null;
            aRPointer.SetActive(false);
            item3DModel = null;
        }
    }
    /// <summary>
    /// Eimina el modelo 3D actual y oculta el puntero
    /// </summary>
    public void DeleteItem3DModel()
    {
        Destroy(item3DModel); 
        aRPointer.SetActive (false);
        GameManager.instance.MainMenu();
    }
    /// <summary>
    /// En esta parte se va actualizar por frame, se encarga de manipular el modelo con toques en la pantalla.
    /// Va a poder posicionarse, mover y rotar
    /// </summary>
    void Update()
    {
        if (isInitialPosition)
        {
            //Posicionar el modelo en el mundo real 
            Vector2 middelPointScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            aRRaycastManager.Raycast(middelPointScreen, hits, TrackableType.Planes);
            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
                aRPointer.SetActive(true);
                isInitialPosition = false;
            }

        }

        //Si hay interaccion en la pantalla
        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
        {
            var touchOne = Touchscreen.current.touches[0];
            if (touchOne.press.wasPressedThisFrame)
            {
                var touchPosition = touchOne.position.ReadValue();
                isOverUI = isTapOverUI(touchPosition);
                isOver3DModel = isTapOver3DModel(touchPosition);
                //Comprueba si es un modelo o un elemento de la interfaz
                if (isOver3DModel && !isOverUI)
                {
                    if (item3DModel != null)
                    {
                        item3DModel.transform.parent = null;
                    }
                    GameManager.instance.ARPosition();
                    item3DModel = itemSelect;
                    itemSelect = null;
                    aRPointer.SetActive(true);
                    transform.position = item3DModel.transform.position;
                    item3DModel.transform.parent = aRPointer.transform;
                }
            }
            //Movimiento del dedo si se arrastra
            if (touchOne.delta.ReadValue() != Vector2.zero)
            {
                
                if (aRRaycastManager.Raycast(touchOne.position.ReadValue(), hits, TrackableType.Planes))
                {
                    Pose hitPose = hits[0].pose;
                    if (!isOverUI && isOver3DModel)
                    {
                        transform.position = hitPose.position;
                    }
                }
            }
            //Rotacion del modelo al presionar con dos dedos
            if (Touchscreen.current.touches.Count >= 2)
            {
                var  touchTwo = Touchscreen.current.touches[1];
                //Direccion inicial
                if (touchOne.press.wasPressedThisFrame || touchTwo.press.wasPressedThisFrame)
                {
                    initialTouchPos = touchTwo.position.ReadValue() - touchOne.position.ReadValue();
                }
                //Si hay movimiento, calcula la nueva direccion y lo aplica
                if (touchOne.delta.ReadValue() != Vector2.zero || touchTwo.delta.ReadValue() != Vector2.zero)
                {
                    Vector2 currentTouch = touchTwo.position.ReadValue() - touchOne.position.ReadValue();
                    float angle = Vector2.SignedAngle(initialTouchPos, currentTouch)*rotationSensitivity;
                    Quaternion rotationDelta = Quaternion.Euler(0, -angle, 0);
                    item3DModel.transform.rotation =item3DModel.transform.rotation * rotationDelta;
                    initialTouchPos = currentTouch;
                }
            }
        }
    }
    /// <summary>
    /// Comprueba si al tocar fue sobre un elemento con una etiqueta Item
    /// </summary>
    /// <param name="touchPosition"></param>
    /// <returns>True si se ha seleccionado un modelo correcto</returns>
    private bool isTapOver3DModel(Vector2 touchPosition)
    {
        //Crea un "rayo" desde la camara en la posicion del toque
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        //Comprueba si ha tocado un collider de un modelo 3D
        if (Physics.Raycast(ray, out RaycastHit hit3DModel))
        {
            //Comprueba si tiene la etiqueta Item
            if (hit3DModel.collider.CompareTag("Item"))
            {
                itemSelect = hit3DModel.transform.gameObject;
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// En este metodo se esta comprobando si es sobre un elemento de la interfaz
    /// </summary>
    /// <param name="touchPosition"></param>
    /// <returns>True si se ha tocado un elemento grafico</returns>
    private bool isTapOverUI(Vector2 touchPosition)
    {
        //Almacena los datos del toque
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position =  new Vector2(touchPosition.x, touchPosition.y);
        //Almacena los resultadors de raycast
        List<RaycastResult> result = new List<RaycastResult>();
        //Ejecuta todos los raycast contra todos los elemntos de ese toque
        EventSystem.current.RaycastAll(eventData, result);
        //Si hay algun resultado significa que toco un elemnto grafico
        return result.Count > 0;
    }
}
