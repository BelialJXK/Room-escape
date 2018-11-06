using UnityEngine;

public class FPPcamera : MonoBehaviour
{
    private bool isRotating = false;
    //旋转轴
    public RotationAxes axes = RotationAxes.MouseXAndY;
    private float rotationY = 0F;
    private float rotationX = 0F;
    private float rotation_X = 0F;
    public GameObject a;
    public bool ishiding = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ishiding = false;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ishiding = true;
        }
        if (ishiding)
        {
            
            Rotation();
        }
            
    }
    public enum RotationAxes
    {
        MouseXAndY = 0, MouseX = 1, MouseY = 2
    }

    void Rotation()
    {      
            if (axes == RotationAxes.MouseXAndY)
            {
                rotationX = a.transform.rotation.y;
                rotationY += Input.GetAxis("Mouse Y");
                rotationY = Mathf.Clamp(rotationY, -40, 40);
                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                rotationX = a.transform.rotation.y;

                transform.localEulerAngles = new Vector3(0, rotationX, 0);
                
            }
            else if (axes == RotationAxes.MouseY)
            { 
                
                rotationY = Input.GetAxis("Mouse Y");
                rotationY = Mathf.Clamp(rotationY, -40, 40);
                transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
                             
            }       
    }
}
