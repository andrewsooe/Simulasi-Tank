using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerakPeluru : MonoBehaviour
{
    private Transform myTransform;
    public float waktuTerbangPeluru; 
    private TankBehavior tankBehavior;
    private float _kecAwal;
    private float _sudutTembak;
    private float _sudutMeriam;
    private Vector3 _posisiAwal;
    private AudioSource audioSource;

    public AudioClip audioLedakan;
    public GameObject ledakan;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;

        tankBehavior = GameObject.FindObjectOfType<TankBehavior>();
        tankBehavior = GameObject.Find("torque").GetComponent<TankBehavior>();

        _kecAwal = tankBehavior.kecepatanAwalPeluru;
        _sudutTembak = tankBehavior.nilaiRotasiY;
        _posisiAwal = myTransform.position;
        _sudutMeriam = tankBehavior.sudutMeriam;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        waktuTerbangPeluru += Time.deltaTime;

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
        if(other.tag == "Land")
        {
            Destroy(this.gameObject, 2f);

            GameObject go = Instantiate(ledakan, myTransform.position, Quaternion.identity);
            Destroy(go, 2f);

            audioSource.PlayOneShot(audioLedakan);
        }
    }
}
