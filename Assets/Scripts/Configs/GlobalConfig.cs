using System.Collections.Generic;

namespace Configs
{
    public static class GlobalConfig
    {
        public static List<BuildConfig> BuildConfigList = new List<BuildConfig>()
        {
            new BuildConfig(1,"弓兵营","Builds/Archery"),
            new BuildConfig(2,"铁匠铺","Builds/Blacksmith"),
            new BuildConfig(3,"农场","Builds/Farm"),
            new BuildConfig(4,"训练场","Builds/Keep"),
            new BuildConfig(5,"图书馆","Builds/Library"),
            new BuildConfig(6,"弓箭塔1","Builds/Tower_A"),
            new BuildConfig(7,"弓箭塔2","Builds/Tower_B"),
            new BuildConfig(8,"弓箭塔3","Builds/Tower_C"),
            new BuildConfig(9,"魔法院","Builds/MageTower"),
            new BuildConfig(10,"骑兵营","Builds/Stables"),
            new BuildConfig(11,"木材厂","Builds/LumberMill"),
            new BuildConfig(12,"机械厂","Builds/Workshop"),
            new BuildConfig(13,"术士馆","Builds/Temple"),
        };

        public static BuildConfig GetBuildConfigByType(int build_Type)
        {
            BuildConfig config = null;
            for (int i = 0; i < BuildConfigList.Count; i++)
            {
                if (BuildConfigList[i].build_Type == build_Type)
                {
                    config = BuildConfigList[i];
                    break;
                }
            }
            return config;
        }
    }



    public class BuildConfig
    {
        public string res;
        public string name;
        public int build_Type;

        public BuildConfig(int buildType, string name, string res)
        {
            this.res = res;
            this.name = name;
            this.build_Type = buildType;
        }
    }
}
