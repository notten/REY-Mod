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
    [Weight(10)]
    [Fuel(50)]
    [Currency]              
    public partial class WoodPulpItem :
    Item                                     
    {
        public override string FriendlyName { get { return "Wood Pulp"; } }
        public override string FriendlyNamePlural { get { return "Wood Pulp"; } } 
        public override string Description { get { return "A byproduct of processing lumber, wood pulp can be burned for pitch, pressed into paper or used as fuel."; } }

    }

}