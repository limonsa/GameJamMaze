using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class Entity
    {
        public bool canMove = false;
        public bool canJump = false;
        public bool isSealed = false;
        public bool isAlive = true;

        public Vector3 position = Vector3.zero;

        public float health = 10;
        public float moveSpeed = 1; //m/s
        
        public float attackSpeed = 1; // attacks per second
        public float attackDMG;
        public float noticeSphere = 10f;
        public bool attackEnd = false;
        public bool canAttack = false;
        public float runSpeed = 2; //m/s

        public string name;

        public bool isRoaring = true;

        public float damagePower; //Add damage impact on the victim's health
        public float attackSpeedPower; //Powers up the speed attack
        public int type; // 1 = weapon, 2 = meme, 3 = life

    }

    public class Player : Entity
    {
        public void SetGeneralValues()
        {
            name = "Player";
            health *= 10;
            moveSpeed *= 2;
            canMove = true;
            canJump = true;
        }

        public void SetAttackSpeed(float attackModifier)
        {
            attackSpeed *= attackModifier;
        }

    }

    public class Zombie : Entity
    {
        public void SetGeneralValues()
        {
            name = "Zombie";
            health *= 4;
            moveSpeed *= .6f;
            canMove = true;
            canJump = true;
            attackDMG = 10;
            noticeSphere = 10;
        }
    }

    public class Golem : Entity
    {
        public void SetGeneralValues()
        {
            name = "Golem";
            health *= 8;
            moveSpeed *= .8f;
            runSpeed *= 1.5f;
            canMove = true;
            canJump = true;
            attackDMG = 25;
            noticeSphere = 20;
        }
       
    }

    /*
     * GAME SECRETS
    **/

    public class BananaGun : Entity
    {
        public void SetGeneralValues()
        {
            name = "BananaGun";
            type = 1;
            damagePower = 2f;
            attackSpeedPower = 3f;
        }
    }

    public class Hammer : Entity
    {
        public void SetGeneralValues()
        {
            name = "Hammer";
            type = 1;
            damagePower = 5f;
            attackSpeedPower = 6f;
        }
    }

    public class Katana : Entity
    {
        public void SetGeneralValues()
        {
            name = "Katana";
            type = 1;
            damagePower = 8f;
            attackSpeedPower = 10f;
        }
    }

    public class Meme : Entity
    {
        public void SetGeneralValues()
        {
            name = "Meme";
            type = 2;
        }
    }

    public class Life : Entity
    {
        public void SetGeneralValues()
        {
            name = "Life";
            type = 3;
            damagePower = -8f; //It heals instead of causing damage
        }
    }




}
