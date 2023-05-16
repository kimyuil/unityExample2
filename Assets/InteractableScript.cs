using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableScript : MonoBehaviour
{

    [SerializeField]
    TMP_Dropdown dropdown;

    [SerializeField]
    List<string> options;

    [SerializeField]
    TMP_InputField inputField;

    private void Awake()
    {
        // ����ٿ� �ɼ� �������� �����ϱ�
        dropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

        foreach(string o in options){
            optionList.Add(new TMP_Dropdown.OptionData(o));
        }

        dropdown.AddOptions(optionList);

        dropdown.value = 0;

        // ��ǲ�ʵ� �̺�Ʈ�޼ҵ� �������� �����غ���

        inputField.onValueChanged.AddListener(inputValueChanged);
        inputField.onEndEdit.AddListener(inputValueEnd);
        inputField.onSelect.AddListener(inputValueSelect);
        inputField.onDeselect.AddListener(inputValueDeselect);

        // ���� �븮���̼� ��������~!
        inputField.onValidateInput = (string text, int charIndex, char addedChar) =>
        {
            char toUpper = addedChar;
            if (addedChar >= 'a' && addedChar <= 'z')
                toUpper = (char)(addedChar + ('A' - 'a'));
            return toUpper;
        };

    }

    public void toggleChanged(bool isOn)
    {
        Debug.Log("��۹�ư üũ - " + isOn);
    }

    public void scrollChanged(Vector2 vector2)
    {
        Debug.Log("��ũ�Ѹ� - " + vector2);
    }

    public void optionChanged(int index)
    {
        Debug.Log("���õ� �ε��� - " + index);
    }

    void inputValueChanged(string text)
    {
        Debug.Log("��ǲ�ʵ� ������ - " + text);
    }

    void inputValueEnd(string text)
    {
        Debug.Log("��ǲ�ʵ� �Է¿Ϸ� - " + text);
    }

    void inputValueSelect(string text)
    {
        Debug.Log("��ǲ�ʵ� ���� - " + text);
    }

    void inputValueDeselect(string text)
    {
        Debug.Log("��ǲ�ʵ� �������� - " + text);
    }
}
