using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    private Transform myTransform;
    
    private GameObject selongsong;
    private GameObject titikTembakan;
    private AudioSource audioSource;
    private string stateRotasiVertical;

    public float kecepatanRotasi = 20;
    public float kecepatanAwalPeluru = 20;
    public float nilaiRotasiY; 
    public float sudutMeriam;

    public GameObject objectTembakan;
    public GameObject objectLedakan;
    public GameObject peluruMeriam;
    public AudioClip audioTembakan;
    public AudioClip audioLedakan;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        selongsong = myTransform.Find("selongsong").gameObject; 

        titikTembakan = selongsong.transform.Find("titik tembakan").gameObject;

        audioSource = selongsong.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Rotasi Horizontal

        if (Input.GetKey(KeyCode.A)) //Rotasi berlawanan jarum jam
        {
            myTransform.Rotate(Vector3.back * kecepatanRotasi * Time.deltaTime, Space.Self);

        }
        else if (Input.GetKey(KeyCode.D)) //Rotasi searah jarum jam
        {
            myTransform.Rotate(Vector3.forward * kecepatanRotasi * Time.deltaTime, Space.Self);
        }
        sudutMeriam = myTransform.localEulerAngles.z;
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


        #region Rotasi Vertical
        if (stateRotasiVertical == "aman")
        {
            if (Input.GetKey(KeyCode.W)) //Rotasi berlawanan jarum jam
            {
                selongsong.transform.Rotate(Vector3.left * kecepatanRotasi * Time.deltaTime, Space.Self);

            }
            else if (Input.GetKey(KeyCode.S)) //Rotasi searah jarum jam
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
                -14.5f, selongsong.transform.localEulerAngles.y, 0);
        }
        #endregion

        #region Penembakan
        if (Input.GetKeyDown(KeyCode.Space))
        {
            #region Initiate Peluru
            GameObject peluru = Instantiate(peluruMeriam, titikTembakan.transform.position,
                    Quaternion.Euler(selongsong.transform.localEulerAngles.x, myTransform.localEulerAngles.z, 0));
            #endregion

            #region Initiate Objek Tembakan
            GameObject efekTembakan = Instantiate(objectTembakan, titikTembakan.transform.position,
                    Quaternion.Euler(selongsong.transform.localEulerAngles.x, myTransform.localEulerAngles.z, 0));
            Destroy(efekTembakan, 2f);
            #endregion

            #region Initiate Audio Tembakan
            audioSource.PlayOneShot(audioTembakan);
            #endregion
        }

        #endregion
    }
}
