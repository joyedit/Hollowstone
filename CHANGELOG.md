# Changelog

All notable changes to Hollowstone are documented here.

## 1.0.0

Initial standalone release. Hollowstone began life inside The Seraph's Ledger
(1.9.0–1.11.0) and was extracted into its own mod; version 1.0.0 is
feature-identical to the lockbox feature in Seraph's Ledger 1.11.0.

### Features
- **Hidden Lockbox**: chisel an 8-slot lockbox into any cobblestone block
  (hammer + chisel + cobblestone in a vertical column; one variant per rock
  type). Placed, it passes for plain cobblestone — the block info HUD reports
  it as cobblestone, there is no mouse-over hint, placement snaps to 90°, and
  a plain right-click behaves like clicking real stone. Sneak + right-click
  with an empty hand slides a small stone drawer out (stone-on-stone sound,
  rock-colored dust puff, dark cavity) and opens the inventory. Supports
  padlocks/reinforcement like other containers.
- **Page of Secrets**: crafted from parchment + ink and quill, sealed to its
  crafter. Whoever *holds* a page sees floating labels (visible through walls,
  within 30 blocks) over the *owner's* lockboxes — a stolen page is a treasure
  map to another player's stashes. Pages are live keys: boxes placed after the
  page was written show up too. The reveal is server-authoritative; positions
  beyond 30 blocks never cross the wire.
- **Stone-press combinations**: the owner, holding their own page, sneak-clicks
  four face quadrants to program a box. It then opens only to that empty-hand
  press sequence; right and wrong presses give identical feedback. Combinations
  are stored server-side only and cleared by breaking the box.

### Migration from The Seraph's Ledger
- Ships `config/remaps.json` remapping all `seraphsledger:hiddenlockbox-*`
  blocks and the `seraphsledger:secretspage` item to the `hollowstone` domain.
  **Install Hollowstone and Seraph's Ledger ≥ 1.12.0 together before the first
  world load** — the remap only applies cleanly if it's present on the first
  load after the old blocks disappear.
- Adopts the lockbox registry (owners and combinations) from the old
  `seraphsledger-lockboxes` savegame data automatically.
