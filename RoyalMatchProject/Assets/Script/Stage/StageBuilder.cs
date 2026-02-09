using RoyalMatch.Board;

//Stage 객체를 생성하고, Board를 구성하고 있는 Cell과 Block  게임오브젝트를 생성해서 Board 게임오브젝트에 제공하는 역할
namespace RoyalMatch.Stage
{
    public class StageBuilder
    {
        //플레이 중인 스테이지 번호 저장하는 멤버변수
        int m_nStage;

        //생성자, 플레이하는 스테이지 번호 저장
        public StageBuilder(int nStage)
        {
            m_nStage = nStage;
        }

        /*
         * 입력받은 크기 row, col 의 Board를 가지는 Stage 객체를 생성
         * Board를 구성하는 Cell, Block 객체를 생성한다.
         */
        public Stage ComposeStage(int row, int col)
        {
            //스테이지 객체 생성
            Stage stage = new Stage(this, row, col);

            //9x9 보드 라면 81개의 Cell과 Block 객체 생성
            for (int nRow = 0; nRow < row; nRow++)
            {
                for (int nCol = 0; nCol < col; nCol++)
                {
                    stage.blocks[nRow, nCol] = SpawnBlockForStage(nRow, nCol);
                    stage.cells[nRow, nCol] = SpawnCellForStage(nRow, nCol);

                }
            }
            return stage;
        }

        //지정된 위치에 Block 객체 생성 후 리턴 초기값은 BASIC
        Block SpawnBlockForStage(int nRow, int nCol)
        {
            //직접생성하지 않고 블럭을 생성하는 함수 호출
            return nRow == nCol ? SpawnEmptyBlock() : SpawnBlock();
        }
        //지정된 위치에 Cell 객체 생성 후 리턴 초기값은 BASIC
        Cell SpawnCellForStage(int nRow, int nCol)
        {
            return new Cell(nRow == nCol ? CellType.EMPTY : CellType.BASIC);
            
        }

        /*
         * static 메소드
         * StageBuilder객체 생성, Stage를 구성하는 Cell,Block을 생성하는 ComposeStage 함수를 호출해서
         * Stage객체를 생성한다.
         * StageController 초기화 코드에서 스테이지를 구성하기 위해 호출
         * 
         */
        public static Stage BuildStage(int nStage, int row, int col)
        {
            StageBuilder stageBuilder = new StageBuilder(0);
            Stage stage = stageBuilder.ComposeStage(row, col);

            return stage;
        }

        //기본 블럭 생성을 요청하는 메소드. BlockFactory는 아래에서 설명한다.
        public Block SpawnBlock()
        {
            return BlockFactory.SpawnBlock(BlockType.BASIC);
        }
        //빈 블럭 생성을 요청한 메소드.
        public Block SpawnEmptyBlock()
        {
            Block newBlock = BlockFactory.SpawnBlock(BlockType.EMPTY);

            return newBlock;
        }
    }
}


/*
 * 1. static 메소드 BuildStage 호출, Stage 객체를 리턴  
 * 2. StageBuilder 객체 인스턴스를 생성   new StageBuilder
 * 3. Stage 정보를 구성하는 ComposeStage 함수 호출
 * 4. Stage 객체 인스턴스 생성
 * 5. Stage에서 Board 객체를 생성, Board를 구성하는 Cell과 Block을 저장할 수 있는 배열을 각각 생성
 * 6. 보드를 구성하는 모든 행과 열에 대해서 Block 객체를 요청
 * 7. Block 객체 생성
 * 8. 보드를 구성하는 모든 행과 열에 대해서 Cell 객체를 요청
 * 9. Cell 객체 생성
 * 
 * StageBuilder에게 Stage구성을 요청하면, Stage객체를 생성한 후, Stage를 구성하는 Board를 구성하는 흐름이다.
 */