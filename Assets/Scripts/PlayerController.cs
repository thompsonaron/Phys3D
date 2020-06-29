using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : Collisions
{
    public GameObject bullet;

    public float objMass;
    float objSpeed = 50f;
    float dragCoeffGround = 0.1f;
    public Position p, p0;
    public Velocity v0, v;
    bool isMoveRight = true, isMoveLeft = true, isMoveUp = true, isMoveDown = true;
    bool isGrounded = false, isJumping = false;
    public Action onFire;
    private GameObject playerController;
    private float topVelocityLimit = 7f;
    private float collisionCorrection = 0.1f;

    // FORCES
    float Wf;
    float Fbou;

    // BUOYANCY
    public TypeOfFluid typeOfFluid;
    public TypeOfMaterial typeOfMaterial;
    float densityOfMaterial = 10f;
    float densityOfFluid = 10f;
    bool isSwimmingEnabled = false;


    public float buoyancyF;
    private float dragF;
    private float playerDragCoeff = 0.8f;
    private float acc;
    public float objVol;

    private void Start()
    {
        playerController = this.gameObject;
        SetFluid();
        SetMaterial();
        SetMovement();
        SetProperties();
    }

    private void SetProperties()
    {
        objVol = transform.localScale.x * transform.localScale.y * transform.localScale.z;
        objMass = objVol * densityOfMaterial;
        dragF = (playerDragCoeff * densityOfFluid * objVol * (10 * 10)) * 0.5f;
    }

    private void SetMovement()
    {
        p0 = new Position(transform.position.x, transform.position.y, transform.position.z);
        p = p0;
        v0 = new Velocity(0, 0, 0);
        v = v0;
    }

    private void Update()
    {
        // Movement
        if (Input.GetKey(KeyCode.LeftArrow) && isMoveLeft) { v.x -= objSpeed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.RightArrow) && isMoveRight) { v.x += objSpeed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.UpArrow) && isMoveUp) { v.z += objSpeed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.DownArrow) && isMoveDown) { v.z -= objSpeed * Time.deltaTime; }

        // Velocity Limiter
        if (v.x > topVelocityLimit) { v.x = topVelocityLimit; }
        if (v.x < -topVelocityLimit) { v.x = -topVelocityLimit; }
        if (v.z > topVelocityLimit) { v.z = topVelocityLimit; }
        if (v.z < -topVelocityLimit) { v.z = -topVelocityLimit; }

        p.x = p0.x + v0.x * Time.deltaTime;
        p.z = p0.z + v0.z * Time.deltaTime;
        transform.position = new Vector3(p0.x, p0.y, p0.z);

        // Collisions
        CollisionsHandling();

        // Water Collisions and bouyancy
        BouyancyHandling();

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)  { isJumping = true;isGrounded = false;v.y = 10f;}
        if (isJumping){p.y = p0.y + v.y * Time.deltaTime - PhyConstants.gravity / 2 * Time.deltaTime * Time.deltaTime;}

        // Projectile
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        // Friction
        if (isGrounded)
        {
            isJumping = false;
            if (v.x > dragCoeffGround) {v.x -= dragCoeffGround;}
            if (v.x < -dragCoeffGround) {v.x += dragCoeffGround;}
            if (v.x >= -dragCoeffGround && v.x <= dragCoeffGround) {v.x = 0;}
            if (v.z > dragCoeffGround) {v.z -= dragCoeffGround;}
            if (v.z < -dragCoeffGround) {v.z += dragCoeffGround;}
            if (v.z >= -dragCoeffGround && v.z <= dragCoeffGround) {v.z = 0;}
        }

        // Updating
        p0 = p;
        v0 = v;
    }

    private void BouyancyHandling()
    {
        if (IsCollidingWaterBottom(playerController))
        {
            Fbou = displacedV * densityOfFluid * PhyConstants.gravity;
            Wf = objMass * (-PhyConstants.gravity) ;
        }

        if (isInWater)
        {
            if (Mathf.Abs(Fbou) > Mathf.Abs(Wf)){isSwimmingEnabled = true;}
            v.y = (v0.y + Fbou + Wf )* 0.99f;
            if (Mathf.Abs(Wf) == Mathf.Abs(Fbou)){v.y = 0f;}
        }
    }

    private void CollisionsHandling()
    {

        // Up and down
        if (CollidingBottomYAxis(playerController))
        {
            v.y = 0;
            isGrounded = true;
            isInWater = false;
            isSwimmingEnabled = false;
        }
        else if (!isSwimmingEnabled)
        {
            isGrounded = false;
            v.y = v0.y - PhyConstants.gravity * Time.deltaTime;
            p.y = p0.y + v0.y * Time.deltaTime - (PhyConstants.gravity / 2) *Time.deltaTime * Time.deltaTime;
        }
        if (CollidingTopYAxis(playerController)) { v.y = 0; }
        if (CollidingBottomZAxis(playerController))
        {
            isMoveDown = false;
            v.z = 0;
            playerController.transform.position = new Vector3(playerController.transform.position.x, playerController.transform.position.y, playerController.transform.position.z + collisionCorrection);
        }
        else{isMoveDown = true;}
        if (CollidingTopZAxis(playerController))
        {
            isMoveUp = false;
            v.z = 0;
            playerController.transform.position = new Vector3(playerController.transform.position.x, playerController.transform.position.y, playerController.transform.position.z - collisionCorrection);
        }
        else{isMoveUp = true;}

        // Left and right 
        if (CollidingLeft(playerController))
        {
            isMoveLeft = false;
            v.x = 0;
            playerController.transform.position = new Vector3(playerController.transform.position.x + collisionCorrection, playerController.transform.position.y, playerController.transform.position.z);
        }
        else{isMoveLeft = true;}
        if (CollidingRight(playerController))
        {
            isMoveRight = false;
            v.x = 0;
            playerController.transform.position = new Vector3(playerController.transform.position.x - collisionCorrection, playerController.transform.position.y, playerController.transform.position.z);
        }
        else{isMoveRight = true;}

    }

    private void SetFluid()
    {
        switch (typeOfFluid)
        {
            case TypeOfFluid.SeaWater:
                densityOfFluid = FluidDensity.seaWater;
                break;
            case TypeOfFluid.Gaslone:
                densityOfFluid = FluidDensity.gasoline;
                break;
            case TypeOfFluid.Bromine:
                densityOfFluid = FluidDensity.bromine;
                break;
            default:
                break;
        }
    }

    private void SetMaterial()
    {
        switch (typeOfMaterial)
        {
            case TypeOfMaterial.Timber:
                densityOfMaterial = MaterialDensity.timber;
                break;
            case TypeOfMaterial.Gold:
                densityOfMaterial = MaterialDensity.rock;
                break;
            case TypeOfMaterial.Walnut:
                densityOfMaterial = MaterialDensity.walnut;
                break;
            default:
                break;
        }
    }


}
