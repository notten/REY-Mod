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
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(HousingComponent))]                          
    [RequireComponent(typeof(RoomRequirementsComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomContainment]
    [RequireRoomVolume(45)]                              
    [RequireRoomMaterialTier(2, 32)]        
    public partial class BakeryOvenObject : WorldObject
    {
        public override string FriendlyName { get { return "Bakery Oven"; } } 

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem),
            typeof(WoodPulpItem),
        };

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(10);                    
            this.GetComponent<HousingComponent>().Set(BakeryOvenItem.HousingVal);
            this.GetComponent<PropertyAuthComponent>().Initialize(AuthModeType.Inherited);



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(10000)]
    public partial class BakeryOvenItem : WorldObjectItem<BakeryOvenObject>
    {
        public override string FriendlyName { get { return "Bakery Oven"; } } 
        public override string Description { get { return "A solidly built brick oven useful for baking all manner of treats."; } }

        static BakeryOvenItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 3,
                                                    TypeForRoomLimit = "Baking",
                                                    DiminishingReturnPercent = 0.3f
                                                };}}       
    }


    [RequiresSkill(typeof(StoneworkingSkill), 2)]
    public partial class BakeryOvenRecipe : Recipe
    {
        public BakeryOvenRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BakeryOvenItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(typeof(StoneworkingEfficiencySkill), 30, StoneworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(StoneworkingEfficiencySkill), 6, StoneworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(StoneworkingEfficiencySkill), 10, StoneworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(StoneworkingEfficiencySkill), 10, StoneworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(20, StoneworkingSpeedSkill.MultiplicativeStrategy, typeof(StoneworkingSpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(BakeryOvenRecipe), Item.Get<BakeryOvenItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<BakeryOvenItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Bakery Oven", typeof(BakeryOvenRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}