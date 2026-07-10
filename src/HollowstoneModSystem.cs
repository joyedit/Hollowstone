using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Hollowstone
{
    // Hollowstone: hidden lockboxes chiseled into cobblestone, found again via
    // a sealed Page of Secrets. Extracted from The Seraph's Ledger 1.11.0; a
    // remaps.json migrates blocks/items from the old seraphsledger domain, and
    // LockboxRegistry adopts the old savegame registry data (owners + combos).
    public class HollowstoneModSystem : ModSystem
    {
        public const string NetworkChannelName = "hollowstone";

        private ICoreClientAPI capi;
        private LockboxRegistry lockboxRegistry;
        private LockboxLabelRenderer lockboxLabels;

        public override void Start(ICoreAPI api)
        {
            base.Start(api);

            // Registered identically on both sides so the message type ids match.
            api.Network.RegisterChannel(NetworkChannelName)
                .RegisterMessageType<LockboxListPacket>();

            api.RegisterBlockClass("HollowstoneLockbox", typeof(BlockHiddenLockbox));
            api.RegisterBlockEntityClass("HollowstoneLockboxBE", typeof(BlockEntityHiddenLockbox));
            api.RegisterItemClass("HollowstoneSecretsPage", typeof(ItemSecretsPage));
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            lockboxRegistry = new LockboxRegistry(api);
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            base.StartClientSide(api);
            capi = api;

            api.Network.GetChannel(NetworkChannelName)
                .SetMessageHandler<LockboxListPacket>(OnLockboxList);

            // Floating labels over hidden lockboxes while a Page of Secrets is
            // held. The server decides what this client may see (page owner's
            // boxes within range); we just render whatever arrives.
            lockboxLabels = new LockboxLabelRenderer(api);
            api.Event.RegisterRenderer(lockboxLabels, EnumRenderStage.Ortho, "hollowstonelockboxlabels");
        }

        private void OnLockboxList(LockboxListPacket packet)
        {
            if (lockboxLabels == null) return;

            var labels = new List<LockboxLabel>();
            var p = packet?.Positions;
            if (p != null)
            {
                string text = Lang.Get("hollowstone:hiddenlockbox-title");
                for (int i = 0; i + 2 < p.Length; i += 3)
                {
                    labels.Add(new LockboxLabel { X = p[i], Y = p[i + 1], Z = p[i + 2], Text = text });
                }
            }
            lockboxLabels.SetLabels(labels);
        }

        public override void Dispose()
        {
            if (lockboxLabels != null)
            {
                capi?.Event.UnregisterRenderer(lockboxLabels, EnumRenderStage.Ortho);
                lockboxLabels.Dispose();
                lockboxLabels = null;
            }
            lockboxRegistry?.Dispose();
            lockboxRegistry = null;
            capi = null;
        }
    }
}
