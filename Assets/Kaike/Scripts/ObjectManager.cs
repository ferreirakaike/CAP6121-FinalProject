using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject cubeRed;
    public GameObject cubeGreen;
    public GameObject cubeBlue;
    
    public GameObject sphereRed;
    public GameObject sphereGreen;
    public GameObject sphereBlue;

    public GameObject cylinderRed;
    public GameObject cylinderGreen;
    public GameObject cylinderBlue;

    public Transform CameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(string command)
    {
        bool hasShape = false;
        bool hasColor = false;

        if (command.ToUpper().Contains("CUBE") || command.ToUpper().Contains("SPHERE") || command.ToUpper().Contains("CYLINDER"))
        {
            hasShape = true;
        }

        if (command.ToUpper().Contains("RED") || command.ToUpper().Contains("GREEN") || command.ToUpper().Contains("BLUE"))
        {
            hasColor = true;
        }

        if (hasShape && hasColor)
        {
            Vector3 position = new Vector3(0, 0, 0);
            position = CameraTransform.position;
            //position.z += 3;

            Debug.Log("Adding Object at " + position.ToString());

            if (command.ToUpper().Contains("CUBE"))
            {
                if (command.ToUpper().Contains("RED"))
                {
                    Instantiate(cubeRed, position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("GREEN"))
                {
                    Instantiate(cubeGreen, position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("BLUE"))
                {
                    Instantiate(cubeBlue, position, this.transform.rotation);
                }
            }

            else if (command.ToUpper().Contains("SPHERE"))
            {
                if (command.ToUpper().Contains("RED"))
                {
                    Instantiate(sphereRed, position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("GREEN"))
                {
                    Instantiate(sphereGreen, position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("BLUE"))
                {
                    Instantiate(sphereBlue, position, this.transform.rotation);
                }
            }

            else if (command.ToUpper().Contains("CYLINDER"))
            {
                if (command.ToUpper().Contains("RED"))
                {
                    Instantiate(cylinderRed, position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("GREEN"))
                {
                    Instantiate(cylinderGreen, position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("BLUE"))
                {
                    Instantiate(cylinderBlue, position, this.transform.rotation);
                }
            }

        }
        else
        {
            if (!hasShape && !hasColor)
            {
                Debug.Log("Invalid shape and color.");
            }
            else if (!hasShape)
            {
                Debug.Log("Invalid shape.");
            }
            else
            {
                Debug.Log("Invalid color.");
            }
        }
        
    }
}
