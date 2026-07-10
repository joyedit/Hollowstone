# Hollowstone

A Vintage Story mod: hide your valuables in plain sight.

Chisel a **Hidden Lockbox** into any cobblestone block — indistinguishable
from plain stone until you sneak-click it with an empty hand and a small
stone drawer slides out. Write a **Page of Secrets** to find your stashes
again: whoever holds a page sees floating markers over its owner's lockboxes,
so a stolen page is a treasure map to another player's hoard. Program a
four-stone **press combination** onto a box and even a thief who finds it
stares at mute cobblestone.

## Crafting

- **Hidden Lockbox** — hammer, chisel, and a cobblestone block in a vertical
  column (tools survive, losing some durability). One lockbox per rock type.
- **Page of Secrets** — parchment + ink and quill (the quill survives). The
  page seals to its crafter.

## Using it

- **Open**: sneak + right-click with an empty hand. A plain right-click does
  nothing — just like real cobblestone.
- **Find**: hold a Page of Secrets; the owner's lockboxes within 30 blocks
  show a floating label, visible through walls.
- **Protect**: hold your own page and sneak-click four spots (face quadrants)
  on a box to set its combination. It then opens only to that sequence,
  entered with an empty hand. Wrong presses look and sound identical to right
  ones. Break and re-place the box to clear its combination.
- **Steal**: pages drop on death like any item. Guard your map.

## Multiplayer notes

Everything is server-authoritative: combinations never leave the server, and
lockbox positions are only sent to a client while it holds the right page and
stands within 30 blocks. Opening a box puffs visible stone dust — stashes are
findable by observant neighbours.

## Building

`dotnet build -c Debug Hollowstone.csproj`, or `./deploy.sh` to build, zip,
and drop `Hollowstone_<version>.zip` into the local Mods folder.
