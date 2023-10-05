using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Player : Character
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody rb;
    [SerializeField] LineRenderer line;
    public LayerMask layerObstacle;
    public FloatingJoystick joystick;
    List<Collider> obstacles = new List<Collider>();
    bool isMove = false;
    float radius;
    private void Awake()
    {
        ChangeSkin();
    }
    void Start()
    {
        ChangeAnim(Const.DANCE);
    }
    public override void OnInit()
    {
        target = null;
        gameObject.SetActive(true);
        base.OnInit();
        isMove = false;
        radius = attackRange;
        CameraFollow.dist = 1;
        ChangeAnim(Const.IDLE);


    }
    public void ChangeSkin()
    {
        (HairType hair, PantType pant, SkinColor skin) = DataManager.Instance.GetLastOb();
        WeaponType weapon = DataManager.Instance.GetLastWeapon();
        ChangeHair(hair);
        ChangePant(pant);
        ChangeWeapon(weapon);
        ChangeSkin(skin);
    }
    private void Update()
    {
        if(GameManager.IsState(GameState.Gameplay))
        {
            if (!isMove)
            {
                CheckAttack();
                FindTarget();
                if (target != null && hasBullet)
                {
                    Attack();
                }
                else
                {
                    if (!isAttack)
                        ChangeAnim(Const.IDLE);
                }
            }
            else
            {
                time = 0;
            }
            CheckObstacle(attackRange);
        }
        
    }
    void FixedUpdate()
    {   

        if (!isDead && GameManager.IsState(GameState.Gameplay))
        {
            Vector3 velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);
            rb.velocity = velocity;
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                hasBullet = true;
                isMove = true;
                isAttack = false;
                ChangeAnim(Const.RUN);
                rb.rotation = Quaternion.LookRotation(velocity);
                
            }
            else
            {
                isMove = false;               
                
            }
        }
        
    }
    private void LateUpdate()
    {
        DrawCircle(radius);
        

    }
    public override void OnDeath()
    {
        base.OnDeath();
        GameManager.ChangeState(GameState.Pause);
        rb.velocity = Vector3.zero;
    }
    public override void OnDespawn()
    {
        gameObject.SetActive(false);
        UIManager.Instance.OpenUI<Lose>().SetScore(LevelManager.Instance.point);
        UIManager.Instance.CloseUI<InGame>();


    }
    public override void LevelUp()
    {
        base.LevelUp();
        if(level<5)
        {
            CameraFollow.dist *= 1.1f;
        }
    }
    private void DrawCircle(float radius)
    {
        var segments = 360;
        line.useWorldSpace = false;
        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
        line.startColor = Color.black;
        line.endColor = Color.black;
        line.positionCount = segments + 1;
        var pointCount = segments + 1;
        var points = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius)+Vector3.up*0.3f;
        }
        line.SetPositions(points);
    }
    void CheckObstacle(float radius)
    {
       List<Collider> colliders = Physics.OverlapSphere(tf.position, radius, layerObstacle).ToList();
        foreach(var colid in colliders)
        {
            Cache.GetObstacle(colid)?.Faded();
        }
       foreach(var colid in obstacles)
        {
            if(!colliders.Contains(colid))
            {
                Cache.GetObstacle(colid)?.GetColorBack();
            }
        }


       obstacles = colliders;




    }

}
