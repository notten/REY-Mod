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
    [RequireComponent(typeof(OnOffComponent))]    
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    public partial class TorchStandObject : WorldObject
    {
        public override string FriendlyName { get { return "Torch Stand"; } } 

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(TorchItem),
        };

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Lights");                                 
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(0.5f);
            this.GetComponent<PropertyAuthComponent>().Initialize(AuthModeType.Inherited);



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class TorchStandItem : WorldObjectItem<TorchStandObject>
    {
        public override string FriendlyName { get { return "Torch Stand"; } } 
        public override string Description { get { return "A stand for a torch."; } }

        static TorchStandItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(BasicCraftingSkill), 1)]
    public partial class TorchStandRecipe : Recipe
    {
        public TorchStandRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TorchStandItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(BasicCraftingEfficiencySkill), 10, BasicCraftingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RopeItem>(typeof(BasicCraftingEfficiencySkill), 1, BasicCraftingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(2, BasicCraftingSpeedSkill.MultiplicativeStrategy, typeof(BasicCraftingSpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(TorchStandRecipe), Item.Get<TorchStandItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<TorchStandItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Torch Stand", typeof(TorchStandRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}