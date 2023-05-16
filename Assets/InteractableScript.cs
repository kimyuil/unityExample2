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
        // 드랍다운 옵션 동적으로 설정하기
        dropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData>();

        foreach(string o in options){
            optionList.Add(new TMP_Dropdown.OptionData(o));
        }

        dropdown.AddOptions(optionList);

        dropdown.value = 0;

        // 인풋필드 이벤트메소드 동적으로 설정해보기

        inputField.onValueChanged.AddListener(inputValueChanged);
        inputField.onEndEdit.AddListener(inputValueEnd);
        inputField.onSelect.AddListener(inputValueSelect);
        inputField.onDeselect.AddListener(inputValueDeselect);

        // 글자 밸리데이션 동적으로~!
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
        Debug.Log("토글버튼 체크 - " + isOn);
    }

    public void scrollChanged(Vector2 vector2)
    {
        Debug.Log("스크롤링 - " + vector2);
    }

    public void optionChanged(int index)
    {
        Debug.Log("선택된 인덱스 - " + index);
    }

    void inputValueChanged(string text)
    {
        Debug.Log("인풋필드 값변경 - " + text);
    }

    void inputValueEnd(string text)
    {
        Debug.Log("인풋필드 입력완료 - " + text);
    }

    void inputValueSelect(string text)
    {
        Debug.Log("인풋필드 선택 - " + text);
    }

    void inputValueDeselect(string text)
    {
        Debug.Log("인풋필드 선택해제 - " + text);
    }
}
