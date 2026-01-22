using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;


    void Update()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            x = -1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            x = 1f;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            y = 1f;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            y = -1f;
        }
            
        Vector3 dir = new Vector3(x, y, 0f);

        transform.position += dir * speed * Time.deltaTime;

    }

    public void PlayerDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
