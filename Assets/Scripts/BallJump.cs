using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UnityEngine.SceneManagement;

public class BallJump : MonoBehaviour
{
    private Rigidbody rb;
    private bool _isOnGround;
    private Vector3 _velocity = Vector3.zero;
    private BallData loadedData;

    private class BallData
    {
        public List<float> x = new List<float>();
        public List<float> y = new List<float>();
        public List<float> z = new List<float>();
    }

    [SerializeField]
    [Range(0f, 1f)]
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        string json = File.ReadAllText(Application.dataPath + "/Resources/ball_path.json");
        loadedData = JsonUtility.FromJson<BallData>(json);
    }

    // Called at constant time interval
    void FixedUpdate()
    {
        Debug.Log(_isOnGround);
        if (Input.GetMouseButtonDown(0) && _isOnGround == true)
        {
            for (int i = 0; i < loadedData.x.Count; i++)
            {
                //Vector3 dest = new Vector3(x[i], y[i], z[i]);
                //transform.Translate(dest * Time.deltaTime * _speed); // transform.Translate ignores box colliders
                rb.AddForce(loadedData.x[i]*_speed, loadedData.y[i]*_speed, loadedData.z[i]*_speed);
            }
        }
        if (Input.GetMouseButtonDown(1) && _isOnGround == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }

    // Called on collision detection
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }

    // Called when exit collision
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isOnGround = false;
        }
    }
}