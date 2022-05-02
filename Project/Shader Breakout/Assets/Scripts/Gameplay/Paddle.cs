using UnityEngine;

public class Paddle
{
    public Paddle(ref Ball ball, Vector2 position, float leftWallPos, float rightWallPos)
    {
        m_ball = ball;
        m_initialPosition = m_position = position;
        m_paddleSpeed = 0.5f;
        m_paddleHalfWidth = 0.1f;
        m_leftWallPos = leftWallPos + m_paddleHalfWidth;
        m_rightWallPos = rightWallPos - m_paddleHalfWidth;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !m_bisGameRunning)
        {
            StartGame();
        }

        if (m_ball.OutOfBounds)
        {
            StopGame();
        }

        if (m_bisGameRunning)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (m_position.x > m_leftWallPos)
                {
                    m_position -= new Vector2(1, 0) * m_paddleSpeed * Time.deltaTime;
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (m_position.x < m_rightWallPos)
                {
                    m_position += new Vector2(1, 0) * m_paddleSpeed * Time.deltaTime;
                }
            }

            Vector2 pointOfCollision;
            //Collision Check
            if (CheckCollision(m_ball, out pointOfCollision))
            {
                //Collision Response
                Vector2 paddleNormal = new Vector2(0, -1);
                m_ball.SetPosition(new Vector2(pointOfCollision.x, pointOfCollision.y + m_ball.GetRadius()));
                m_ball.SetVelocity(-2.0f * Vector2.Dot(m_ball.GetVelocity(), paddleNormal) * paddleNormal + m_ball.GetVelocity());

                m_soundController.Play();
            }
        }
    }

    bool CheckCollision(Ball ball, out Vector2 pointOfCollision)
    {
        //Ensure the ball is moving
        if (ball.GetVelocity() != Vector2.zero)
        {
            //Calculate the closest point of the line
            Vector2 lineStart = new Vector2(m_position.x - m_paddleHalfWidth, m_position.y);
            Vector2 lineEnd = new Vector2(m_position.x + m_paddleHalfWidth, m_position.y);
            Vector2 closestPoint = CalculateClosestPoint(m_ball.GetPosition(), m_ball.GetRadius(), lineStart, lineEnd);
            pointOfCollision = closestPoint;
            //Calculate the distance between the closest point and the center of the ball
            float distance = Vector2.Distance(ball.GetPosition(), closestPoint);
            float radius = ball.GetRadius();

            //If the distance squared is less than the radii squared, then there's a collision
            bool didCollide = distance < radius;
            return didCollide;
        }
        pointOfCollision = Vector2.zero;
        return false;
    }

    Vector2 CalculateClosestPoint(Vector2 aCircleCenter, float aRadius, Vector2 aLineStart, Vector2 aLineEnd)
    {
        //Calculate the circle vector        
        Vector2 circleVector = aCircleCenter - aLineStart;

       //Calculate the line segment vector        
       Vector2 lineVector = aLineEnd - aLineStart;

        //Normalize the line segment vector        
       Vector2 normalizedVector = lineVector.normalized;

        //Calculate the dot product between the circle vector and the normalized line segment vector       
       float magnitude = Vector2.Dot(normalizedVector, circleVector);

       //Calculate the projection using the result of the dot product and multiply it by the normalized line segment        
       Vector2 projection = normalizedVector * magnitude;

       //Calculate the closest point on the line segment, by adding the project vector to the line start vector        
       Vector2 closestPoint = aLineStart + projection;
       closestPoint.x = Mathf.Clamp(closestPoint.x, aLineStart.x, aLineEnd.x);
       closestPoint.y = Mathf.Clamp(closestPoint.y, aLineStart.y, aLineEnd.y);
       return closestPoint;
    }

    public Vector2 GetPosition()
    {
        return m_position;
    }

    public void StartGame()
    {
        m_bisGameRunning = true;
        m_ball.StartGame();
    }

    public void StopGame()
    {
        m_bisGameRunning = false;
        m_position = m_initialPosition;
        m_ball.StopGame();
    }

    public void SetSound(SoundController sound)
    {
        m_soundController = sound;
    }

    public float GetHalfWidth()
    {
        return m_paddleHalfWidth;
    }

    Ball m_ball;
    Vector2 m_position;
    Vector2 m_initialPosition;
    float m_paddleSpeed;
    float m_leftWallPos;
    float m_rightWallPos;
    float m_paddleHalfWidth;

    bool m_bisGameRunning = false;

    SoundController m_soundController;
}
