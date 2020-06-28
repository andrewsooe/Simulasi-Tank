using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerakPeluruScript : MonoBehaviour
{
    private Transform myTransform;
    public float waktuTerbangPeluru; //t
    private TankBehaviorScript tankBehavior;
    private float _kecAwal;
    private float _sudutTembak;
    private float _sudutMeriam;
    private Vector3 _posisiAwal;
    private AudioSource audioSource;

    public AudioClip audioLedakan;
    public GameObject ledakan;

    public GameManagerScript gameManager;
    private bool isLanded = true;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;

        tankBehavior = GameObject.FindObjectOfType<TankBehaviorScript>();
        tankBehavior = GameObject.Find("Torque").GetComponent<TankBehaviorScript>();

        _kecAwal = tankBehavior.kecepatanAwalPeluru;
        _sudutTembak = tankBehavior.nilaiRotasiY;
        _posisiAwal = myTransform.position;
        _sudutMeriam = tankBehavior.sudutMeriam;

        audioSource = GetComponent<AudioSource>();

        gameManager = GameObject.FindObjectOfType<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLanded)
        waktuTerbangPeluru += Time.deltaTime;

        gameManager._lamaWaktuTerbangPeluru = waktuTerbangPeluru;

        myTransform.position = PosisiTerbangPeluru(_posisiAwal, _kecAwal, waktuTerbangPeluru, _sudutTembak, _sudutMeriam);
    }

    private Vector3 PosisiTerbangPeluru(Vector3 _posisiAwal, float _kecAwal, float _waktu, 
        float _sudutTembak, float sudutMeriam)
    {
        float _x = _posisiAwal.x + (_kecAwal * _waktu * Mathf.Sin(_sudutMeriam * Mathf.PI / 180));
        float _y = _posisiAwal.y + ((_kecAwal * _waktu * Mathf.Sin(_sudutTembak * Mathf.PI / 180)) - (0.5f * 10 * Mathf.Pow(_waktu, 2)));
        float _z = _posisiAwal.z + (_kecAwal * _waktu * Mathf.Cos(_sudutMeriam * Mathf.PI / 180));

        return new Vector3(_x, _y, _z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Land" || other.tag == "Tree")
        {
            Destroy(this.gameObject, 5f);

            GameObject go = Instantiate(ledakan, myTransform.position, Quaternion.identity);
            Destroy(go, 2f);

            audioSource.PlayOneShot(audioLedakan);

            gameManager._jarakTembak = Vector3.Distance(_posisiAwal, myTransform.position);

            isLanded = false;
        }

        else if(other.tag == "Rumah")
        {
            audioSource.PlayOneShot(audioLedakan);

            GameObject go = Instantiate(ledakan, myTransform.position, Quaternion.identity);
            Destroy(go, 2f);


            Destroy(this.gameObject, 3f);

        }
    }
}
