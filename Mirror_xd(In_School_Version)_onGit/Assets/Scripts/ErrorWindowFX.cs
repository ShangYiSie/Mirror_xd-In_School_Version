using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ErrorWindowFX : MonoBehaviour
{
    public float _radius_length;
    public float _angle_speed;

    private float temp_angle;

    private Vector3 _pos_new;

    public Vector3 _center_pos;

    public bool _round_its_center;

    public GameObject PreErrorWindow;

    public Canvas BlueLayerCanvas;

    GameObject[] Errorwindows = new GameObject[200];

    VisualManager visualManager;
    AudioManager audioManager;

    int counter = 0;

    public static bool showerrorwin = false;

    // Use this for initialization
    void Start()
    {
        showerrorwin = false;
        visualManager = GameObject.Find("VisualManager").GetComponent<VisualManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (_round_its_center)
        {
            _center_pos = transform.localPosition;
        }
        for (int i = 0; i < 200; i++)
        {
            Errorwindows[i] = Instantiate(PreErrorWindow, new Vector3(-15, 5, 0), Quaternion.identity, GameObject.Find("Clone").transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showerrorwin)
        {

            temp_angle += _angle_speed * Time.deltaTime; // 

            _pos_new.x = _center_pos.x + Mathf.Cos(temp_angle) * _radius_length;
            _pos_new.y = _center_pos.y + Mathf.Sin(temp_angle) * _radius_length;
            _pos_new.z = transform.localPosition.z;


            if (counter < 40)
            {
                // =====================================
                // 觸發生成錯誤視窗的音效
                audioManager.ClickGUESTandShowErr();
                // =====================================
                // _radius_length += 0.05f;
                _angle_speed -= 0.01f;
                // Instantiate(PreErrorWindow, _pos_new, Quaternion.identity, GameObject.Find("Clone").transform);
                Errorwindows[counter].transform.position = _pos_new;
                counter++;
            }
            else if (counter < 60)
            {
                // =====================================
                // 觸發生成錯誤視窗的音效
                audioManager.ClickGUESTandShowErr();
                // =====================================
                _radius_length += 0.02f;
                _angle_speed -= 0.02f;
                // Instantiate(PreErrorWindow, _pos_new, Quaternion.identity, GameObject.Find("Clone").transform);
                Errorwindows[counter].transform.position = _pos_new;
                counter++;
            }
            else if (counter < 150)
            {
                // =====================================
                // 觸發生成錯誤視窗的音效
                audioManager.ClickGUESTandShowErr();
                // =====================================

                _radius_length += 0.02f;
                _radius_length += 0.02f;
                // Instantiate(PreErrorWindow, _pos_new, Quaternion.identity, GameObject.Find("Clone").transform);
                Errorwindows[counter].transform.position = _pos_new;
                counter++;
            }
            else if (counter < 200)
            {
                // =====================================
                // 觸發生成錯誤視窗的音效
                audioManager.ClickGUESTandShowErr();
                // =====================================

                _radius_length += 0.02f;
                _radius_length += 0.08f;
                // Instantiate(PreErrorWindow, _pos_new, Quaternion.identity, GameObject.Find("Clone").transform);
                Errorwindows[counter].transform.position = _pos_new;
                counter++;
            }

            if (counter == 200)
            {
                showerrorwin = false;
                counter++;
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        // BlueLayerCanvas.sortingOrder = 6;
        this.transform.parent.parent.Find("BlueLayer").gameObject.SetActive(true);
        visualManager.StartCoroutine(visualManager.SceneChangeToCH2());
        Destroy(this);
    }

}
