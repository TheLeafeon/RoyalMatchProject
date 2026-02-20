using RoyalMatch.Board;
using UnityEngine;
using RoyalMatch.Util;
using RoyalMatch.Core;
using System.Collections;

namespace RoyalMatch.Stage
{
    public class Stage
    {
        public int maxRow { get { return m_Board.maxRow; } }
        public int maxCol { get { return m_Board.maxCol; } }

        RoyalMatch.Board.Board m_Board;
        public RoyalMatch.Board.Board board { get { return m_Board; } }

        StageBuilder m_StageBuilder;

        public Block[,] blocks { get { return m_Board.blocks; } }
        public Cell[,] cells { get { return m_Board.cells; } }

        /// <summary>
        /// 생성자.
        /// 주어진 크기를 갖는 Board를 생성한다.
        /// </summary>
        /// <param name="stageBuilder"></param>
        /// <param name="nRow"></param>
        /// <param name="nCol"></param>
        public Stage(StageBuilder stageBuilder, int nRow, int nCol)
        {
            m_StageBuilder = stageBuilder;

            m_Board = new RoyalMatch.Board.Board(nRow, nCol);
        }

        /// 주어진 정보(Cell/Block Prefab, 컨테이너)를 이용해서 Board를 구성한다.
        /// <param name="cellPrefab">Cell Prefab</param>
        /// <param name="blockPrefab">Board Prefab</param>
        /// <param name="container">Cell/Board GameObject의 부모 GameObject</param>
        internal void ComposeStage(GameObject cellPrefab, GameObject blockPrefab, Transform container)
        {
            m_Board.ComposeStage(cellPrefab, blockPrefab, container);
        }

        #region Simple Methods
        //----------------------------------------------------------------------
        // 조회(get/set/is) 메소드
        //----------------------------------------------------------------------

        /*
         * 보드안에서 발생한 이벤트인지 체크한다       
         */
        public bool IsInsideBoard(Vector2 ptOrg)
        {
            // 계산의 편의를 위해서 (0, 0)을 기준으로 좌표를 이동한다. 
            // 8 x 8 보드인 경우: x(-4 ~ +4), y(-4 ~ +4) -> x(0 ~ +8), y(0 ~ +8) 
            Vector2 point = new Vector2(ptOrg.x + (maxCol / 2.0f), ptOrg.y + (maxRow / 2.0f));

            if (point.y < 0 || point.x < 0 || point.y > maxRow || point.x > maxCol)
                return false;

            return true;
        }

        /*
         * 유효한 블럭(이동가능한 블럭) 위에서 있는지 체크한다.
         * @param point Wordl 좌표, 컨테이너 기준
         * @param blockPos out 파라미터, 보드에 저장된 블럭의 인덱스
         * 
         * @return 스와이프 가능하면 true
         */
        public bool IsOnValideBlock(Vector2 point, out BlockPos blockPos)
        {
            //1. World 좌표 -> 보드의 블럭 인덱스로 변환한다.
            Vector2 pos = new Vector2(point.x + (maxCol / 2.0f), point.y + (maxRow / 2.0f));
            int nRow = (int)pos.y;
            int nCol = (int)pos.x;

            //리턴할 블럭 인덱스 생성
            blockPos = new BlockPos(nRow, nCol);

            //2. 스와이프 가능한지 체크한다.
            return board.IsSwipeable(nRow, nCol);
        }

        #endregion 

        public void PrintAll()
        {
            System.Text.StringBuilder strCells = new System.Text.StringBuilder();
            System.Text.StringBuilder strBlocks = new System.Text.StringBuilder();

            for (int nRow = maxRow - 1; nRow >= 0; nRow--)
            {
                for (int nCol = 0; nCol < maxCol; nCol++)
                {
                    strCells.Append($"{cells[nRow, nCol].type}, ");
                    strBlocks.Append($"{blocks[nRow, nCol].breed}, ");
                }

                strCells.Append("\n");
                strBlocks.Append("\n");
            }

            Debug.Log(strCells.ToString());
            Debug.Log(strBlocks.ToString());
        }


        //스와이프 대상은 블럭 -> 블럭의 정보는 보드가 관리하고 있음 -> 보드에 대한 명령은 Stage가 총괄
        // 그러므로 스와이프 액션은 Stage에서 요청할 것
        public IEnumerator CoDoSwipeAction(int nRow, int nCol, Swipe swipeDir, Returnable<bool> actionResult)
        {
            //코루틴 실행 결과를 전달하는 객체에 초기값으로 false를 설정
            actionResult.value = false;


            int nSwipeRow = nRow, nSwipeCol = nCol;
            nSwipeRow += swipeDir.GetTargetRow();
            nSwipeCol += swipeDir.GetTargetCol();

            Debug.Assert(nRow != nSwipeRow || nCol != nSwipeCol, "Invalid Swipe : ({nSwipeRow} , {nSwipeCol})");
            Debug.Assert(nSwipeRow >= 0 && nSwipeCol < maxRow && nSwipeCol >= 0 && nSwipeCol < maxCol, $"Swipe 타겟 블럭 인덱스 오류 = ({nSwipeRow},{nSwipeCol})");

            //스와이프 대상이 되는 두개의 블럭 정보를 구해서 스와이프 액션을 실행한다.
            if (m_Board.IsSwipeable(nSwipeRow,nSwipeCol))
            {
                //스와이프 대상 Block 객체와 위치 정보를 구하기
                Block targetBlock = blocks[nSwipeRow, nSwipeCol];
                Block baseBlock = blocks[nRow, nCol];

                Debug.Assert(baseBlock != null && targetBlock != null);

                Vector3 basePos = baseBlock.blockObj.transform.position;
                Vector3 targetPos = targetBlock.blockObj.transform.position;

                if(targetBlock.IsSwipeable(baseBlock))
                {
                    //블럭에게 지정된 위치로 이동하도록 요청
                    //프레임마다 블럭이 이동하는 모습을 볼 수 있다.
                    //이동을 요청받은 블럭은 코루틴을 생성해서 프레임마다 블럭의 위치를 변경할 것이다.
                    baseBlock.MoveTo(targetPos, Constants.SWIPE_DURATION);
                    targetBlock.MoveTo(basePos, Constants.SWIPE_DURATION);

                    //스와이프 액션이 실행되는 동안 대기한다.
                    yield return new WaitForSeconds(Constants.SWIPE_DURATION);

                    //스와이프 액션이 종료되면 보드에 저장된 블럭의 위치를 서로 바꾼다.
                    //최종적으로 스와이프 대상 2개의 블럭 위치가 변경된다.
                    blocks[nRow, nCol] = targetBlock;
                    blocks[nSwipeRow, nSwipeCol] = baseBlock;

                    //액션 수행 결과로 true를 설정한다.
                    actionResult.value = true;
                }
            }
            yield break;
        }

        //주어진 위치가 스와이프 액션이 유효한지 체크하는 메소드
        public bool IsValideSwipe(int nRow, int nCol, Swipe swipeDir)
        {
            switch(swipeDir)
            {
                case Swipe.DOWN: return nRow > 0; 
                case Swipe.UP: return nRow < maxRow - 1; 
                case Swipe.LEFT: return nCol > 0; 
                case Swipe.RIGHT:return nCol < maxCol - 1;
                default: return false;
            }
        }
    }
}