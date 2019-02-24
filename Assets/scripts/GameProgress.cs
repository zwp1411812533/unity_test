using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏进度
public class GameProgress : MonoBehaviour {

    public static bool isStartGame;//是否开始游戏
    public static bool isBattingStage;//是否击球阶段
    public static bool isGoal;//是否进球
    public static bool isFoul;//是否犯规
    public static bool isGameSuspend;//是否暂停

    void Start () {
        GameProgress.isStartGame = false;
        GameProgress.isBattingStage = false;
        GameProgress.isGoal = false;
        GameProgress.isFoul = false;
        GameProgress.isGameSuspend = false;

    }
	
	void Update () {
		
	}
}
