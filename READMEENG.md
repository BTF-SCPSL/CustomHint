# CustomHint
## Description
A plugin that allows you to create your own custom HUD for the server.  
To ensure proper functionality, the plugin requires **HintServiceMeow** and **Newtonsoft.Json**, which are included in the releases for all versions.  
For suggestions, please ping me on the EXILED Discord server or DM: @narin4ik.  

## Guide

### How to install the plugin?
Go to the [latest release](https://github.com/BTF-SCPSL/CustomHint/releases). Download all the *dll* files from the release. Then upload the following files to the server: *CustomHint.dll* and *HintServiceMeow.dll* to the Plugins folder (`.../EXILED/Plugins`), and *Newtonsoft.Json.dll* to the dependencies folder (`.../EXILED/Plugins/dependencies`).  
After installation, *start/restart* the server.  
Once you've completed all the steps, the configuration will be generated in `.../EXILED/Configs` under `[port]-config.yml` and `[port]-translation.yml`.

### Configuring the plugin
We'll start with the easiest part, `[port]-config.yml`. Use the *CTRL+F* shortcut to search for `custom_hint` and locate the plugin configuration.  
The default `[port]-config.yml` looks like this, with all points explained:
```yaml
custom_hint:
# Plugin enabled (bool)?
  is_enabled: true
  # Debug mode?
  debug: false
  # Enable or disable HUD-related commands.
  enable_hud_commands: true
  # Enable or disable automatic plugin updates.
  auto_updater: true
  # The interval for changing {hints} placeholder (in seconds).
  hint_message_time: 5
  # Default role name for players without a role.
  default_role_name: 'Player'
  # Default role color (for players without roles).
  default_role_color: 'white'
  # Server timezone for placeholder. Use 'UTC' by default or a valid timezone ID (e.g., 'Europe/Kyiv').
  server_time_zone: 'UTC'
  # Enable counting Overwatch players in placeholder {spectators_num}.
  enable_overwatch_counting: true
  # List of hints.
  hints:
  - id: 'firsthint'
    text: 'Hello World!'
    font_size: 15
    position_x: 500
    position_y: 500
    can_be_hidden: true
    roles:
    - ClassD
```
After configuring `[port]-config.yml`, move on to `[port]-translation.yml`. Use *CTRL+F* again to search for `custom_hint`.  
You'll find the following:
```yaml
custom_hint:
# Message displayed when the HUD is successfully hidden.
  hide_hud_success_message: '<color=green>You have successfully hidden the server HUD! To get the HUD back, use .showhud.</color>'
  # Message displayed when HUD is already hidden.
  hide_hud_already_hidden_message: '<color=red>You''ve already hidden the server HUD.</color>'
  # Message displayed when HUD is successfully shown.
  show_hud_success_message: '<color=green>You have successfully returned the server HUD! To hide again, use .hidehud</color>'
  # Message displayed when HUD is already shown.
  show_hud_already_shown_message: '<color=red>You already have the server HUD displayed.</color>'
  # Message displayed when DNT (Do Not Track) mode is enabled.
  dnt_enabled_message: '<color=red>Disable DNT (Do Not Track) mode.</color>'
  # Message displayed when commands are disabled on the server.
  command_disabled_message: '<color=red>This command is disabled on the server.</color>'
  # Round time.
  round_time_formats:
    seconds: '{round_duration_seconds} seconds'
    minutes: '{round_duration_minutes} minutes {round_duration_seconds} seconds'
    hours: '{round_duration_hours} hours {round_duration_minutes} minutes {round_duration_seconds} seconds'
  # Game Role of a player, {player_gamerole} is placeholder.
  game_roles:
  - role: Tutorial
    name: 'Tutorial'
  - role: ClassD
    name: 'Class-D'
  - role: Scientist
    name: 'Scientist'
  - role: FacilityGuard
    name: 'Facility Guard'
  - role: Filmmaker
    name: 'Film Maker'
  - role: Overwatch
    name: 'Overwatch'
  - role: NtfPrivate
    name: 'MTF Private'
  - role: NtfSergeant
    name: 'MTF Sergeant'
  - role: NtfSpecialist
    name: 'MTF Specialist'
  - role: NtfCaptain
    name: 'MTF Captain'
  - role: ChaosConscript
    name: 'CI Conscript'
  - role: ChaosRifleman
    name: 'CI Rifleman'
  - role: ChaosRepressor
    name: 'CI Repressor'
  - role: ChaosMarauder
    name: 'CI Marauder'
  - role: Scp049
    name: 'SCP-049'
  - role: Scp0492
    name: 'SCP-049-2'
  - role: Scp079
    name: 'SCP-079'
  - role: Scp096
    name: 'SCP-096'
  - role: Scp106
    name: 'SCP-106'
  - role: Scp173
    name: 'SCP-173'
  - role: Scp939
    name: 'SCP-939'
```
And after localization... Voilà! Everything is ready! You can restart the server *(fully)*, and CustomHint will work perfectly.  
Thank you to everyone who uses this plugin. Best of luck!  

## Placeholders
| Placeholder            | Description                                 |
| ----------------------- | ------------------------------------------ |
| {servername}            | Server name.                               |
| {ip}                    | Server IP address.                         |
| {port}                  | Server port.                               |
| {tps}                   | Server TPS.                                |
| {player_nickname}       | Player's nickname.                         |
| {player_role}           | Player's role.                             |
| {player_gamerole}       | Player's Game role.                         |
| {round_time}       | General round duration.                 |
| {round_duration_seconds}| Round duration in seconds.                 |
| {round_duration_minutes}| Round duration in minutes.                 |
| {round_duration_hours}  | Round duration in hours.                   |
| {classd_num}            | Number of Class-D personnel.               |
| {scientist_num}         | Number of Scientists.                      |
| {facilityguard_num}     | Number of Facility Guards.                 |
| {mtf_num}               | Number of MTFs.                            |
| {ci_num}                | Number of Chaos Insurgents.                |
| {scp_num}               | Number of SCP subjects.                     |
| {spectators_num}        | Number of spectators (including Overwatch).|
| {generators_activated}  | Number of activated generators.            |
| {generators_max}        | Maximum number of generators.              |
| {current_time}        | Current real time in your time zone.              |
| {hints}                 | Hints from the Hints.txt file.             |

## Third-party placeholders
| Author            | Name            | Description                                 |
| ----------------------- | ----------------------- | ------------------------------------------ |
| [Narin](https://github.com/Narin4ik)            | [SteamIDPlaceholder](https://github.com/Narin4ik/SteamIDPlaceholder)                               | Displays STEAMID64.                               |
| [Chuppa2](https://github.com/Chuppa2)            | [CRCHPlaceholder](https://github.com/Chuppa2/CRCHPlaceholder)                               | Displays a spectated players custom info.                               |
| [Narin](https://github.com/Narin4ik)            | [RespawnTimer](https://github.com/Narin4ik/RespawnTimer)                               | Adding a timers until the spawn of MTF and CI.                               |
