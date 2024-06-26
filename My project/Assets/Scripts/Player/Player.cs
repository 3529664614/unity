using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private Behaviour[] componentsToDisable;
    private bool[] componentsEnabled;
    private bool colliderEnabled;
    private NetworkVariable<int> currentHealth = new NetworkVariable<int>();

    private NetworkVariable<bool> isDead = new NetworkVariable<bool>();
    public void Setup()
    {
        componentsEnabled = new bool[componentsToDisable.Length];
        for (int i = 0; i < componentsEnabled.Length; i++)
        {
            componentsEnabled[i] = componentsToDisable[i].enabled;
        }
        Collider col = GetComponent<Collider>();
        colliderEnabled = col.enabled;
        SetDefaults();
    }

    private void SetDefaults()
    {
        for (int i = 0; i <  componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = componentsEnabled[i]; 
        }
        Collider col = GetComponent<Collider>();
        col.enabled = colliderEnabled;
        if (IsServer)
        {
            currentHealth.Value = maxHealth;
            isDead.Value = false;
        }
    }

    public int GetHealth()
    {
        return currentHealth.Value;
    }

    public bool IsDead()
    {
        return isDead.Value;
    }

    public void TakeDamage(int damage)
    {
        currentHealth.Value -= damage;

        if (currentHealth.Value <= 0)
        {
            currentHealth.Value = 0;
            isDead.Value = true;

            DieOnServer();
            DieClientRpc();

        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.Singleton.MatchingSetting.respawnTime);
        SetDefaults();
        GetComponentInChildren<Animator>().SetInteger("direction", 0);
        GetComponent<Rigidbody>().useGravity = true;
        if (IsLocalPlayer)
        {
            transform.position = new Vector3(0f, 10f, 0f);
        }
    }

    private void DieOnServer()
    {
        Die();
    }
    [ClientRpc]
    private void DieClientRpc()
    {
        Die();
    }

    private void Die()
    {
        GetComponent<PlayerShooting>().StopShooting();
        GetComponentInChildren<Animator>().SetInteger("direction", -1);
        GetComponent<Rigidbody>().useGravity = false; 
        for (int i = 0; i < componentsEnabled.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
        Collider col = GetComponent<Collider>();
        col.enabled = false;
        StartCoroutine(Respawn());
    }
}
