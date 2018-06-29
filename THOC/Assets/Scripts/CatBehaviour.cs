using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatBehaviour : MonoBehaviour {

    private float mapEdgex = 15;
    private float mapEdgez = 15;
    private float m_WanderRadius = 2;
    private Rigidbody c_Rigidbody = null;
    private bool isFlee = false;
    private float timeUntilFleeEnd = 5;
    private Vector3 wanderAnchor = Vector3.zero;
    private float wanderRadius = 2;
    public float wanderMovementSpeed = 1;
    public float fleeMovementSpeed = 2;
    public float playerSearchRadius = 4;
    private GameObject[] playerList;
    private GameObject[] nearbyPlayers = null;
    private float timerRandom = 0;
    public float randomGenerationTime = 10;
    private Vector3 randomMove = Vector3.zero;
    private float timerFlee = 0;

    // Use this for initialization
    void Start()
    {
        c_Rigidbody = GetComponent<Rigidbody>();
        wanderAnchor = transform.position;
        playerList = GameObject.FindGameObjectsWithTag("Player");
        MakeRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlee == false)
        {
            NotFleeBehaviour();
        }
        else
        {
            FleeBehaviour();
        }

    }

    private void NotFleeBehaviour()
    {
        if (DetectPlayers() == true)
        {
            isFlee = true;
        }
        if (ReachedLocation() == true) MakeRandomDestination();
        Vector3 desiredVelocity = (randomMove - transform.position) * wanderMovementSpeed;
        ApplyMovementVector(desiredVelocity);
        UpdateTimerRandom();
    }

    private void FleeBehaviour()
    {
        if (nearbyPlayers.Length != 0)
        {
            List<Vector3> listVectors = new List<Vector3>();
            foreach (GameObject i in nearbyPlayers)
            {
                listVectors.Add(i.transform.position);
            }
            Vector3 averageVector = new Vector3(
                                        listVectors.Average(x => x.x),
                                        listVectors.Average(x => x.y),
                                        listVectors.Average(x => x.z));

            ApplyMovementVector(-averageVector * fleeMovementSpeed);
        }
        UpdateTimerFlee();
    }

    private bool DetectPlayers()
    {
        nearbyPlayers = GameObject.FindGameObjectsWithTag("Player")
            .Where(t => Vector3.Distance(t.transform.position, transform.position) < playerSearchRadius)
            .ToArray();
        if (nearbyPlayers.Length == 0)
        {
            return false;
        } else
        {
            return true;
        }
    }

    private bool ReachedLocation()
    {
        Vector3 currentLoc = Vector3.zero;
        Vector3 goalLoc = Vector3.zero;

        currentLoc.x = Mathf.Round(currentLoc.x);
        currentLoc.y = 0;
        currentLoc.z = Mathf.Round(currentLoc.z);

        goalLoc.x = Mathf.Round(goalLoc.x);
        goalLoc.y = 0;
        goalLoc.z = Mathf.Round(goalLoc.z);

        if (currentLoc == goalLoc) return true;

        return false;
    }
    private void ApplyMovementVector(Vector3 movementForce)
    {
        // adjust movement
        c_Rigidbody.velocity += movementForce;
        
        // turn head of cat
        if (c_Rigidbody.velocity.sqrMagnitude > 1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(c_Rigidbody.velocity), Time.deltaTime * 5);
        }
    }

    private void UpdateTimerRandom()
    {
        timerRandom += Time.deltaTime;

        if (timerRandom > randomGenerationTime)
        {
            MakeRandomDestination();
            timerRandom = 0;
        }
    }
    private void MakeRandomDestination()
    {
        randomMove = Random.insideUnitCircle * wanderRadius;
        randomMove.z = randomMove.y;
        if (randomMove.z > mapEdgez) randomMove.z = mapEdgez;
        if (randomMove.z < -mapEdgez) randomMove.z = -mapEdgez;
        if (randomMove.x > mapEdgex) randomMove.x = mapEdgex;
        if (randomMove.x < -mapEdgex) randomMove.x = -mapEdgex;
        randomMove.y = 0;
        randomMove += wanderAnchor;
    }

    private void UpdateTimerFlee()
    {
        if (DetectPlayers() == true)
        {
            timerFlee = 0;
        }
        else
        {
            timerFlee += Time.deltaTime;
        }

        if (timerFlee >= timeUntilFleeEnd)
        {
            isFlee = false;
            c_Rigidbody.velocity = Vector3.zero;
            wanderAnchor = transform.position;
            MakeRandomDestination();
        }
    }
}
