using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    // Use this for initialization
    void Start ()
    {
        // Construct Ball and Paddle classes
        m_ball = new Ball(-0.625f, 0.425f, 0.425f, -0.425f);
        m_paddle = new Paddle(ref m_ball, new Vector2(-0.1f, -0.25f), -0.6162f, 0.4155f);

        if (sound)
        {
            m_ball.SetSound(sound);
            m_paddle.SetSound(sound);
        }

        renderer = GameObject.Find("Tv").GetComponent<Renderer>();
        renderer.material.shader = Shader.Find("SurfaceShader");
        renderer.material.SetFloat("_BallRadius", m_ball.GetRadius());
        renderer.material.SetFloat("_PaddleWidth", m_paddle.GetHalfWidth());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (renderer)
        {
            renderer.material.SetVector("_BallPosition", m_ball.GetPosition());
            renderer.material.SetVector("_PaddlePosition", m_paddle.GetPosition());
        }

        m_ball.Update();
        m_paddle.Update();
    }

    Ball m_ball;
    Paddle m_paddle;
    Renderer renderer;

    public SoundController sound;
}