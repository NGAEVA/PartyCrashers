using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    // Player stats
    public string m_PlayerName;
    public float m_AttackSpeed;
    public int m_Gold;
    public int m_Health;
    private Weapon m_Weapon;
    private Character m_Character;

    //Input
    public string m_PrimaryAttack;
    public string m_SecondaryAttack;
    public string m_Interact;
    public string m_Silly;
    public string m_Stats;
    public string m_Pause;

	// Use this for initialization
	void Start () {
        m_Weapon = GetComponent<Weapon>();
        m_Character = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown(m_PrimaryAttack))
        {
        }
	}
}
