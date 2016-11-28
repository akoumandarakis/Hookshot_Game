using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GUIScript : MonoBehaviour
{

    public GameObject Player;
    private HealthScript PlayerHP;

    public GameObject HealthBar;
    private Vector3 originalHPScale;


    void Start()
    {
        PlayerHP = Player.GetComponent<HealthScript>();
        originalHPScale = HealthBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHP != null)
        {
            float maxHP = (float)PlayerHP.maxHP;
            float HP = (float)PlayerHP.hp;
            float scale = HP / maxHP;

            Vector3 newLocalScale = new Vector3(originalHPScale.x * scale,
                                                originalHPScale.y,
                                                0);
            HealthBar.transform.localScale = newLocalScale;
        }
        else
        {
            Vector3 newLocalScale = new Vector3(originalHPScale.x * .01F,
                                                originalHPScale.y,
                                                0);
            HealthBar.transform.localScale = newLocalScale;
        }
    }
}