using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace felipsteles
{
    public class ThirdPersonCamera : MonoBehaviour
{
    //Declaracao de variaveis
    public Transform target;
    public Transform child;
    public Camera cameraComponent;

    public float horizontalRotationValue;
    public float verticalRotationValue;
    public float distanceOfTarget;
    public float heightOfCamera;

    public LayerMask cameraColisionDetection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalRotationValue += Input.GetAxis("Mouse X");
        verticalRotationValue += Input.GetAxis("Mouse Y");

        transform.position = target.transform.position;
        transform.rotation = Quaternion.Euler(-verticalRotationValue,horizontalRotationValue,0);

        //Lembrete: Criar um objeto Camera dentro do camera colocar o Child e dentro do Child a Main Camera
        child.transform.localPosition = new Vector3(0,heightOfCamera,-distanceOfTarget);

        RaycastHit hit;
        verticalRotationValue = Mathf.Clamp(verticalRotationValue,-40,50); //limitar rotação

        if(Physics.Linecast(transform.position, child.transform.position, out hit, cameraColisionDetection)){
            Debug.DrawLine(transform.position, child.transform.position, Color.red);
            cameraComponent.transform.position = hit.point;

        }else{
            Debug.DrawLine(transform.position, child.transform.position, Color.green);
            cameraComponent.transform.localPosition = new Vector3(0,0,0);
        }

    }
}
}
