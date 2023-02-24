using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkript : MonoBehaviour
{
    public Vector3 Speed = new Vector3(0, 0, 1);
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("Ok");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = transform.localPosition + Speed * Time.deltaTime;
    }
}
