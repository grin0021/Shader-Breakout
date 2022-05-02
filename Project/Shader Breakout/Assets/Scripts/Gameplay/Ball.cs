using UnityEngine;

public class Ball
{
    Renderer renderer;

    public Ball(float leftWallPos, float rightWallPos, float topWallPos, float bottomWallPos)
    {
        m_wallNormal = new Vector2(0, 0);
        m_position = new Vector2(-0.1f, -0.18f);
        m_speed = 0.4f;

        m_velocity = Vector2.zero;
       
        m_leftWallPos = leftWallPos + 0.04f;
        m_rightWallPos = rightWallPos - 0.04f;
        m_topWallPos = topWallPos - 0.04f;
        m_bottomWallPos = bottomWallPos + 0.04f;
        m_radius = 0.04f;
    }
    Vector2 GenerateRandomDirection()
    {
        float angle = Random.Range(20.0f, 160.0f);
        float radians = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    public void StartGame()
    {
        m_velocity = GenerateRandomDirection() * m_speed;
    }

    public void StopGame()
    {
        OutOfBounds = false;
        m_velocity = Vector2.zero;
        m_wallNormal = Vector2.zero;
        // Reset Ball Position
        m_position = new Vector2(-0.1f, -0.18f);
    }

    public void Update()
    {
        m_position += m_velocity * Time.deltaTime;

        ////Check For Collision
        if (m_position.x <= m_leftWallPos)
        {
            m_wallNormal = new Vector2(-1, 0);
            m_position.x = m_leftWallPos;

            m_soundController.Play();
        }
        else if (m_position.x >= m_rightWallPos)
        {
            m_wallNormal = new Vector2(1, 0);
            m_position.x = m_rightWallPos;

            m_soundController.Play();
        }

        if (m_position.y >= m_topWallPos - m_radius * 2.0f)
        {
            m_wallNormal = new Vector2(0, 1);
            m_position.y = m_topWallPos - m_radius * 2.0f;

            m_soundController.Play();
        }

        if (m_position.y <= m_bottomWallPos + m_radius * 2.0f)
        {
            OutOfBounds = true;
        }

        //Handle Collision
        if (m_wallNormal != Vector2.zero)
        {
           m_velocity = -2.0f * Vector2.Dot(m_velocity, m_wallNormal) * m_wallNormal + m_velocity;
           m_wallNormal = new Vector2(0, 0);
        }
    }
    public Vector2 GetPosition()
    {
        return m_position;
    }
    public void SetPosition(Vector2 position)
    {
        m_position = position;
    }

    public Vector2 GetVelocity()
    {
        return m_velocity;
    }

    public void SetVelocity(Vector2 velocity)
    {
        m_velocity = velocity;
    }

    public float GetRadius()
    {
        return m_radius;
    }

    public void SetSound(SoundController sound)
    {
        m_soundController = sound;
    }

    float m_rightWallPos;
    float m_leftWallPos;
    float m_topWallPos;
    float m_bottomWallPos;
    float m_speed;
    float m_radius;
    Vector2 m_position;
    Vector2 m_velocity;
    Vector2 m_wallNormal;

    public bool OutOfBounds = false;

    SoundController m_soundController;
}
