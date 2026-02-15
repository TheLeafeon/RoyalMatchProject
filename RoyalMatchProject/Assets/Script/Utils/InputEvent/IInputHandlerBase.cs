using UnityEngine;

namespace RoyalMatch.Util
{
    /*
     * 마우스와 터치 이벤트 처리를 동일한 프로토타입으로 처리할 수 있도록,
     * InputHandler 인터페이스를 정의한다.
     * 3 매치 게임에 필요한 up down 이벤트만 처리하는 제한된 기능만 제공한다.
     */

    public interface IInputHandlerBase
    {
        bool isInputDown { get; }
        bool isInputUp { get; }
        Vector2 inputPosition { get; } //Screen(픽셀) 좌표
    }
}

