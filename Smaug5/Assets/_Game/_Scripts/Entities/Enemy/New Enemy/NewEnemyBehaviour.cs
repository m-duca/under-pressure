using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class NewEnemyBehaviour : MonoBehaviour
{
    #region Global Variables
    // Inspector:
    [Header("Configurações:")]
    //Tamanho da visão do inimigo
    [Header("Visão do Inimigo:")]
    [SerializeField] private float lookRadius = 20f;

    [Header("Grupo de WayPoints:")]
    public GameObject waypointsGroup;

    [Header("Sons:")] 
    [SerializeField] private float screamInterval = 20f;

    //[Header("Ataque:")] 
    //[SerializeField] private float resetAttackTime = 1.25f;

    // Componentes:
    private NavMeshAgent _agent;
    private BoxCollider[] _boxColliders;

    // Referências:
    private Transform _player;
    private Animator _enemyAnimator;
    private Transform _enemyTransform;

    // Sfx:
    private int _enemySfxIndex = 1;
    private bool _canScream = true;
    private int curSfxIndex = 1;
    #endregion

    #region Default Methods
    private void Start()
    {
        _enemyAnimator = GetComponent<Animator>();
        _enemyTransform = _enemyAnimator.gameObject.transform;
        _player = PlayerManager.instance.player.transform;
        _agent = GetComponent<NavMeshAgent>();
        _boxColliders = GetComponentsInChildren<BoxCollider>();
    }
    #endregion

    #region Custom Methods

    //Gerando número aleatório para colocar no tempo de idle
    public int RandomInteger(int min, int max)
    {
        return Random.Range(min, max);
    }

    // Retorna a lista de waypoints do inimigo
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform t in waypointsGroup.transform)
        {
            waypoints.Add(t);
        }
        return waypoints;
    }


    public void disableAttack()
    {
         if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("enemy attack " + curSfxIndex);
            if (curSfxIndex == 1) curSfxIndex = 2;
            else curSfxIndex = 1;
        }
        foreach (BoxCollider collider in _boxColliders)
        {
            collider.enabled = false;
            //Debug.Log("Desativou Colisor");
        }
    }

    public void enableAttack()
    {
        foreach (BoxCollider collider in _boxColliders)
        {
            collider.enabled = true;
            //Debug.Log("Ativou Colisor");
        }
    }

    //Apenas visual do raio de visão
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    #endregion

}
