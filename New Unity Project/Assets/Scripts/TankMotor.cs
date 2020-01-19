using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TankData))]
public class TankMotor : MonoBehaviour
{
    public TankData data;
    public CharacterController CharacterController;
    public Transform tf;
    private static GameManager gameManager;
    public GameObject SoundEffectObject;

    void Start()
    {
        CharacterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void DestroyTank()
    {
        if (data.Lives <= 0)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
            {
                gameManager.DestroyRooms();
                SceneManager.LoadScene("GameOverScene");
            }
            Destroy(this.gameObject);
        }
        else
        {
            Instantiate(SoundEffectObject);
            data.Lives--;
            data.Health = data.MaxHealth;
            tf.position = gameManager.FindSpawnPoint().position;
            Debug.Log("tried to respawn");
        }
    }

    void OnDestroy()    // spawns object to play destroy sound
    {
        Instantiate(SoundEffectObject);
    }

    public void move(float speed)   // Moves tank forward
    {
        Vector3 MoveVector = tf.forward * speed * Time.deltaTime;
        CharacterController.SimpleMove(MoveVector); // Simple move
    }
    public void Rotate(float speed) // Rotate
    {
        tf.Rotate(Vector3.up * speed * Time.deltaTime);
    }
    public bool RotateTowards(Vector3 target, float rotateSpeed)
    {
        Vector3 vectorToTarget;

        vectorToTarget = target - tf.position;

        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);
        tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, data.rotateSpeed * Time.deltaTime);
        if (targetRotation == tf.rotation)
        {
            return false;
        }
        return true;
    }
}
