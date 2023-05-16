using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ChatType { Normal = 0 , Party, Gild, Whisper, System, Count }

public class ChatController : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    GameObject textChatPrefab; // ��ȭ ��¿� Text ������

    [SerializeField]
    Transform parentContent; // ��ũ�Ѻ��� content

    string ID = "yikim"; // �ӽ�

    [SerializeField]
    Image chatTypeChangeBtn; // ��ư�� �̹���

    [SerializeField]
    Sprite[] chatTypeBtnSprites; // �ٲ� �̹��� �迭

    List<ChatCell> chatList;
    ChatType currentChatView;

    ChatType currentChatType;
    Color currentChatColor;

    private void Awake()
    {
        chatList = new List<ChatCell>();
        currentChatView = ChatType.Normal;
        currentChatType = ChatType.Normal;
        currentChatColor = Color.black;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && inputField.isFocused == false)
        {
            inputField.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && inputField.isFocused == true)
        {
            ChangeChatType();
        }
    }

    public void OnEditEndEventMethod()
    {
        if (Input.GetKeyDown(KeyCode.Return) )
        {
            UpdateChat();
        }
    }

    public void UpdateChat()
    {
        if (inputField.text.Equals("")) return;

        GameObject clone = Instantiate(textChatPrefab, parentContent);

        clone.GetComponent<ChatCell>().Setup(currentChatType, $"{ID} : {inputField.text}", currentChatColor);
        chatList.Add(clone.GetComponent<ChatCell>());
        inputField.text = "";
    }

    public void ChangeChatType()
    {
        currentChatType = currentChatType + 1;
        if( currentChatType > ChatType.Gild)
        {
            currentChatType = ChatType.Normal;
        }

        currentChatColor = GetColorByType(currentChatType);
        chatTypeChangeBtn.sprite = chatTypeBtnSprites[(int)currentChatType];
    }

    Color GetColorByType(ChatType type)
    {
        switch (type)
        {
            case ChatType.Normal:
                return Color.black;
            case ChatType.Party:
                return Color.blue;
            case ChatType.Gild:
                return Color.green;
            case ChatType.Whisper:
                return Color.magenta;
            case ChatType.System:
                return Color.yellow;
            default:
                return Color.black;
        }
    }

    public void ChangeChatView(int newType) // enum�� ��Ŭ���� �Ű������� �� �� ����.
    {
        currentChatView = (ChatType) newType;

        if (currentChatView == ChatType.Normal)
        {
            foreach(var chat in chatList)
            {
                chat.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var chat in chatList)
            {
                chat.gameObject.SetActive(chat.chatType == currentChatView);                
            }
        }
    }
}
