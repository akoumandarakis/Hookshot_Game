using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour
{

    public GameObject Player;
	public GameObject Boss;
    private HealthScript PlayerHP;
	private HealthScript BossHP;
	private WeaponScript weapon;
	private InventoryScript playerInventory;

    public GameObject HealthBar;
	public GameObject BossHealthBar;
	public GameObject AmmoType;
	private SpriteRenderer ammoSprite;
	public GameObject AmmoCounter;
    private Vector3 originalHPScale;
	private Vector3 originalBossHPScale;

	public GameObject KeycardIndicator1;
	public GameObject KeycardIndicator2;
	public GameObject KeycardIndicator3;
	public GameObject KeycardIndicator4;
	public GameObject KeycardIndicator5;

	public bool ScaleToZoom;
	private bool scaled;

    void Start()
    {
        PlayerHP = Player.GetComponent<HealthScript>();
        originalHPScale = HealthBar.transform.localScale;
		originalBossHPScale = BossHealthBar.transform.localScale;
		weapon = Player.GetComponentInChildren<WeaponScript> ();
		ammoSprite = AmmoType.GetComponent<SpriteRenderer> ();
		playerInventory = Player.GetComponent<InventoryScript> ();
		BossHP = Boss.GetComponent<HealthScript> ();

		KeycardIndicator1.SetActive (false);
		KeycardIndicator2.SetActive (false);
		KeycardIndicator3.SetActive (false);
		KeycardIndicator4.SetActive (false);
		KeycardIndicator5.SetActive (false);

    }

    // Update is called once per frame
    void Update()
    {
		if (ScaleToZoom && !scaled) {
			this.transform.localScale = new Vector3 (this.transform.localScale.x + 0.0025f, this.transform.localScale.y + 0.0025f, this.transform.localScale.z);

			if (this.transform.localScale.x >= 1.5)
			{
				scaled = true;
			}
		}

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

		if (weapon != null) {
			ammoSprite.sprite = weapon.shotPrefab.GetComponent<SpriteRenderer> ().sprite;

			int ammo = weapon.shotTypes [weapon.shotIndex].Value;

			if (ammo >= 0) {
				AmmoCounter.GetComponent<Text> ().text = weapon.shotTypes [weapon.shotIndex].Value.ToString();
			} else {
				AmmoCounter.GetComponent<Text> ().text = "Inf.";
			}
		}

		if (playerInventory != null) {

			if (playerInventory.inventory.Contains ("Key1")) {
				KeycardIndicator1.SetActive (true);
			}

			if (playerInventory.inventory.Contains ("Key2")) {
				KeycardIndicator2.SetActive (true);
			}

			if (playerInventory.inventory.Contains ("Key3")) {
				KeycardIndicator3.SetActive (true);
			}

			if (playerInventory.inventory.Contains ("Key4")) {
				KeycardIndicator4.SetActive (true);
			}

			if (playerInventory.inventory.Contains ("Key5")) {
				KeycardIndicator5.SetActive (true);
			}
		}

		if (BossHP != null) {
			float maxBossHP = (float)BossHP.maxHP;
			float BossCurrentHP = (float)BossHP.hp;
			float scale = BossCurrentHP / maxBossHP;

			Vector3 newLocalScale = new Vector3(originalBossHPScale.x * scale,
				originalHPScale.y,
				0);
			BossHealthBar.transform.localScale = newLocalScale;
		}
		else
		{
			Vector3 newLocalScale = new Vector3(originalBossHPScale.x * .01F,
				originalHPScale.y,
				0);
			BossHealthBar.transform.localScale = newLocalScale;
		}

    }
}