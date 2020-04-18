using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace CharmedPrisoners
{
    public class CharmedPrisonersModule : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (game.GameType is Campaign)
            {
                gameStarterObject.AddModel(new CharmedPrisonerRecruitmentCalculationModel());
            }
        }

    }

}
