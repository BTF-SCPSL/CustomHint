# CustomHint
## 描述
一个允许为服务器制作自定义 HUD 的插件。
要使插件正常工作，需要使用 **HintServiceMeow**、**SSMenuSystem** 和 **Newtonsoft.Json**，后者在所有版本的发行版中都有提供。   
如有建议，请在 EXILED 服务器的 Discord 上 ping 或私信联系：@narin4ik   
## 教程
### 如何安装插件？
跳转到 [最新版本](https://github.com/BTF-SCPSL/CustomHint/releases)，下载发行版中的所有 *dll* 和 *zip* 文件，然后上传到服务器：将 *CustomHint.dll* 和 *dependencies.zip* 放入 Plugins 文件夹（`.../EXILED/Plugins`），然后解压 *dependencies.zip*。  
安装完成后 *启动或重启* 服务器。  
完成上述操作后，`.../EXILED/Configs` 目录下的 `[port]-config.yml` 和 `[port]-translation.yml` 文件将会生成配置。  
### 插件配置
我们先从最简单的开始，即 `[port]-config.yml`。使用 *Ctrl+F* 组合键，输入 `custom_hint` 来定位插件配置。  
默认的 `[port]-config.yml` 配置如下，所有选项都在其中列出：  
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
设置完 `[port]-config.yml` 后，进入 `[port]-translation.yml`，再次使用组合键 *Ctrl+F*，输入 `custom_hint`。   
在那里我们会看到如下内容：
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
关于 **SSMenuSystem**，要删除示例设置，请在配置中找到 `ss_menu_system`，并将 `enable_examples` 的值设为 `false`。  
完成上述所有操作后……Voilà！一切就绪！您可以完全重启服务器，CustomHint 即可完美运行。  
感谢所有使用该插件的朋友，祝您好运！  
## 占位符

| 占位符                        | 描述                   |
| -------------------------- | -------------------- |
| `{servername}`             | 服务器名称                |
| `{ip}`                     | 服务器 IP 地址            |
| `{port}`                   | 服务器端口                |
| `{tps}`                    | 服务器 TPS              |
| `{player_nickname}`        | 玩家昵称                 |
| `{player_role}`            | 玩家角色                 |
| `{player_gamerole}`        | 玩家游戏内角色              |
| `{round_time}`             | 回合已进行总时长             |
| `{round_duration_seconds}` | 回合时长（秒）              |
| `{round_duration_minutes}` | 回合时长（分钟）             |
| `{round_duration_hours}`   | 回合时长（小时）             |
| `{classd_num}`             | Class-D 人员数量         |
| `{scientist_num}`          | 科学家数量                |
| `{facilityguard_num}`      | 设施警卫数量               |
| `{mtf_num}`                | MTF 数量           |
| `{ci_num}`                 | CI 数量           |
| `{scp_num}`                | SCP 对象数量             |
| `{spectators_num}`         | 观众数量（包括 Overwatch）   |
| `{generators_activated}`   | 已激活发电机数量             |
| `{generators_max}`         | 发电机总数量               |
| `{current_time}`           | 您时区的当前实时时间           |
| `{hints}`                  | 来自 Hints.yml 文件的提示文本 |
## 第三方占位符
如果您希望添加自己的插件以提供占位符，请私信联系：*@narin4ik*
| 作者                                    | 名称                                                                   | 描述                  |
| ------------------------------------- | -------------------------------------------------------------------- | ------------------- |
| [Narin](https://github.com/Narin4ik)  | [SteamIDPlaceholder](https://github.com/Narin4ik/SteamIDPlaceholder) | 以占位符形式显示 STEAMID64。 |
| [Chuppa2](https://github.com/Chuppa2) | [CRCHPlaceholder](https://github.com/Chuppa2/CRCHPlaceholder)        | 显示被观察玩家的自定义信息。      |
| [Narin](https://github.com/Narin4ik)  | [RespawnTimer](https://github.com/Narin4ik/RespawnTimer)             | 添加 MTF 和 CI 到达的计时器。 |

使用 **ChatGPT** 翻译，如有任何问题，请私信：*@narin4ik*
