using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatCell : MonoBehaviour
{
    public ChatType chatType { get; set; }

    public void Setup(ChatType type, string content, Color color)
    {
        chatType = type;
        GetComponent<TextMeshProUGUI>().text = content;
        GetComponent<TextMeshProUGUI>().color = color;
    }

}
