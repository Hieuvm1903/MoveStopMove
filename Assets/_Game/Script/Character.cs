using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Character : GameUnit
{
    string currentAnim;
    public bool hasBullet = true;
    public bool isDead = false;
    public bool isAttack;
    public float attackRange;
    public float time;
    public float speed = 5;
    public int level;
    public LayerMask layer;
    public Bullet bullet;
    public Transform throwPoint;
    public Transform hand;
    public Transform head;
    public Weapon weaponHand;
    public SkinnedMeshRenderer pant;
    public SkinnedMeshRenderer skin;
    public GameObject hair;
    public Character target;
    public Animator animator;
    public Collider colider;
    void Awake()
    {
        colider = GetComponent<Collider>();
    }
    public virtual void OnInit()
    {
        transform.localScale = Vector3.one;
        colider.enabled = true;
        enabled = true;
        isDead = false;
        isAttack = false;
        hasBullet = true;
        attackRange = 7;
        time = 0;
        level = 0;
    }
    public void FindTarget()
    {
        List<Collider> colliders = Physics.OverlapSphere(tf.position, attackRange, layer).ToList();       
        if (colliders.Count > 1)
        {
            colliders.Sort((x, y) => Vector3.Distance(tf.position, x.transform.position).CompareTo(Vector3.Distance(tf.position, y.transform.position)));
           // target = colliders[1].GetComponent<Character>();
            
           target = Cache.CharacterCollider(colliders[1]); 
            
        }
        else
            target = null;


    }
    public void LookRotate()
    {     
       tf.rotation = Quaternion.LookRotation(new Vector3 (target.tf.position.x - tf.position.x, 0, target.tf.position.z - tf.position.z));
    }
    public void Attack()
    {        
        isAttack = true;
        LookRotate();
        ChangeAnim(Const.ATTACK);        
    }
    void Throw()
    {
        
        hasBullet = false;
        weaponHand.gameObject.SetActive(false);      
        Bullet bulletFire = PoolLearning.Spawn(this.bullet.type, throwPoint.position, tf.rotation) as Bullet;
        bulletFire.tf.localScale = tf.localScale;
        bulletFire.character = this;
        bulletFire.direct = tf.forward;
        bulletFire.OnInit(attackRange);
        Invoke(nameof(ResetAttack), 0.4f);
    }
    public void CheckAttack()
    {
        if (isAttack)
        {
            time += GameManager.deltaTime;
            if (time >= 0.4f && hasBullet)
            {
                Throw();
            }
        }
        
        

    }
    void ResetAttack()
    {
        isAttack = false;
        time = 0;
        weaponHand.gameObject.SetActive(true);
    }
    public virtual void OnDespawn()
    {       
            //Destroy(gameObject);
            //Destroy(this);
        // gameObject.SetActive(false);
        PoolLearning.DeSpawn(this);

    }
    public virtual void OnDeath()
    {
        colider.enabled = false;
        isDead = true;
        enabled = false;
        ChangeAnim(Const.DEAD);
        Invoke(nameof(OnDespawn), 2f);
    }
    public void ChangeAnim(string anim)
    {
        if (currentAnim == null)
        {
            currentAnim = anim;
            animator.SetTrigger(currentAnim);
        }
        else
        {
            if (currentAnim != anim)
            {
                animator.ResetTrigger(currentAnim);
                currentAnim = anim;
                animator.SetTrigger(currentAnim);

            }
        }

    }
    public virtual void LevelUp()
    {
        if(level <5)
        {
            level++;
            attackRange *= 1.1f;
            tf.localScale *= 1.1f;
        }
    }
    public void ChangeHair(HairType type)
    {
        if(hair != null)
        {
            Destroy(hair);
        }
        hair = Instantiate(DataManager.Instance.Hair(type).hairPrefab, head);

    }
    public void ChangePant(PantType type)
    {
        pant.material = DataManager.Instance.Pant(type).material;
    }
    public void ChangeWeapon(WeaponType type)
    {
        this.bullet = DataManager.Instance.BulletData((PoolType)type) ;
        if(weaponHand != null)
        {
            Destroy(weaponHand.gameObject);
            Destroy(weaponHand);

        }
        weaponHand = Instantiate(DataManager.Instance.Weapon(type), hand);
    }
    public void ChangeSkin(SkinColor color)
    {
        skin.material = DataManager.Instance.Skin(color).skin;
    }
    public void ChangeRandomSkin()
    {
        (HairType hair,PantType pant,SkinColor skin, WeaponType weapon) = DataManager.Instance.RandomSkin();
        ChangeHair(hair);
        ChangePant(pant);
        ChangeWeapon(weapon);
        ChangeSkin(skin);

    }

    
}
