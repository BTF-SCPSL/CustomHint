# CustomHint
## Descrição
Um plug-in que permite que você crie sua própria HUD personalizada para o servidor.
Para garantir funcionalidade adequada, o plug-in exige o **HintServiceMeow** e o **Newtonsoft.json**, que estão incluídos nos lançamentos para todas as versões.  
Para sugestões, por favor, me mencione no servidor do Discord EXILED ou na DM: @narin4ik.

## Guia

### Como instalar o plug-in?
Go to the [latest release](https://github.com/BTF-SCPSL/CustomHint/releases). After that, download all the *dll* and *zip* files from the release, then upload *CustomHint.dll* and *dependencies.zip* to the server into the Plugins folder (`.../EXILED/Plugins`), and then extract *dependencies.zip*.   
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
  # Provide data for plugin statistics (data will not be shared with third parties).
  send_anon_info: true
  # Enable or disable the HUD settings in the game menu.
  hud_settings: true
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
  # Sync speed for hints. Available values: UnSync, Slowest, Slow, Normal, Fast, Fastest.
  sync_speed: 'Fastest'
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
# Settings header text.
  header_text: 'CustomHint'
  # Name of the item in the settings.
  button_name: 'Server HUD display'
  # Buttom hint.
  button_hint: 'Enable or disable server HUD display.'
  # Enable button.
  button_enable: 'Enable'
  # Disable button.
  button_disable: 'Disable'
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
  - role: Scp3114
    name: 'SCP-3114'
```
And after all that... Voilà! Everything is ready! You can restart the server *(fully)*, and CustomHint will work perfectly.  
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
| {hints}                 | Hints from the Hints.yml file.             |

## Third-party placeholders
If you want to add your own plugin for adding placeholders, please contact me via DM: *@narin4ik*
| Author            | Name            | Description                                 |
| ----------------------- | ----------------------- | ------------------------------------------ |
| [Narin](https://github.com/Narin4ik)            | [SteamIDPlaceholder](https://github.com/Narin4ik/SteamIDPlaceholder)                               | Displays STEAMID64.                               |
| [Chuppa2](https://github.com/Chuppa2)            | [CRCHPlaceholder](https://github.com/Chuppa2/CRCHPlaceholder)                               | Displays a spectated players custom info.                               |
| [Narin](https://github.com/Narin4ik)            | [RespawnTimer](https://github.com/Narin4ik/RespawnTimer)                               | Adding a timers until the spawn of MTF and CI.                               |
