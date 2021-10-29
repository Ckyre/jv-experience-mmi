using UnityEngine;

public class MoveMonsterPositionTrigger : Trigger
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float moveSpeed;

    private Transform monsterDome;
    private bool isMoving = false;

    private void Awake()
    {
        monsterDome = GameObject.FindGameObjectWithTag("MonsterDome").transform;
    }

    protected override void OnPlayerEnter(Collider col)
    {
        base.OnPlayerEnter(col);

        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (Quaternion.Angle(monsterDome.rotation, Quaternion.Euler(rotation)) < 10f)
                isMoving = false;

            monsterDome.rotation = Quaternion.Lerp(monsterDome.rotation, Quaternion.Euler(rotation), moveSpeed * Time.deltaTime);
        }
    }
}
