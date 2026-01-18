using CardopediaFix.Patches;
using UnityEngine.InputSystem;

namespace CardopediaFix
{
    public class CardopediaFix : Mod
    {

        public static CardopediaFix Instance;
        //public int mIdeaGroup;
        public ModLogger logger;
        public bool hasLogCardBug;

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
            this.Harmony.PatchAll(typeof(CardopediaEntryElementPatches));
            //CardType.Resources
            //CardData
            //Cards.hammer
        }

        public override void Ready()
        {
            Logger.Log("Ready!");
            try
            {
                string hammerId = "cardopedia_fix_hammer";

                //Logger.Log("CardData Count: " + WorldManager.instance.AllCards.Count);
                //Logger.Log("CardDataPrefabs Count: " + GameDataLoader.instance.CardDataPrefabs.Count);

                if (!WorldManager.instance.HasFoundCard(hammerId))
                {
                    CardData? hammer = null;
                    //ModCard
                    int cardCount = GameDataLoader.instance.CardDataPrefabs.Count;
                    if (cardCount > 0)
                    {
                        for (int i = cardCount - 1; i >= 0; i--)
                        {
                            //j++;
                            if (GameDataLoader.instance.CardDataPrefabs[i].Id == hammerId)
                            {
                                hammer = GameDataLoader.instance.CardDataPrefabs[i];
                                //Logger.Log("My hammer cardData loaded index: " + i + " / " + j);
                                break;
                            }
                        }
                    }
                    if (hammer != null)
                    {
                        WorldManager.instance.FoundCard(hammer);
                    }
                    else 
                    {
                        //Logger.Log("My cardData load failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.ToString());
            }
            //Logger.Log("Ready End!");
        }
    }
}