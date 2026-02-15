using UnityEngine;


namespace RoyalMatch.Util
{
    //마우스 이벤트 처리를 위해 IInputHandlerBase를 구현한 클래스
    public class MouseHandler : IInputHandlerBase
    {
        //왼쪽버튼이 down이면 true 리턴
        bool IInputHandlerBase.isInputDown => Input.GetButtonDown("Fire1");
        //왼쪽버튼 up이면 true 리턴
        bool IInputHandlerBase.isInputUp => Input.GetButtonUp("Fire1");

        //마우스 좌표 return
        Vector2 IInputHandlerBase.inputPosition => Input.mousePosition;

    }

}
