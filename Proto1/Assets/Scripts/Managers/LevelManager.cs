﻿using GameAnalyticsSDK;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    //testing
    public LevelSettings levelSettings;

    [SerializeField]
    LevelSettings introLevel,
        level2RampEasy,
        level3JumpsEasy,
        LevelSettingLevel4TrampolineHard,
        level5BlackHoleTutorial,
        Level6BlackHoleBallAndJump,
        Level7BoosterEasy,
        Level8BoosterHard,
        Level9PortalEasy,
        Level10TrampolineAdvanced,
        Level11,
        LevelSettingLevel12LiftMoeilijk,
        Level13,
        Level14,
        Level15,
        Level16,
        smiletTest,
        house,
        multiLevel1,
        multiLevel2,
        multiLevel3,
        multiLevel4,
        multiLevel5,
        multiLevel6,
        multiLevel7,
        multiLevel8,
        multiLevel9,
        multiLevel10,
        boom;
    #region levelArrays
    /// <summary > Arrays voor het bouwen van levels
    int[] level5 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] levelTeGroot = new int[] { 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 7, 7, 7, 7, 7, 7, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 4, 0, 7, 7, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 3, 0, 7, 7, 7, 7, 1, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 7, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 7, 0, 7, 0, 7, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 3, 0, 3, 0, 4, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 7, 7, 7, 0, 0, 0, 2, 0, 3, 0, 7, 0, 0, 0, 7, 0, 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 2, 0, 3, 0, 3, 0, 0, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 2, 0, 3, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 3, 0, 3, 0, 3, 0, 3, 0 };
    int[] levelTeGrootMaarDanKlein = new int[] {
        7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 7, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 8, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 7, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 7, 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0 };
    int[] levelDom = new int[] { 0, 0, 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0 };
    int[] levelMulti = new int[] {
        7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 8, 0, 8, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0,
        7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0 };
    int[] level2RampsEasy = new int[] {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0,
        0, 0, 0, 0, 0, 0, 0, 8, 2, 0, 0, 0,
        1, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, };
    int[] level3JumpEasy = new int[] {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 9,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 3, 0,
        0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] level4JumpHard = new int[] {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 };
    int[] level5BlackHoleBallTutorial = new int[] {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 8, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 8, 1, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9,
        0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 3, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] level6BlackHoleBallEnJump = new int[] {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 8, 0, 3, 0, 3, 0, 3, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0,
        1, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 3, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    int[] level7BoosterEasy = new int[] {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 3, 0, 5, 0, 0, 7, 7, 7, 7, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 9, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 3, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] level8BoosterHard = new int[] {
        7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0,
        0, 0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 7, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 8, 0,
        0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 8, 0, 0, 0, 0, 0, 0,
        7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0,
        0, 0, 7, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0,
        0, 0, 0, 7, 7, 7, 7, 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0 };
    int[] level12AdvancedPortal = new int[] {
        0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0,
        1, 0, 0, 0, 0, 0, 7, 9, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 7, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 8, 0, 0, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 8, 0 , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        10, 0, 0, 0, 5, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] multiplayerLevel2Cross = new int[] {
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,2,0,
        0,0,3,0,5,0,0,0,0,0,3,0,3,0,3,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,2,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,2,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,
        0,0,9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,3,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
    //int[] level9PortalEasy = new int[] { 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    /// </summary>
    #endregion

    #region levelmanagerMethods
    GameState gameState;
    Scene currentScene;
    //List<Vector3> coinPositions = new List<Vector3>();
    Vector3 stickerPosition;
    Vector3 finishPosition;
    public List<Coin> coinList = new List<Coin>();
    public StickerObject stickerObject;
    public Finish finish;
    //public Canvas canvas;
    public BallKnop balknop;
    public int currentLevel;

    public PlayerPlatforms playerPlatforms;

    public bool bigLevel;

    //multiplayer booleans
    public bool multiUIRequired;
    public bool p1Finish;
    public bool p2Finish;

    private void Start()
    {
        balknop = gameState.UIManager.canvas.GetComponentInChildren<BallKnop>();
    }
    public Boolean levelIsSpawned = false;

    private void Update()
    {
        //deze is nu leeg kan weg als je wilt was eerst nodig voor MP
    }
    //public PlayerPlatforms PlayerPlatforms
    //{
    //    get
    //    {
    //        return playerPlatforms;
    //    }
    //    set
    //    {
    //        playerPlatforms = value;
    //    }
    //}

    public LevelPlatformen levelPlatformen = new LevelPlatformen();


    //ik wil levels uit een textbestand kunnen opslaan en uitlezen ga ik proberen hier

    public void ReadLevelsFromText(string levelName)
    {
        if (levelName == "")
        {
            InputField InputName = GameObject.Find("LevelName").GetComponent<InputField>();
            levelName = InputName.text;
        }
        string filePath = Application.streamingAssetsPath + "/" + levelName + ".json";

        if (File.Exists(filePath))
        {
            string dataAsJSON = File.ReadAllText(filePath);
            levelPlatformen = JsonUtility.FromJson<LevelPlatformen>(dataAsJSON);
        }
        else
        {
            //moet file createn
            Debug.LogError("Cannot find file!");
        }
        GameState.Instance.gridManager.Build_Grid_FromJSON_Without_Visuals(levelPlatformen.width, levelPlatformen.heigth);
        //moet later terug
        //GameState.Instance.platformManager.BuildLevelFromLevelPlatformen(levelPlatformen);
    }

    public void SaveLevelToText(string levelName)
    {

        if (levelName == "")
        {
            InputField InputName = GameObject.Find("LevelName").GetComponent<InputField>();
            levelName = InputName.text;
        }
        string filePath = Application.streamingAssetsPath + "/" + levelName + ".json";
        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(gameState.levelManager.levelPlatformen);
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    private void Awake()
    {
        gameState = GameState.Instance;
        levelPlatformen = new LevelPlatformen();


        //gameState = GameObject.Find("GameState").GetComponent<GameState>();
    }

    private void DefaultSceneInit()
    {
        //bigLevel = true;
        gameState.UIManager.canvas = Instantiate(gameState.UIManager.UICanvas);
        gameState.UIManager.newLevelInventoryisRequired = true;
        gameState.collectableManager.newCollectablesAreRequired = true;
        gameState.playerCamera.ManualInit();
    }

    private void DefaultSceneEndInit()
    {
        gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);
        //gameState.collectableManager.InitCollectables(coinPositions, finishPosition);
        gameState.BuildingPhaseActive = true;
        //gameState.PreviousLevel = Int32.Parse(sceneName);
        //PlayerDataController.instance.PreviousScene = Int32.Parse(sceneName);
        levelIsSpawned = true;
    }
    #endregion

    public void SpawnLevel(LevelSettings levelSetting)
    {
        DefaultSceneInit();

        levelSetting.Init();
        bigLevel = levelSetting.bigLevel;
        //dit verhaal moet in levesettings zelf gebeuren
        //playerplatforms
        playerPlatforms = new PlayerPlatforms(levelSetting.playerPlatformsArray[1], levelSetting.playerPlatformsArray[0], levelSetting.playerPlatformsArray[2], levelSetting.playerPlatformsArray[3], levelSetting.playerPlatformsArray[4]);
        //spawngrid
        gameState.gridManager.Build_Grid_FromJSON_Without_Visuals(gameState.gridManager.width, gameState.gridManager.height);
        //spawnlevel
        gameState.platformManager.BuildLevelFromLevelPlatformen(levelSetting.levelPlatformen);
        //dit is blijkbaar superbelangrijk
        //spawnpoint
        gameState.playerBallManager.SetSpawnpoint(levelSetting.spawnPoint);
        //balzooi
        gameState.playerBallManager.WhatBalls(levelSetting.ballArray[0], levelSetting.ballArray[1], levelSetting.ballArray[2]);

        DefaultSceneEndInit();
    }


    public void InitScene()
    {
        GAManager.StartGame();
        //this.sceneName = sceneName;
        currentLevel = PlayerDataController.instance.PreviousScene;
        Debug.Log(currentLevel);
        string scenestring = SceneManager.GetActiveScene().name;
        //if(scenestring == "1")
        //{
        //    SpawnLevel(levelSettings);
        //}
        //deze if else statement is 400 regels lang JONGE
        if (scenestring == "LevelEditor")
        {
            gameState.gridManager.width = 11;
            gameState.gridManager.height = 12;

            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0, 0);

            levelPlatformen.tileList = new int[gameState.gridManager.width * gameState.gridManager.height];

            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
        }

        // af
        else if (currentLevel == 1)
        {
            if (!levelIsSpawned)
            {
                gameState.tutorialManager.StartTutorial();
                SpawnLevel(introLevel);
            }
        }

        // af
        else if (currentLevel == 2)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(level2RampEasy);
            }
        }

        // af
        else if (currentLevel == 3)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(level3JumpsEasy);
            }
        }

        // vervang deze met ander jump level
        else if (currentLevel == 4)
        {
            if (!levelIsSpawned)
            {
                if (!levelIsSpawned)
                {
                    SpawnLevel(boom);
                    
                }
            }
        }

        // af
        else if (currentLevel == 5)
        {
            if (!levelIsSpawned)
            {
                gameState.tutorialManager.StartTutorial();
                gameState.tutorialManager.changeBallTutorial = true;
                SpawnLevel(level5BlackHoleTutorial);
            }
        }

        // af
        else if (currentLevel == 6)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level6BlackHoleBallAndJump);
            }
        }

        // af
        else if (currentLevel == 7)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level7BoosterEasy);
            }
        }

        // af
        else if (currentLevel == 8)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level8BoosterHard);
            }
        }

        // af?
        else if (currentLevel == 9)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level9PortalEasy);
            }
        }

        // af? portal & trampoline
        else if (currentLevel == 10)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level10TrampolineAdvanced);
            }
        }

        // af, lift easy
        else if (currentLevel == 11)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level11);
                gameState.platformManager.InitLift(42, 84);
            }
        }

        // af, lift hard
        else if (currentLevel == 12)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(LevelSettingLevel12LiftMoeilijk);
                gameState.platformManager.InitLift(263, 43);
            }
        }

        // af, cannon level easy
        else if (currentLevel == 13)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level13);
            }
        }

        // niet af (cannon level hard, kaput op moment)
        else if (currentLevel == 14)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level14);
                //bigLevel = true;

                //gameState.UIManager.canvas = Instantiate(canvas);
                //gameState.UIManager.newLevelInventoryisRequired = true;
                ////gameState.collectableManager.newCollectablesAreRequired = true;
                ////coinPositions.Clear();
                ////coinList.Clear();
                //GameState.Instance.playerCamera.ManualInit();
                ////gameState.playerCamera = Instantiate(gameState.playerCamera);
                //Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                //gameState.gridManager.width = 40;
                //gameState.gridManager.height = 12;
                //playerPlatforms = new PlayerPlatforms(3, 4, 0, 0, 2);
                //gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
                //gameState.playerBallManager.SetSpawnpoint(41);
                //gameState.platformManager.BuildLevel6();


                //gameState.playerBallManager.WhatBalls(true, true, true);
                //gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);

                //gameState.BuildingPhaseActive = true;
                //GameState.Instance.PreviousLevel = Int32.Parse(sceneName);
                //PlayerDataController.instance.PreviousScene = Int32.Parse(sceneName);
            }
        }

        else if (currentLevel == 15)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level15);
            }
        }

        else if (currentLevel == 17)
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;

                //gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                //gameState.collectableManager.newCollectablesAreRequired = true;
                //coinPositions.Clear();
                //coinList.Clear();
                GameState.Instance.playerCamera.ManualInit();
                //gameState.playerCamera = Instantiate(gameState.playerCamera);
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                gameState.gridManager.width = 20;
                gameState.gridManager.height = 11;
                playerPlatforms = new PlayerPlatforms(4, 4, 1, 0, 0);
                gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
                gameState.playerBallManager.SetSpawnpoint(60);
                //gameState.platformManager.Build_Vertical_Slice_Level6();
                gameState.platformManager.Build_Vertical_Slice_Level7();


                gameState.playerBallManager.WhatBalls(true, true, true);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);

                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 4;
                PlayerDataController.instance.PreviousScene = 4;
            }
        }

        // niet af (coins en finish in array zetten)
        else if (currentLevel == 18)
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;

                //gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                GameState.Instance.playerCamera.ManualInit();
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                levelPlatformen.tileList = levelDom;
                levelPlatformen.width = 20;
                levelPlatformen.heigth = 10;
                gameState.gridManager.width = 20;
                gameState.gridManager.height = 10;
                playerPlatforms = new PlayerPlatforms(7, 7, 4, 3, 0);
                GameState.Instance.gridManager.Build_Grid_FromJSON_Without_Visuals(levelPlatformen.width, levelPlatformen.heigth);

                gameState.playerBallManager.SetSpawnpoint(41);
                //int[] coinarray = new int[] { 46, 127, 133 };
                //int finishPosition = 39;
                //GameState.Instance.platformManager.BuildLevelFromLevelPlatformen(levelPlatformen, coinarray, finishPosition);

                gameState.playerBallManager.WhatBalls(true, true, true);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);

                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 6;
                PlayerDataController.instance.PreviousScene = 6;
            }
        }

        ///multiplayer levels moet nog ff via dezelfde manier voor nu 10 scenes
        //dit is een super klein multiplayer leveltje coins moeten naar boven en in het midden
        else if (SceneManager.GetActiveScene().name == "MultiplayerLevel1")
        {
            if (!levelIsSpawned)
            {

                SpawnLevel(multiLevel1);
                //vies
                Destroy(GameObject.Find("player1Ball"));
                Destroy(GameObject.Find("player2Ball"));
                //einde vies
                gameState.gridManager.InitPlayerGridsMultiPlayer(0, 9, 0, 9,10,19,0,9);
                gameState.playerManager.MultiPlayerBallInit(42, 57);
                gameState.UIManager.AddMultiplayerUI();
                ////GameState.Instance.PreviousLevel = 27;
                ////PlayerDataController.instance.PreviousScene = 27;
            }
        }

        else if (SceneManager.GetActiveScene().name == "MultiplayerLevel2")
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel2);
                //vies
                Destroy(GameObject.Find("player1Ball"));
                Destroy(GameObject.Find("player2Ball"));
                //einde vies
                //naamgeving zo slecht/vies bij playergrids
                //eerste 2 is het begin eindpunt maar die laatste 2 is gewoon de hoogte 
                gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 125, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(1, 125);
                gameState.UIManager.AddMultiplayerUI();
                
            }
        }
        else if (SceneManager.GetActiveScene().name == "MultiplayerLevel3")
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel3);
                //vies
                Destroy(GameObject.Find("player1Ball"));
                Destroy(GameObject.Find("player2Ball"));
                //einde vies
                //naamgeving zo slecht/vies bij playergrids
                //eerste 2 is het begin eindpunt maar die laatste 2 is gewoon de hoogte 
                //gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 125, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(1, 13);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
        else if (SceneManager.GetActiveScene().name == "MultiplayerLevel4")
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel4);
                //vies
                Destroy(GameObject.Find("player1Ball"));
                Destroy(GameObject.Find("player2Ball"));
                //einde vies
                //naamgeving zo slecht/vies bij playergrids
                //eerste 2 is het begin eindpunt maar die laatste 2 is gewoon de hoogte 
                //gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 125, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(0, 1);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
        else if (SceneManager.GetActiveScene().name == "MultiplayerLevel5")
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel5);
                //vies
                Destroy(GameObject.Find("player1Ball"));
                Destroy(GameObject.Find("player2Ball"));
                //einde vies
                //naamgeving zo slecht/vies bij playergrids
                //eerste 2 is het begin eindpunt maar die laatste 2 is gewoon de hoogte 
                //gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 125, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(1, 13);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
        else if (SceneManager.GetActiveScene().name == "MultiplayerLevel6")
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel6);
                //vies
                Destroy(GameObject.Find("player1Ball"));
                Destroy(GameObject.Find("player2Ball"));
                //einde vies
                //naamgeving zo slecht/vies bij playergrids
                //eerste 2 is het begin eindpunt maar die laatste 2 is gewoon de hoogte 
                //gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 125, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(1, 13);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
        else if (SceneManager.GetActiveScene().name == "MultiplayerLevel7")
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel7);
                //vies
                Destroy(GameObject.Find("player1Ball"));
                Destroy(GameObject.Find("player2Ball"));
                //einde vies
                //naamgeving zo slecht/vies bij playergrids
                //eerste 2 is het begin eindpunt maar die laatste 2 is gewoon de hoogte 
                //gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 125, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(1, 23);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
        else if (SceneManager.GetActiveScene().name == "MultiplayerLevel8")
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel8);
                //vies
                Destroy(GameObject.Find("player1Ball"));
                Destroy(GameObject.Find("player2Ball"));
                //einde vies
                //naamgeving zo slecht/vies bij playergrids
                //eerste 2 is het begin eindpunt maar die laatste 2 is gewoon de hoogte 
                //gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 125, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(1, 19);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
        else if (SceneManager.GetActiveScene().name == "MultiplayerLevel9")
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel9);
                //vies
                Destroy(GameObject.Find("player1Ball"));
                Destroy(GameObject.Find("player2Ball"));
                //einde vies
                //naamgeving zo slecht/vies bij playergrids
                //eerste 2 is het begin eindpunt maar die laatste 2 is gewoon de hoogte 
                //gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 125, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(1, 13);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
        else if (SceneManager.GetActiveScene().name == "MultiplayerLevel10")
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel10);
                //vies
                Destroy(GameObject.Find("player1Ball"));
                Destroy(GameObject.Find("player2Ball"));
                //einde vies
                //naamgeving zo slecht/vies bij playergrids
                //eerste 2 is het begin eindpunt maar die laatste 2 is gewoon de hoogte 
                //gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 125, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(1, 19);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
    }



    public void SetRollingPhase()
    {
        gameState.BuildingPhaseActive = false;
        gameState.RollingPhaseActive = true;
        gameState.uIRollingManager.ChangeEnviroment();

        //if (sceneName != "1" || !PhotonNetwork.IsConnected)
        //{
        //    balknop.gameObject.SetActive(false);
        //}
        if (currentLevel != 1 || !PhotonNetwork.InRoom)
        {
            //idk man fucking error bullshit magic spell try catch
            try
            {
                balknop.gameObject.SetActive(false);
            }
            catch
            {

            }
        }
        if (PhotonNetwork.IsConnected)
        {
            //deze oplossing is zo smerig wil niet eens een comment schrijven
            //de bal van de andere speler wordt altijd als een prefabnaam(Clone) gespawnd en zo kan ik ervoor zorgen dat ie op de client die dit uitvoert niet verplaatst wordt
            //ENIGE manier die tot nu toe werkt
            if (PhotonNetwork.IsMasterClient)
            {
                GameObject.Find("player1Ball").GetComponent<Rigidbody>().isKinematic = false;
                GameObject.Find("Photon Player Ball(Clone)").GetComponent<Rigidbody>().isKinematic = true;

            }
            else
            {
                GameObject.Find("player2Ball").GetComponent<Rigidbody>().isKinematic = false;
                GameObject.Find("Photon Player Ball(Clone)").GetComponent<Rigidbody>().isKinematic = true;
            }
            //ik wil hetlater toch echt zo doen dit hierboven zo WACK
            //if (gameState.playerBallManager.activePlayer.GetPhotonView().IsMine)
            //{
            //    GameObject mijnBal = gameState.playerBallManager.activePlayer;
            //    mijnBal.GetComponent<Rigidbody>().isKinematic = false;
            //}
            //else if (gameState.playerBallManager.activePlayer.GetPhotonView().IsMine)
            //{
            //    GameObject andereBal = gameState.playerBallManager.activePlayer;
            //    andereBal.GetComponent<Rigidbody>().isKinematic = true;
            //}
        }
        else
        {
            gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    public void SetBuildingPhase()
    {
        if (PlayerDataController.instance.PreviousScene == 1)
        {
            gameState.tutorialManager.RollingFinished = true;
        }

        if (PhotonNetwork.InRoom)
        {
            PhotonView view = gameState.playerBallManager.activePlayer.GetComponent<PhotonView>();
            if (PhotonNetwork.IsMasterClient)
            {
                view.RPC("FlagUnHit", RpcTarget.All, "masterhit");
            }
            else
            {
                view.RPC("FlagUnHit", RpcTarget.All, "clienthit");
            }
        }
        if (SceneManager.GetActiveScene().name != "VictoryScreen")
        {
            gameState.RollingPhaseActive = false;
            gameState.playerBallManager.respawnBal();
            gameState.platformManager.RespawnCollectables();

            //if (sceneName != "1"||!PhotonNetwork.IsConnected)
            //{
            //    balknop.gameObject.SetActive(true);
            //}
            if (SceneManager.GetActiveScene().name != "1" || !PhotonNetwork.InRoom)
            {
                //idk man fucking error bullshit magic spell try catch
                try
                {
                    balknop.gameObject.SetActive(true);
                }
                catch
                {

                }
            }
            gameState.platformManager.lift.ResetPlatform();
            gameState.BuildingPhaseActive = true;
            gameState.uIRollingManager.ChangeEnviroment();
            gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().isKinematic = true;

        }
        else
        {
            gameState.RollingPhaseActive = false;
            gameState.BuildingPhaseActive = true;
        }

    }
}



///////////////////////////////////////////
//////dit hieronder kan allemaal weg??///// idk, you tell me Denk t wel zal het uitcommenten voor de zekerheid
///////////////////////////////////////////


////SceneLoading and generals
////https://www.alanzucconi.com/2016/03/30/loading-bar-in-unity/
////
//    internal void AsynchronousLoadStart(string scene)
//    {
//        //coinList.Clear();
//        IEnumerator coroutine;
//        //niet vergeten die unsubscribe te doen enzo
//        SceneManager.sceneLoaded += SceneIsLoaded;
//        coroutine = AsynchronousLoad(scene);
//        StartCoroutine(coroutine);

//    }

//    private void SceneIsLoaded(Scene arg0, LoadSceneMode arg1)
//    {
//        //InitScene(arg0.name);
//        InitScene();
//    }

//    IEnumerator AsynchronousLoad(string scene)
//    {
//        yield return null;

//        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
//        ao.allowSceneActivation = false;

//        while (!ao.isDone)
//        {
//            // [0, 0.9] > [0, 1]
//            float progress = Mathf.Clamp01(ao.progress / 0.9f);
//            Debug.Log("Loading progress: " + (progress * 100) + "%");

//            // Loading completed
//            if (ao.progress == 0.9f)
//            {
//                ao.allowSceneActivation = true;

//            }
//            yield return null;
//        }
//    }
//}
//}
//scheit voor scheit mensen
//if (sceneName == "lol1")
//        {
//            gameState.gridManager.width = 11;
//            gameState.gridManager.heigth = 12;
//            gameState.gridManager.Build_Grid1_Without_Visuals();
//            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
//gameState.playerManager.player.SetSpawnpoint(1);
//            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0);
//SetCoinPositions(1);
//SetCoinPositions(2);
//SetCoinPositions(3);
//SetStickerPositions(4);
//SetfinishPositions(5);

//gameState.collectableManager.InitCollectables(coinPositions, stickerPosition, finishPosition);
//            gameState.levelManager.SetBuildingPhase();
//        }

//        else if (sceneName == "TestLevel1")
//        {

//            //lees level uit Json en vul levelPlatformen
//            //ReadLevelsFromText("Level1.json");

//            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0);

////Dit moet ergens anders
//gameState.gridManager.width = 11;
//            gameState.gridManager.heigth = 12;

//            //deze code is superbelangrijk voor het opslaan van levels
//            //levelPlatformen.width = 11;
//            //levelPlatformen.heigth = 12;
//            //levelPlatformen.tileList = new int[11*12];

//            levelPlatformen.tileList = new int[gameState.gridManager.width * gameState.gridManager.heigth];
//            gameState.gridManager.Build_Grid1_Without_Visuals();

//            gameState.platformManager.BuildLevelFromText(levelPlatformen);
//            //gameState.platformManager.Build_Level1(levelPlatformen);



//            //SaveLevelToText("Level1.json");
//        }
//    else if (sceneName == "VerticalSliceLevel2")
//        {
//            gameState.gridManager.width = 20;
//            gameState.gridManager.heigth = 11;

//            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0);

//gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();

//            gameState.platformManager.Build_Vertical_Slice_Level6();
//        }else if (sceneName == "VerticalSliceLevel3")
//        {
//            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
//gameState.gridManager.width = 27;
//            gameState.gridManager.heigth = 17;

//            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0);

//gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
//            gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[0] + playeradjustment;
//            gameState.platformManager.Build_Vertical_Slice_Level3();
//        }
//        else if (sceneName == "VerticalSliceLevel4")
//        {
//            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
//gameState.gridManager.width = 27;
//            gameState.gridManager.heigth = 17;

//            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0);
//gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
//            gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[0] + playeradjustment;
//        }
