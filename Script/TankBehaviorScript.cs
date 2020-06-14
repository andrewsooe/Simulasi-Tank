using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehaviorScript : MonoBehaviour
{
    private Transform myTransform;
    public float kecepatanRotasi = 20;
    public GameObject selongsong;
    public float nilaiRotasiY;
    private string stateRotasiVertical;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        selongsong = myTransform.Find("selongsong").gameObject; //Jika menggunakan variable private
    }

    // Update is called once per frame
    void Update()
    {
        #region Rotasi Horizontal

        if (Input.GetKey(KeyCode.G)) //Rotasi berlawanan jarum jam
        {
            myTransform.Rotate(Vector3.back * kecepatanRotasi * Time.deltaTime, Space.Self);

        }
        else if (Input.GetKey(KeyCode.J)) //Rotasi searah jarum jam
        {
            myTransform.Rotate(Vector3.forward * kecepatanRotasi * Time.deltaTime, Space.Self);
        }
        #endregion


        #region Menentukan State
        nilaiRotasiY = 360 - selongsong.transform.localEulerAngles.x;
        if (nilaiRotasiY == 0 || nilaiRotasiY == 360)
        {
            stateRotasiVertical = "aman";
        }
        else if (nilaiRotasiY > 0 && nilaiRotasiY < 15)
        {
            stateRotasiVertical = "aman";
        }
        else if (nilaiRotasiY > 15 && nilaiRotasiY < 16)
        {
            stateRotasiVertical = "atas";
        }
        else if (nilaiRotasiY > 350)
        {
            stateRotasiVertical = "bawah";
        }
        #endregion
        

        if (stateRotasiVertical == "aman")
        {
            if (Input.GetKey(KeyCode.Y)) //Rotasi berlawanan jarum jam
            {
                selongsong.transform.Rotate(Vector3.left * kecepatanRotasi * Time.deltaTime, Space.Self);

            }
            else if (Input.GetKey(KeyCode.H)) //Rotasi searah jarum jam
            {
                selongsong.transform.Rotate(Vector3.right * kecepatanRotasi * Time.deltaTime, Space.Self);
            }
        }
        else if (stateRotasiVertical == "bawah")
        {
            selongsong.transform.localEulerAngles = new Vector3(
                -0.5f, selongsong.transform.localEulerAngles.y, selongsong.transform.eulerAngles.z);
        }
        else if (stateRotasiVertical == "atas")
        {
            selongsong.transform.localEulerAngles = new Vector3(
                -14.5f, selongsong.transform.localEulerAngles.y, selongsong.transform.eulerAngles.z);
        }

        

    }
}
