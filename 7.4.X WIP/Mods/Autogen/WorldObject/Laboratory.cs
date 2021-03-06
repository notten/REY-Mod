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
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(RoomRequirementsComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(3, 18)]        
    public partial class LaboratoryObject : WorldObject
    {
        public override string FriendlyName { get { return "Laboratory"; } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            this.GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class LaboratoryItem : WorldObjectItem<LaboratoryObject>
    {
        public override string FriendlyName { get { return "Laboratory"; } } 
        public override string Description { get { return "For researching the science side of cooking. Science rules!"; } }

        static LaboratoryItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(IndustrialEngineeringSkill), 2)]
    public partial class LaboratoryRecipe : Recipe
    {
        public LaboratoryRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LaboratoryItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrialEngineeringEfficiencySkill), 30, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GlassJarItem>(typeof(IndustrialEngineeringEfficiencySkill), 15, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(IndustrialEngineeringEfficiencySkill), 8, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(120, IndustrialEngineeringSpeedSkill.MultiplicativeStrategy, typeof(IndustrialEngineeringSpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(LaboratoryRecipe), Item.Get<LaboratoryItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<LaboratoryItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Laboratory", typeof(LaboratoryRecipe));
            CraftingComponent.AddRecipe(typeof(FactoryObject), this);
        }
    }
}