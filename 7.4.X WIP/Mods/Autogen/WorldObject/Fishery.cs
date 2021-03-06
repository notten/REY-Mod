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
    [RequireComponent(typeof(RoomRequirementsComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(1, 18)]        
    public partial class FisheryObject : WorldObject
    {
        public override string FriendlyName { get { return "Fishery"; } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class FisheryItem : WorldObjectItem<FisheryObject>
    {
        public override string FriendlyName { get { return "Fishery"; } } 
        public override string Description { get { return "A place to create fishing poles and traps."; } }

        static FisheryItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(FishingSkill), 1)]
    public partial class FisheryRecipe : Recipe
    {
        public FisheryRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FisheryItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(BasicCraftingEfficiencySkill), 20, BasicCraftingEfficiencySkill.MultiplicativeStrategy),
				new CraftingElement<RopeItem>(typeof(BasicCraftingEfficiencySkill), 3, BasicCraftingEfficiencySkill.MultiplicativeStrategy),				
            };
            SkillModifiedValue value = new SkillModifiedValue(1, BasicCraftingSpeedSkill.MultiplicativeStrategy, typeof(BasicCraftingSpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(FisheryRecipe), Item.Get<FisheryItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<FisheryItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Fishery", typeof(FisheryRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}