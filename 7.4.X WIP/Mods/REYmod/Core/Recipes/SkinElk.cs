namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(SkinningSkill), 3)]
    public class SkinElkRecipe : Recipe
    {
        public SkinElkRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedElkItem>(1),
               new CraftingElement<LeatherHideItem>(typeof(SkinningEfficiencySkill), 2, SkinningEfficiencySkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ElkCarcassItem>(1),
            };
            this.Initialize("Skin Elk", typeof(SkinElkRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinElkRecipe), this.UILink(), 1, typeof(SkinningSpeedSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}