namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
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
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;



    [Serialized]
    [Weight(1500)]
    [Currency]
    public partial class SkinnedElkItem :
    Item
    {
        public override string FriendlyName { get { return "Skinned Elk"; } }
        public override string Description { get { return "A skinned elk."; } }

    }

}