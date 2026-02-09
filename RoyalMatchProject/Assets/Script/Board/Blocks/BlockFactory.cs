using UnityEngine;

namespace RoyalMatch.Board
{
    /*
     *Block 객체 생성을 담당하는 클래스
     *요청에 따라서 Block 객체를 생성한 후 리턴
     * BlockFactory를 도입함으로써 Block을 제공받는 기능을 좀 더 유연하게
     * 요청에 따라서 Block을 새로 생성해서 전달할 수도 있고, 
     * 이후 오브젝트 풀(Object Pool)을 도입하는 경우 Object Pool에서 남아있는 Block을 전달할 수도 있을 것이다.
     * 
     * Block 객체를 어디에서 가져오는 요청하는 클래스의 코드는 변경되지 않을 것을 것이다.
    */

    //static 클래스 여러개 있을 필요 없음
    public static class BlockFactory
    {
        public static Block SpawnBlock(BlockType blockType)
        {
            //파라미터로 전달된 BlockType으로 Block 객체 생성
            Block block = new Block(blockType);

            //BlockType.BASIC 이라면 0~6 랜덤으로 생성
            if (blockType == BlockType.BASIC)
                block.breed = (BlockBreed)UnityEngine.Random.Range(0, 6);
            //BlockType.EMPTY 라면 NA로 설정
            else if (blockType == BlockType.EMPTY)
                block.breed = BlockBreed.NA;

            return block;
        }
    }
}

