using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private TextMeshPro counterText;
    [SerializeField] private GameObject stickMan;
    public Transform player;
    private int countStickmans;

    // ====================================
    [SerializeField] float distanceBetween;
    [SerializeField] float radius;

    private void StickmanFormation()
    {
        for (int i = 0; i < player.childCount; i++)
        {
            var x = distanceBetween * Mathf.Sqrt(i) * Mathf.Cos(i * radius);
            var z = distanceBetween * Mathf.Sqrt(i) * Mathf.Sin(i * radius);

            var newPos = (x, 1f, z);

            //player.transform.GetChild(i)
        }
    }

    private void Start()
    {
        player = transform;
        counterText = GetComponentInChildren<TextMeshPro>();
        countStickmans = transform.childCount - 1;

        counterText.text = countStickmans.ToString();
    }



    private void MakeStickman(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(stickMan, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity, transform);
        }

        countStickmans = transform.childCount - 1;
        counterText.text = countStickmans.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false;

            var gateScript = other.GetComponent<Gate>();

            if (gateScript.gateType == GateType.Multiply)
            {
                MakeStickman(countStickmans * gateScript.randomNumber);
            }
            else
            {
                MakeStickman(countStickmans + gateScript.randomNumber);
            }
        }
    }

}
