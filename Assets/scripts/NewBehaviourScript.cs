using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{

    float valocity = 0.0f;
    public float Accident = 0;
    UnityEngine.Vector3 startpos;
    private float timer;
    int waitingTime;
    bool signalOn90 = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        waitingTime = 2;

        startpos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (CarDistance() < 10)
        {
            Debug.LogWarning(CarDistance());
            Decel();
        }
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            if (signalOn90)
            {
                signalOn90 = false;

            }
            else
            {
                signalOn90 = true;
            }

            timer = 0;
        }


        if (signalOn90) //when signal is green
        {
            if (this.gameObject.name == "car_180")
            {
                Accel();
            }


        }
        else //when signal is re
        {
            if (this.gameObject.name == "car_90")
            {
                Accel();
            }

        }

      

        this.transform.Translate(0.0f, 0.0f, valocity);
    } //Valu

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision with: " + other.name);
        if (other.gameObject.CompareTag("Car"))
        {
            Debug.LogWarning("Crashed with car");

            GameObject.FindWithTag("Car").GetComponent<NewBehaviourScript>().Accident += 1;

            Reset();
            Debug.LogWarning(Accident);
        }
        if (!other.gameObject.CompareTag("Road") && !other.gameObject.CompareTag("RoadEnd"))
        {
            Debug.LogWarning("Didn't Crashed with road");
            Reset();
        }

        if (other.gameObject.CompareTag("RoadEnd"))
        {

            if (signalOn90) //when signal is green
            {
                Debug.LogWarning("Slowing down for TL 180");
                if (this.gameObject.name == "car_180")
                {
                    Decel();
                }

            }

            else
            {
                Debug.LogWarning("Slowing down for TL 90");
                if (this.gameObject.name == "car_90")
                {
                    Decel();
                }
            }
        }

    }
    public List<GameObject> FoundObjects;
    public GameObject closeCar;
    public string TagName;
    public float shortDis;

    private float CarDistance()
    {



        {
            shortDis = 1000;
            FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Car"));
            shortDis = UnityEngine.Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position); // 첫번째를 기준으로 잡아주기 

            closeCar = FoundObjects[0]; // 첫번째를 먼저 

            foreach (GameObject found in FoundObjects)
            {
                float Distance = UnityEngine.Vector3.Distance(gameObject.transform.position, found.transform.position);


                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    if(Distance != 0)
                    {
                        closeCar = found;
                        shortDis = Distance;
                    }
                    
                }
            }
            return shortDis;

        }
    }


    void Reset()
    {
        gameObject.SetActive(false);
        this.transform.position = (startpos);
        gameObject.SetActive(true);



    }
    void Accel()
    {
        valocity += 0.01f;

    }

    void Decel()
    {
        if (valocity > 0.02f)
        {
            valocity -= 1f;
        }


    }
}
