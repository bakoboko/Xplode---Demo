using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamScript : MonoBehaviour {

    [SerializeField]
    int targetCamSize;

    int normalCamSize = 6;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!collision.GetComponent<PlayerCharacterScript>().isZoomedOut)
            {
                if (collision.transform.parent.GetComponentInChildren<Camera>().orthographicSize < targetCamSize)
                {
                    collision.transform.parent.GetComponentInChildren<Camera>().orthographicSize += 0.2f;
                }
            
            }

            if(collision.GetComponent<PlayerCharacterScript>().isZoomedOut)
            {
                if (collision.transform.parent.GetComponentInChildren<Camera>().orthographicSize > normalCamSize)
                {
                    collision.transform.parent.GetComponentInChildren<Camera>().orthographicSize -= 0.2f;
             
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacterScript>().isZoomedOut)
            collision.GetComponent<PlayerCharacterScript>().isZoomedOut = false;
        else
            collision.GetComponent<PlayerCharacterScript>().isZoomedOut = true;
    }
}
