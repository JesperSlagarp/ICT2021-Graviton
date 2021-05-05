using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class laserMount : MonoBehaviour
{

    [SerializeField]
    private float baseRotationSpeed;

    [SerializeField]
    private List<GameObject> lasers;

    private float rotationSpeed;
    private bool isShooting;
    
    
    private void Awake()
    {
        rotationSpeed = baseRotationSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActiveAndEnabled && isShooting)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed));
        }
        
    }

    

    private void OnEnable()
    {
        Debug.Log("Laser is active");
        isShooting = true;
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Debug.Log("Laser is inactive");
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
        }
    }
}
