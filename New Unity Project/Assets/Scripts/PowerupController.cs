using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
       
    public List<Powerup> powerups;
    public TankData data;

    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Powerup>();
        data = gameObject.GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        // create a list for expired powerups
        List<Powerup> expiredPowerups = new List<Powerup>();

        // Loop through all powers in that list
        foreach (Powerup power in powerups)
        {
            power.duration -= Time.deltaTime;

            // Assemble a list of expired powerups
            if (power.duration <= 0)
            {
                expiredPowerups.Add(power);
            }
        }
        // Remove expierd powerups
        foreach (Powerup power in expiredPowerups)
        {
            power.OnDeactivate(data);
            powerups.Remove(power);
        }
        // clear the list
        expiredPowerups.Clear();
    }

    public void Add(Powerup powerup)
    {
        powerup.OnActivate(data);
        if (!powerup.isPermanent)
        {
            powerups.Add(powerup);
        }
    }
}