namespace Eco.Mods.TechTree
{
    using System;
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
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    
    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(2, 18)]        
    public partial class RollingMillObject : 
        WorldObject    
    {
        public override string FriendlyName { get { return "Rolling Mill"; } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            this.GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    public partial class RollingMillItem : WorldObjectItem<RollingMillObject>
    {
        public override string FriendlyName { get { return "Rolling Mill"; } } 
        public override string Description  { get { return  "For rolling steel into more buildable materials."; } }

        static RollingMillItem()
        {
            
        }

    }


    [RequiresSkill(typeof(SteelworkingSkill), 1)]
    public partial class RollingMillRecipe : Recipe
    {
        public RollingMillRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RollingMillItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(SteelworkingEfficiencySkill), 10, SteelworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(45, SteelworkingSpeedSkill.MultiplicativeStrategy, typeof(SteelworkingSpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(RollingMillRecipe), Item.Get<RollingMillItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<RollingMillItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Rolling Mill", typeof(RollingMillRecipe));
            CraftingComponent.AddRecipe(typeof(MachineShopObject), this);
        }
    }
}