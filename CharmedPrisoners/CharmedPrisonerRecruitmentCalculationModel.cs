using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.TwoDimension;

namespace CharmedPrisoners
{
    public class CharmedPrisonerRecruitmentCalculationModel : DefaultPrisonerRecruitmentCalculationModel
    {
        private static readonly float[] MinimumValues = new[] { 0.5f, 0.3f, 0.1f, 0.05f, 0.05f, 0.05f, 0.05f };
        private static readonly float[] MaximumValues = new[] { 2.5f, 2.25f, 2f, 1.75f, 1.5f, 0.75f, 0.5f };

        public override float[] GetDailyRecruitedPrisoners(MobileParty mainParty)
        {
            // Default Taleworlds values are { 1f, 0.5f, 0.3f, 0.2f, 0.1f, 0f, 0f }
            // Target values for an "average character" (used as reference) are:
            float[] referenceValues = new[] { 0.95f, 0.55f, 0.4f, 0.3f, 0.2f, 0.1f, 0.075f };

            const float ReferenceMorale = 50f;
            float moraleRecruitmentChange = (mainParty.Morale - ReferenceMorale) / (2 * ReferenceMorale); // 2 * to reduce scaling

            const float ReferenceCharisma = 100f;
            SkillObject charismaSkill = SkillObject.FindFirst((x) => { return x.StringId == "Charm"; });
            float leaderCharisma = (float)mainParty.LeaderHero.GetSkillValue(charismaSkill);
            float charmRecruitmentChange = (leaderCharisma - ReferenceCharisma) / (4 * ReferenceCharisma); // 4 * to reduce scaling

            float totalAdjustmentPercent = moraleRecruitmentChange + charmRecruitmentChange;
            InformationManager.DisplayMessage(new InformationMessage(string.Format("Chance to recruit adjustment: {0}%", totalAdjustmentPercent * 100f)));
            for (int troopTier = 0; troopTier < referenceValues.Length; ++troopTier)
            {
                referenceValues[troopTier] += totalAdjustmentPercent * referenceValues[troopTier];
                referenceValues[troopTier] = Mathf.Clamp(referenceValues[troopTier], MinimumValues[troopTier], MaximumValues[troopTier]);
            }

            return referenceValues;
        }

    }
}
