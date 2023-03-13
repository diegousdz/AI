using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections.Generic;

using UnityEngine.Networking;
using System;
using System.Text;
using System.IO;

public class gptTurbo : MonoBehaviour
{
    [SerializeField] private string APIKey;
    [TextArea(3,10)]
    [SerializeField] private string prompt;
    [TextArea(3,40)]
    [SerializeField] private string result;

    private readonly string chatGPTUrlAPI = "https://api.openai.com/v1/chat/completions";
    private readonly string scriptsFolder = "Asset/Scripts";
    private readonly string directory = "ChatGPTTurbo";

    RequestBodyChatGPT2 requestBodyChatGPT2;
    ResponseBodyChatGPT2 responseBodyChatGPT2;

    public void SendRequest(){
        result = string.Empty;

        requestBodyChatGPT2 = new RequestBodyChatGPT2();
        requestBodyChatGPT2.model = "gpt-3.5-turbo";

        requestBodyChatGPT2.messages = new List<Message>();
        Message userMessage = new Message();
        userMessage.role = "user";
        userMessage.content = prompt;

        requestBodyChatGPT2.messages.Add(userMessage);

        StartCoroutine(SendRequestAPI());
    }
    
    private IEnumerator SendRequestAPI()
    {
        string jsonData = JsonUtility.ToJson(requestBodyChatGPT2);
        byte[] rawData = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest webRequest = new UnityWebRequest(chatGPTUrlAPI, "POST");

        webRequest.uploadHandler = new UploadHandlerRaw(rawData);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        webRequest.SetRequestHeader("Content-Type", "application/json");

        webRequest.SetRequestHeader("Authorization", "Bearer " + APIKey);

        result = "Loading...";

        yield return webRequest.SendWebRequest();

        if(webRequest.result == UnityWebRequest.Result.Success)
        {
            responseBodyChatGPT2 = JsonUtility.FromJson<ResponseBodyChatGPT2>(webRequest.downloadHandler.text);
          result = responseBodyChatGPT2.choices[0].message.content;
        } else {
            result = "Error " + webRequest.result;
        }

        webRequest.Dispose();
    }

    public void Clear()
    {
        prompt = string.Empty;
        result = string.Empty;
    }
    
    public void SaveScript()
    {
        if(!Directory.Exists(scriptsFolder + "/" + directory)){
            Directory.CreateDirectory(scriptsFolder + "/" + directory);
        }

        string className = ParseClassName(result);
        string scriptPath = scriptsFolder + "/" + directory + "/" + className + ".cs";
        using FileStream fs = new FileStream(scriptPath, FileMode.Create);
        using StreamWriter write = new StreamWriter(fs);

        write.Write(result);
    }

    public string ParseClassName(string result)
    {
        int indexClass = result.IndexOf("class");
        int indexDots = result.IndexOf(":");
        string className = result.Substring(indexClass + 6, indexDots - indexClass - 6 - 1);
        return className;
    }    
}

[Serializable]
public class RequestBodyChatGPT2
{
    public string model;
    public List<Message> messages;
    
}

[Serializable]
public class Message
{
    public string role;
    public string content;
}


[Serializable]
public class ResponseBodyChatGPT2
{
    public string id ;
    public string @object ;
    public int created ;
    public List<Choice> choices ;
    public Usage usage ;

    [Serializable]
    public class Choice
    {
        public int index ;
        public Message message ;
        public string finish_reason ;
    }

    [Serializable]
    public class Usage
    {
        public int prompt_tokens ;
        public int completion_tokens ;
        public int total_tokens ;
    }

    [Serializable]
    public class Message
    {
        public string role ;
        public string content ;
    }
}


