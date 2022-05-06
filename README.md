# Shader-Breakout
This project is meant to demonstrate some shader usage. The game is Breakout, though there are currently no balls to destroy. Instead of using typical visual components to represent the ball and paddle, a shader is used. The ball and paddle pass their respective positions and size to the shader to render.
To start gameplay, press space. Game resets if ball moves below paddle, press space to start again. 'A' and 'D' keys to move paddle left and right respectively.

- Shader in Assets/Shaders/SurfaceShader.shader
- Gameplay scripts under Assets/Scripts/Gameplay
- Audio system also present to play sounds on collision
