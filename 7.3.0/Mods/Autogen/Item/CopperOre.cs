namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;


    [Serialized]
    [Minable, Solid,Wall]
    public partial class CopperOreBlock :
        Block           
    { }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(15000)]      
    [ResourcePile]                                          
    [Currency]              
    public partial class CopperOreItem :
    BlockItem<CopperOreBlock>
    {
        public override string FriendlyName { get { return "Copper Ore"; } }
        public override string FriendlyNamePlural { get { return "Copper Ore"; } } 
        public override string Description { get { return "Unrefined ore with traces of copper."; } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(CopperOreStacked1Block),
            typeof(CopperOreStacked2Block),
            typeof(CopperOreStacked3Block),
            typeof(CopperOreStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class CopperOreStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class CopperOreStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class CopperOreStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class CopperOreStacked4Block : PickupableBlock { } //Only a wall if it's all 4 CopperOre
}