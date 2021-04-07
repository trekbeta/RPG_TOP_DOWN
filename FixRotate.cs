using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotate : MonoBehaviour {

    Quaternion rotacao;



    // Start is called before the first frame update
    void Aweke()
    {
        rotacao = transform.rotation;    
    }

    private void LateUpdate()
    {
        transform.rotation = rotacao;
    }

   
}
