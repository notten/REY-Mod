namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;

    [Serialized]
    [Weight(10000)]
    public partial class TreasuryItem : WorldObjectItem<TreasuryObject>
    {
        public override string FriendlyName { get { return "Treasury"; } } 
        public override string Description { get { return "Allows the setting of taxes."; } }

        static TreasuryItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(MetalworkingSkill), 2)]
    public partial class TreasuryRecipe : Recipe
    {
        public TreasuryRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TreasuryItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HingeItem>(typeof(MetalworkingEfficiencySkill), 10, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(MetalworkingEfficiencySkill), 30, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GoldIngotItem>(typeof(MetalworkingEfficiencySkill), 10, MetalworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(60, MetalworkingSpeedSkill.MultiplicativeStrategy, typeof(MetalworkingSpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(TreasuryRecipe), Item.Get<TreasuryItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<TreasuryItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Treasury", typeof(TreasuryRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}