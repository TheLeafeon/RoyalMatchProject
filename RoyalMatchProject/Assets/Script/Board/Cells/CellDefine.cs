namespace Ninez.Board
{
    public enum CellType
    {
        EMPTY = 0, // 빈공간, 블럭이 위치할 수 없음
        BASIC = 1, //배경있는 기본형
        FIXTURE =2, //고정된 장애물
        JELLY =3,  // 젤리, 블록이동 ok  블럭 Clear되면 BASIC 출력
    }
}