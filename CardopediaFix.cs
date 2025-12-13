using CardopediaFix.Patches;

namespace CardopediaFix
{
    public class CardopediaFix : Mod
    {

        public static CardopediaFix Instance;
        public int mIdeaGroup;
        public ModLogger logger;

        public void Awake()
        {
            CardopediaFix.Instance = this;
            this.logger = Logger;
            //localization_ideagroup
            //EnumHelper.GetName
            //CardopediaScreen
            //Villager villager = new Villager();
            //Logger.Log("Id: ideagroup_" + customCardType.ToString().ToLower());
            //SokLoc.instance.LoadTermsFromFile(System.IO.Path.Combine(this.Path, "localization_ideagroup.tsv"), false);
            //SokLoc.instance.CurrentLanguage
            this.Harmony.PatchAll(typeof(CombatStatsPatches));
            Cards.hammer
        }

        public override void Ready()
        {
            Logger.Log("Ready!");
        }
    }
}