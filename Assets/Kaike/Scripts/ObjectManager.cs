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
            if (command.ToUpper().Contains("CUBE"))
            {
                if (command.ToUpper().Contains("RED"))
                {
                    Instantiate(cubeRed, this.transform.position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("GREEN"))
                {
                    Instantiate(cubeGreen, this.transform.position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("BLUE"))
                {
                    Instantiate(cubeBlue, this.transform.position, this.transform.rotation);
                }
            }

            else if (command.ToUpper().Contains("SPHERE"))
            {
                if (command.ToUpper().Contains("RED"))
                {
                    Instantiate(sphereRed, this.transform.position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("GREEN"))
                {
                    Instantiate(sphereGreen, this.transform.position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("BLUE"))
                {
                    Instantiate(sphereBlue, this.transform.position, this.transform.rotation);
                }
            }

            else if (command.ToUpper().Contains("CYLINDER"))
            {
                if (command.ToUpper().Contains("RED"))
                {
                    Instantiate(cylinderRed, this.transform.position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("GREEN"))
                {
                    Instantiate(cylinderGreen, this.transform.position, this.transform.rotation);
                }
                else if (command.ToUpper().Contains("BLUE"))
                {
                    Instantiate(cylinderBlue, this.transform.position, this.transform.rotation);
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
