using RoyalMatch.Board;
using UnityEngine;
/*
 * 유니티에서 제공하는 JsonUtility를 사용하여 JSON 파일을 읽어서 Object로 변환한다.
 * 변환되는 Object는 JSON to Object 변환이 적용되도록 Serializable한 객체로 선언되어야 한다.
 * 
 */
namespace RoyalMatch.Stage
{
    [System.Serializable]
    public class StageInfo
    {
        public int row;
        public int col;

        public int[] cells;

        //디버깅 용도. StageInfo의 멤버를 다시 JSON으로 변환해서 결과를 확인하는데 사용한다.
        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }


        //요청한 위치에 CellType을 리턴하는 메소드
        public CellType GetCellType(int nRow, int nCol)
        {
            //요청한 위치가 유효한지 확인
            Debug.Assert(cells != null && cells.Length > nRow *  col +nCol);

            //배열에 저장된값 1,0 에 따라서 CellType 반환
            if (cells.Length > nRow * col + nCol)
                return (CellType)cells[nRow * col + nCol];

            Debug.Assert(false);

            return CellType.EMPTY;
        }

        //JSON 데이터 유효성 검사를 수행하는 메소드
        public bool DoValidation()
        {
            Debug.Assert(cells.Length == row * col);
            Debug.Log($"cell length : {cells.Length}, row, col = ({row},{col})");

            //블럭 크기와 배열 크기가 다른경우 return 한다.
            if (cells.Length != row * col)
                return false;

            return true;
        }

    }
}

