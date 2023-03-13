# AI
Open AI API Templates for Unity3D

Steps to reproduce:

Step 1) Create a new folder named "Editor" under "Assets" folder. Then put the scripts ChatGPTForUnityEditorDavinci.cs and/or ChatGPTForUnityEditorTurbo.cs there.

Step 2) In a Unity scene, drag and drop either davinci003.cs or gptTurbo.cs to a game object.

Step 3) Paste your own API Key.

Step 4) Write your prompt and hit the button for ask. 

Step 5) Wait for your answer! 

Shoutout to this video tutorial that served me as the template for my own implementation (gptTurbo.cs)
https://www.youtube.com/watch?v=RRGtluRf7ys&ab_channel=UnityAdventure

The API Documentation I used
https://platform.openai.com/docs/api-reference/completions/create

I used this website to calculate how much money a query would cost based on how many tokens a message has
https://platform.openai.com/tokenizer
