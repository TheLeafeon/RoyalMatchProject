using UnityEngine;

namespace RoyalMatch.Stage
{
    public static class StageReader
    {
        //StageInfo 객체를 리턴하는 LoadStage 함수 , nStage: 로드할 스테이지 번호
        public static StageInfo LoadStage(int nStage)
        {
            Debug.Log($"Load Stage : Stage/{ GetFileName(nStage) }" );

            //유니티 리소스 파일을 읽어서, 스테이지 데이터를 텍스트로 담고 있는 텍스트 에셋 생성
            TextAsset textAsset = Resources.Load<TextAsset>($"Stage/{GetFileName(nStage)}");

            if (textAsset != null)
            {
                //JsonUtility.FromJson()을 사용해서 읽어들인 스테이지 Json데이터를 Serialize한 Stage 객체로 생성한다.
                StageInfo stageInfo = JsonUtility.FromJson<StageInfo>(textAsset.text);

                Debug.Assert(stageInfo.DoValidation());

                return stageInfo;
            }

            return null;
        }


        //읽어들일 스테이지 리소스 이름을 구한다. stage_숫자 4자리로 구성된 파일 이름을 리턴한다.
        static string GetFileName(int nStage)
        {
            return string.Format("stage_{0:D4}", nStage);
        }
    }
}

