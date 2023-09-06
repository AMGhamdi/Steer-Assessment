using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private TextMesh healthText;
    [SerializeField] private Unit unitData;
    [SerializeField] private Teams team;

    List<GameObject> enemyTeam;
    List<GameObject> unitTeam;
    UnitController opponent;

    Transform target;
    private float HP;
    float attackSpeed;

    bool gameOver = false; // whether or not there is still enemies in the map


    #region Unity Methods
    
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = team.teamMaterial;
        initializeTeam();
        HP = unitData.HP;
        attackSpeed = unitData.attackSpeed;
        var text = Instantiate(healthText.gameObject, transform.position, Quaternion.identity, gameObject.transform);
         healthText = text.GetComponent<TextMesh>();
         healthText.text = HP.ToString("F0");
    }
    void Update()
    {
        if (GameManager.gameState.Equals(GameManager.GameState.GameOn))
        {
            if (opponent == null)
            {
                FindRandomEnemy();
            }
            if (!gameOver)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, unitData.movementSpeed * Time.deltaTime);
                AttackDistance();
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (healthText != null)
        {
            healthText.text = HP.ToString("F0");
        }
        
    }
    #endregion
    #region Team Related Selection Methods
    void initializeTeam()
    {
        if (team.teamName == Teams.Team.Red)
        {
            BattleController.instance.redTeam.Add(gameObject);
            enemyTeam = BattleController.instance.blueTeam;
            unitTeam = BattleController.instance.redTeam;
        }
        else
        {
            BattleController.instance.blueTeam.Add(gameObject);
            enemyTeam = BattleController.instance.redTeam;
            unitTeam = BattleController.instance.blueTeam;
        }
    }
    void FindRandomEnemy()
    {
        int numOfEnemies = enemyTeam.Count;
        if (numOfEnemies > 0)
        {
            int enemyPointer = Random.Range(0, numOfEnemies - 1);
            opponent = enemyTeam[enemyPointer].GetComponent<UnitController>();
            target = opponent.transform;
        }
        else
        {
            gameOver = true;
            FindObjectOfType<UI>().GameEnd();
        }

    }
    #endregion

    #region Combat Related Methods
    void AttackDistance()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= unitData.attackRange)
        {
            Attack();
            target = transform;
        }
        else
        {
            target = opponent.transform;
        }
    }
    void Attack()
    {
        if (attackSpeed <= 0)
        {
            opponent.RegisterHit(unitData.attackDamage);
            attackSpeed = unitData.attackSpeed;
        }
        else
        {
            attackSpeed -= Time.deltaTime;
        }

    }
    void RegisterHit(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            DestroyUnit();
        }
    }
    public void DestroyUnit()
    {
        unitTeam.Remove(gameObject);
        Destroy(gameObject);
    }
    #endregion
}
