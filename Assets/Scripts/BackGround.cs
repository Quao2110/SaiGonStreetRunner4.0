using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform mainCam;
    public Transform migBg;
    public Transform sideBg;
    public float length;

    void Update()
    {
        if(mainCam.position.x > migBg.position.x)
        {
           UpdateBachgroundPosition(Vector3.right);
        }
        else if (mainCam.position.x < migBg.position.x)
        {
           UpdateBachgroundPosition(Vector3.left);
        }

    }
    void UpdateBachgroundPosition(Vector3 direction)
    {
       sideBg.position = migBg.position + direction * length;
       Transform temp = migBg;
       migBg = sideBg;
       sideBg = temp;
    }
}
