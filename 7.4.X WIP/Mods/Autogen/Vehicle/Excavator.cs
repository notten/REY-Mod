namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    
    [Serialized]
    [Weight(30000)]  
    public class ExcavatorItem : WorldObjectItem<ExcavatorObject>
    {
        public override string FriendlyName         { get { return "Excavator"; } }
        public override string Description          { get { return "I EAT DIRT!"; } }
    }

    [RequiresSkill(typeof(IndustrialEngineeringSkill), 1)] 
    public class ExcavatorRecipe : Recipe
    {
        public ExcavatorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ExcavatorItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GearboxItem>(typeof(IndustrialEngineeringEfficiencySkill), 10, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(IndustrialEngineeringEfficiencySkill), 20, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(IndustrialEngineeringEfficiencySkill), 40, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<AdvancedCombustionEngineItem>(typeof(IndustrialEngineeringEfficiencySkill), 2, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(IndustrialEngineeringEfficiencySkill), 20, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(IndustrialEngineeringEfficiencySkill), 10, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RubberItem>(typeof(IndustrialEngineeringEfficiencySkill), 8, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(50);

            this.Initialize("Excavator", typeof(ExcavatorRecipe));
            CraftingComponent.AddRecipe(typeof(FactoryObject), this);
        }
    }
}