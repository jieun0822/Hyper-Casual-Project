using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //0 - level, 1 - need exp, 3 - accumulated exp
    //4 - get vitality
    public int[] level1;
    public int[] level2;
    public int[] level3;
    public int[] level4;
    public int[] level5;
    public int[] level6;
    public int[] level7;
    public int[] level8;
    public int[] level9;
    public int[] level10;
    public int[] level11;
    public int[] level12;
    public int[] level13;
    public int[] level14;
    public int[] level15;
    public int[] level16;
    public int[] level17;
    public int[] level18;
    public int[] level19;
    public int[] level20;
    public int[] level21;
    public int[] level22;
    public int[] level23;
    public int[] level24;
    public int[] level25;
    public int[] level26;
    public int[] level27;
    public int[] level28;
    public int[] level29;
    public int[] level30;
    public int[] level31;
    public int[] level32;
    public int[] level33;
    public int[] level34;
    public int[] level35;
    public int[] level36;
    public int[] level37;
    public int[] level38;
    public int[] level39;
    public int[] level40;
    public int[] level41;
    public int[] level42;
    public int[] level43;
    public int[] level44;
    public int[] level45;
    public int[] level46;
    public int[] level47;
    public int[] level48;
    public int[] level49;
    public int[] level50;
    public int[] level51;
    public int[] level52;
    public int[] level53;
    public int[] level54;
    public int[] level55;
    public int[] level56;
    public int[] level57;
    public int[] level58;
    public int[] level59;
    public int[] level60;
    public int[] level61;
    public int[] level62;
    public int[] level63;
    public int[] level64;
    public int[] level65;
    public int[] level66;
    public int[] level67;
    public int[] level68;
    public int[] level69;
    public int[] level70;
    public int[] level71;
    public int[] level72;
    public int[] level73;
    public int[] level74;
    public int[] level75;
    public int[] level76;
    public int[] level77;
    public int[] level78;
    public int[] level79;
    public int[] level80;
    public int[] level81;
    public int[] level82;
    public int[] level83;
    public int[] level84;
    public int[] level85;
    public int[] level86;
    public int[] level87;
    public int[] level88;
    public int[] level89;
    public int[] level90;
    public int[] level91;
    public int[] level92;
    public int[] level93;
    public int[] level94;
    public int[] level95;
    public int[] level96;
    public int[] level97;
    public int[] level98;
    public int[] level99;
    public int[] level100;

    public Dictionary<int, int[]> levelDic;
    // Start is called before the first frame update
    void Start()
    {

        levelDic = new Dictionary<int, int[]>
        {
            {level1[0], new [] {level1[0], level1[1], level1[2], level1[3]} },
            {level2[0], new [] {level2[0], level2[1], level2[2], level2[3]} },
            {level3[0], new [] {level3[0], level3[1], level3[2], level3[3]} },
            {level4[0], new [] {level4[0], level4[1], level4[2], level4[3]} },
            {level5[0], new [] {level5[0], level5[1], level5[2], level5[3]} },
            {level6[0], new [] {level6[0], level6[1], level6[2], level6[3]} },
            {level7[0], new [] {level7[0], level7[1], level7[2], level7[3]} },
            {level8[0], new [] {level8[0], level8[1], level8[2], level8[3]} },
            {level9[0], new [] {level9[0], level9[1], level9[2], level9[3]} },
            {level10[0], new [] {level10[0], level10[1], level10[2], level10[3]} },
            {level11[0], new [] {level11[0], level11[1], level11[2], level11[3]} },
            {level12[0], new [] {level12[0], level12[1], level12[2], level12[3]} },
            {level13[0], new [] {level13[0], level13[1], level13[2], level13[3]} },
            {level14[0], new [] {level14[0], level14[1], level14[2], level14[3]} },
            {level15[0], new [] {level15[0], level15[1], level15[2], level15[3]} },
            {level16[0], new [] {level16[0], level16[1], level16[2], level16[3]} },
            {level17[0], new [] {level17[0], level17[1], level17[2], level17[3]} },
            {level18[0], new [] {level18[0], level18[1], level18[2], level18[3]} },
            {level19[0], new [] {level19[0], level19[1], level19[2], level19[3]} },
            {level20[0], new [] {level20[0], level20[1], level20[2], level20[3]} },
            {level21[0], new [] {level21[0], level21[1], level21[2], level21[3]} },
            {level22[0], new [] {level22[0], level22[1], level22[2], level22[3]} },
            {level23[0], new [] {level23[0], level23[1], level23[2], level23[3]} },
            {level24[0], new [] {level24[0], level24[1], level24[2], level24[3]} },
            {level25[0], new [] {level25[0], level25[1], level25[2], level25[3]} },
            {level26[0], new [] {level26[0], level26[1], level26[2], level26[3]} },
            {level27[0], new [] {level27[0], level27[1], level27[2], level27[3]} },
            {level28[0], new [] {level28[0], level28[1], level28[2], level28[3]} },
            {level29[0], new [] {level29[0], level29[1], level29[2], level29[3]} },
            {level30[0], new [] {level30[0], level30[1], level30[2], level30[3]} },

            {level31[0], new [] {level31[0], level31[1], level31[2], level31[3]} },
            {level32[0], new [] {level32[0], level32[1], level32[2], level32[3]} },
            {level33[0], new [] {level33[0], level33[1], level33[2], level33[3]} },
            {level34[0], new [] {level34[0], level34[1], level34[2], level34[3]} },
            {level35[0], new [] {level35[0], level35[1], level35[2], level35[3]} },
            {level36[0], new [] {level36[0], level36[1], level36[2], level36[3]} },
            {level37[0], new [] {level37[0], level37[1], level37[2], level37[3]} },
            {level38[0], new [] {level38[0], level38[1], level38[2], level38[3]} },
            {level39[0], new [] {level39[0], level39[1], level39[2], level39[3]} },
            {level40[0], new [] {level40[0], level40[1], level40[2], level40[3]} },
            {level41[0], new [] {level41[0], level41[1], level41[2], level41[3]} },
            {level42[0], new [] {level42[0], level42[1], level42[2], level42[3]} },
            {level43[0], new [] {level43[0], level43[1], level43[2], level43[3]} },
            {level44[0], new [] {level44[0], level44[1], level44[2], level44[3]} },
            {level45[0], new [] {level45[0], level45[1], level45[2], level45[3]} },
            {level46[0], new [] {level46[0], level46[1], level46[2], level46[3]} },
            {level47[0], new [] {level47[0], level47[1], level47[2], level47[3]} },
            {level48[0], new [] {level48[0], level48[1], level48[2], level48[3]} },
            {level49[0], new [] {level49[0], level49[1], level49[2], level49[3]} },
            {level50[0], new [] {level50[0], level50[1], level50[2], level50[3]} },
            {level51[0], new [] {level51[0], level51[1], level51[2], level51[3]} },
            {level52[0], new [] {level52[0], level52[1], level52[2], level52[3]} },
            {level53[0], new [] {level53[0], level53[1], level53[2], level53[3]} },
            {level54[0], new [] {level54[0], level54[1], level54[2], level54[3]} },
            {level55[0], new [] {level55[0], level55[1], level55[2], level55[3]} },
            {level56[0], new [] {level56[0], level56[1], level56[2], level56[3]} },
            {level57[0], new [] {level57[0], level57[1], level57[2], level57[3]} },
            {level58[0], new [] {level58[0], level58[1], level58[2], level58[3]} },
            {level59[0], new [] {level59[0], level59[1], level59[2], level59[3]} },
            {level60[0], new [] {level60[0], level60[1], level60[2], level60[3]} },

            {level61[0], new [] {level61[0], level61[1], level61[2], level61[3]} },
            {level62[0], new [] {level62[0], level62[1], level62[2], level62[3]} },
            {level63[0], new [] {level63[0], level63[1], level63[2], level63[3]} },
            {level64[0], new [] {level64[0], level64[1], level64[2], level64[3]} },
            {level65[0], new [] {level65[0], level65[1], level65[2], level65[3]} },
            {level66[0], new [] {level66[0], level66[1], level66[2], level66[3]} },
            {level67[0], new [] {level67[0], level67[1], level67[2], level67[3]} },
            {level68[0], new [] {level68[0], level68[1], level68[2], level68[3]} },
            {level69[0], new [] {level69[0], level69[1], level69[2], level69[3]} },
            {level70[0], new [] {level70[0], level70[1], level70[2], level70[3]} },
            {level71[0], new [] {level71[0], level71[1], level71[2], level71[3]} },
            {level72[0], new [] {level72[0], level72[1], level72[2], level72[3]} },
            {level73[0], new [] {level73[0], level73[1], level73[2], level73[3]} },
            {level74[0], new [] {level74[0], level74[1], level74[2], level74[3]} },
            {level75[0], new [] {level75[0], level75[1], level75[2], level75[3]} },
            {level76[0], new [] {level76[0], level76[1], level76[2], level76[3]} },
            {level77[0], new [] {level77[0], level77[1], level77[2], level77[3]} },
            {level78[0], new [] {level78[0], level78[1], level78[2], level78[3]} },
            {level79[0], new [] {level79[0], level79[1], level79[2], level79[3]} },
            {level80[0], new [] {level80[0], level80[1], level80[2], level80[3]} },
            {level81[0], new [] {level81[0], level81[1], level81[2], level81[3]} },
            {level82[0], new [] {level82[0], level82[1], level82[2], level82[3]} },
            {level83[0], new [] {level83[0], level83[1], level83[2], level83[3]} },
            {level84[0], new [] {level84[0], level84[1], level84[2], level84[3]} },
            {level85[0], new [] {level85[0], level85[1], level85[2], level85[3]} },
            {level86[0], new [] {level86[0], level86[1], level86[2], level86[3]} },
            {level87[0], new [] {level87[0], level87[1], level87[2], level87[3]} },
            {level88[0], new [] {level88[0], level88[1], level88[2], level88[3]} },
            {level89[0], new [] {level89[0], level89[1], level89[2], level89[3]} },
            {level90[0], new [] {level90[0], level90[1], level90[2], level90[3]} },

            {level91[0], new [] {level91[0], level91[1], level91[2], level91[3]} },
            {level92[0], new [] {level92[0], level92[1], level92[2], level92[3]} },
            {level93[0], new [] {level93[0], level93[1], level93[2], level93[3]} },
            {level94[0], new [] {level94[0], level94[1], level94[2], level94[3]} },
            {level95[0], new [] {level95[0], level95[1], level95[2], level95[3]} },
            {level96[0], new [] {level96[0], level96[1], level96[2], level96[3]} },
            {level97[0], new [] {level97[0], level97[1], level97[2], level97[3]} },
            {level98[0], new [] {level98[0], level98[1], level98[2], level98[3]} },
            {level99[0], new [] {level99[0], level99[1], level99[2], level99[3]} },
            {level100[0], new [] {level100[0], level100[1], level100[2], level100[3]} }
        };

        /*
        levelDic.Add(level1[0], level1);
        levelDic.Add(level2[0], level2);
        levelDic.Add(level3[0], level3);
        levelDic.Add(level4[0], level4);
        levelDic.Add(level5[0], level5);
        levelDic.Add(level6[0], level6);
        levelDic.Add(level7[0], level7);
        levelDic.Add(level8[0], level8);
        levelDic.Add(level9[0], level9);
        levelDic.Add(level10[0], level10);
        levelDic.Add(level11[0], level11);
        levelDic.Add(level12[0], level12);
        levelDic.Add(level13[0], level13);
        levelDic.Add(level14[0], level14);
        levelDic.Add(level15[0], level15);
        levelDic.Add(level16[0], level16);
        levelDic.Add(level17[0], level17);
        levelDic.Add(level18[0], level18);
        levelDic.Add(level19[0], level19);
        levelDic.Add(level20[0], level20);
        levelDic.Add(level21[0], level21);
        levelDic.Add(level22[0], level22);
        levelDic.Add(level23[0], level23);
        levelDic.Add(level24[0], level24);
        levelDic.Add(level25[0], level25);
        levelDic.Add(level26[0], level26);
        levelDic.Add(level27[0], level27);
        levelDic.Add(level28[0], level28);
        levelDic.Add(level29[0], level29);
        levelDic.Add(level30[0], level30);

        levelDic.Add(level31[0], level31);
        levelDic.Add(level32[0], level32);
        levelDic.Add(level33[0], level33);
        levelDic.Add(level34[0], level34);
        levelDic.Add(level35[0], level35);
        levelDic.Add(level36[0], level36);
        levelDic.Add(level37[0], level37);
        levelDic.Add(level38[0], level38);
        levelDic.Add(level39[0], level39);
        levelDic.Add(level40[0], level40);
        levelDic.Add(level41[0], level41);
        levelDic.Add(level42[0], level42);
        levelDic.Add(level43[0], level43);
        levelDic.Add(level44[0], level44);
        levelDic.Add(level45[0], level45);
        levelDic.Add(level46[0], level46);
        levelDic.Add(level47[0], level47);
        levelDic.Add(level48[0], level48);
        levelDic.Add(level49[0], level49);
        levelDic.Add(level50[0], level50);
        levelDic.Add(level51[0], level51);
        levelDic.Add(level52[0], level52);
        levelDic.Add(level53[0], level53);
        levelDic.Add(level54[0], level54);
        levelDic.Add(level55[0], level55);
        levelDic.Add(level56[0], level56);
        levelDic.Add(level57[0], level57);
        levelDic.Add(level58[0], level58);
        levelDic.Add(level59[0], level59);
        levelDic.Add(level60[0], level60);

        levelDic.Add(level61[0], level61);
        levelDic.Add(level62[0], level62);
        levelDic.Add(level63[0], level63);
        levelDic.Add(level64[0], level64);
        levelDic.Add(level65[0], level65);
        levelDic.Add(level66[0], level66);
        levelDic.Add(level67[0], level67);
        levelDic.Add(level68[0], level68);
        levelDic.Add(level69[0], level69);
        levelDic.Add(level70[0], level70);
        levelDic.Add(level71[0], level71);
        levelDic.Add(level72[0], level72);
        levelDic.Add(level73[0], level73);
        levelDic.Add(level74[0], level74);
        levelDic.Add(level75[0], level75);
        levelDic.Add(level76[0], level76);
        levelDic.Add(level77[0], level77);
        levelDic.Add(level78[0], level78);
        levelDic.Add(level79[0], level79);
        levelDic.Add(level80[0], level80);
        levelDic.Add(level81[0], level81);
        levelDic.Add(level82[0], level82);
        levelDic.Add(level83[0], level83);
        levelDic.Add(level84[0], level84);
        levelDic.Add(level85[0], level85);
        levelDic.Add(level86[0], level86);
        levelDic.Add(level87[0], level87);
        levelDic.Add(level88[0], level88);
        levelDic.Add(level89[0], level89);
        levelDic.Add(level90[0], level90);

        levelDic.Add(level91[0], level91);
        levelDic.Add(level92[0], level92);
        levelDic.Add(level93[0], level93);
        levelDic.Add(level94[0], level94);
        levelDic.Add(level95[0], level95);
        levelDic.Add(level96[0], level96);
        levelDic.Add(level97[0], level97);
        levelDic.Add(level98[0], level98);
        levelDic.Add(level99[0], level99);
        levelDic.Add(level100[0], level100);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
