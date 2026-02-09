using UnityEngine;

/*
 * Stage 객체 참조를 선언하고,
 * Start() 함수에서 초기화 메소드 InitStage()를 호출한다.
 * 
 */
namespace RoyalMatch.Stage
{
    public class StageController : MonoBehaviour
    {
        bool m_bInit;
        Stage m_Stage;

        [SerializeField] Transform m_Container; //Cell GameObj가 씬에 추가될 때 부모 역할을 담당할 게임오브젝트
        [SerializeField] GameObject m_CellPrefab; // Cell Prefab
        [SerializeField] GameObject m_BlockPrefab; //Block Prefab

        private void Start()
        {
            InitStage();

        }

        void InitStage()
        {
            if (m_bInit)
                return;

            m_bInit = true;

            //초기화 과정에서 BuildStage() 호출
            BuildStage();

            //디버깅 코드
            m_Stage.PrintAll();
        }

        //Stage를 생성/구성하는 역할을 하는 StageBuilder.BuildStage 으로 스테이지 구성
        void BuildStage()
        {
            //스테이지 구성
            m_Stage = StageBuilder.BuildStage(nStage: 0, row: 9, col: 9);

            //생성한 스테이지 정보를 이용하여 씬 구성
            //Stage 객체에 주어진 리소스를 이용하여 스테이지 구성 요청
            m_Stage.ComposeStage(m_CellPrefab, m_BlockPrefab, m_Container);
        }
    }
}


