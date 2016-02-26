using UnityEngine;
using System.Collections;

public class marbleController : MonoBehaviour {
    public int x;
    public int percentOfFake;
    public GameObject Marble1;
    private GameObject[] noOfMarbles;
    private bool hasRun = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(createInitialMarbles());
	}

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator createInitialMarbles ()
    {
        for (int i = 0; i < x; i++)
        {
            yield return new WaitForSeconds(1);
            int rand = Random.Range(0, 100);

            if (rand > percentOfFake)
            {
                Marble1.GetComponent<marbleBehavior>().isFake = false;
            }
            else
            {
                Marble1.GetComponent<marbleBehavior>().isFake = true;
            }

            Instantiate(Marble1, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }





}
