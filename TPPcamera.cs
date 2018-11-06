using UnityEngine;

public class TPPcamera : MonoBehaviour {
    public Transform FPPCamera;
    Vector3 offset;
    RaycastHit hit;
    public float distance = 0;
    public float scrollspeed = 10;//鼠标滚轮拉近拉远的速度
    private bool isRotating = false;
    public float rotateSpeed = 2F;//摄像机绕着角色旋转时的旋转速度
    Vector3 X = new Vector3(0.1f, 0, 0);
    Vector3 Z = new Vector3(0, 0, 0.1f);
    void Start () {
        offset = transform.position -FPPCamera.position;
    }
	
	void Update () {
        transform.position = FPPCamera.position + offset;
        RotateView(); 
        CameraWithWall();           
    }
   
    void CameraWithWall()
    {
        //用射线碰撞检测相机和人物之间的障碍物
        bool grounded = Physics.Linecast(FPPCamera.position, transform.position);
        if (!grounded)
        {   //nothing between of camera and player          
            transform.Translate(Vector3.forward * (0.010f* -Vector3.Distance(FPPCamera.position, transform.position)));
        }
        else
        {    //The wall between of camera and player
            if (Physics.Raycast(FPPCamera.position, transform.TransformDirection(-Vector3.forward), out hit))
            {   //make sure the camera between wall and player
                if (hit.point.z == -6f)
                {
                    transform.position = hit.point + Z;
                }
                else if (hit.point.x == 3.2f)
                {
                    transform.position = hit.point;
                }
                else if (hit.point.x == -3.2f)
                {
                    transform.position = hit.point;
                    //Debug.LogError(3);
                }
                else if (hit.point.z == 6.28f)
                {
                    transform.position = hit.point - Z;
                }
                else
                {
                    transform.position = hit.point;
                }
            }
        }
    }
    
    void RotateView()
    {      
        if (Input.GetKeyDown(KeyCode.LeftAlt))//你需要在Update方法中调用这个方法，此后每一帧重置状态时，它将不会返回true除非用户释放这个鼠标按钮然后重新按下它。
        {
            isRotating = true;
        }       
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            isRotating = false;
        }


        if (isRotating)
        {                           //某位置           //某轴            //旋转度数
            transform.RotateAround(FPPCamera.position, transform.up, rotateSpeed * Input.GetAxis("Mouse X"));//简单的说，在世界坐标的某位置的某轴按照旋转度数旋转调用这个函数的物体。
            Vector3 originalPos = transform.position; Quaternion originalRotation = transform.rotation;
            transform.RotateAround(FPPCamera.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));//影响本物体transform的属性有两个，一个是position 一个是rotation
            float x = transform.eulerAngles.x;
            if (x < 0 || x > 80)//垂直方向角度限制:不论怎么旋转都限制在0到80度之间
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
            x = Mathf.Clamp(x, 0, 80);//限制x的值在0和90之间， 如果x小于0，返回0。 如果x大于80，返回80，否则返回value 
            transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);//测试过之后发现上面两句的没什么影响，可能是因为有了下面transform.LookAt(player);这句的存在
        }
        offset = transform.position - FPPCamera.position;//因为旋转后相对位置变了，要保持相对位置，原来的offsetposition已经不适用
        transform.LookAt(FPPCamera);//保证不论怎么旋转都是角色在摄像机视野的正中心,如果去掉则会出现角色不在正中心的现象
    }
}
