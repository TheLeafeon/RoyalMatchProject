
namespace RoyalMatch.Board
{
    /*
     * Cell 구성에 필요한 부가 정보 정의 enum타입 선언 및 확장 메소드 정의
     * Cell의 종류를 식별하기 위해 enum타입 CellType을 선언
     * 
     */
    public enum CellType
    {
        EMPTY = 0, //빈공간, 블럭이 위치할 수 없음
        BASIC = 1, //배경있는 기본형
        FIXTURE = 2, //고정된 장애물
        JELLY = 3, //블럭 이동 o  블럭이 clear되면 BASIC으로 변화
    }
}
