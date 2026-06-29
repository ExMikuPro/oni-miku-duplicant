# ONI Miku Duplicant Mod

`ONI Miku Duplicant Mod` 是一个面向《Oxygen Not Included》本地测试的角色 Mod 仓库，当前整理目标是 `v0.1.0` 内部测试版。

本仓库目前包含以下内容：
- `mod_folder/`：可直接用于本地安装的 Mod 目录
- `mod_folder/personalities.json`：角色人格配置
- `mod_folder/strings/*.po`：本地化文本
- `mod_folder/anim/`：当前已导出的角色相关动画资源
- `anim_source/`：用于生成附件动画的源文件

当前仓库不包含 `Dupery` 的底层实现，因此 `personalities.json` 中只能使用 `Dupery` 已支持的 `PersonalityOutline` 字段。

## 依赖说明

运行本 Mod 需要以下前置条件：
- 已安装《Oxygen Not Included》
- 已安装并启用 `Dupery`
- 需要将本仓库中的 `mod_folder/` 放入 ONI 的本地 Mod 目录，或通过脚本创建软链接

当前已确认可安全使用的字段包括：
- `Printable`
- `Randomize`
- `StartingMinion`
- `Name`
- `Description`
- `Gender`
- `PersonalityType`
- `StressTrait`
- `JoyTrait`
- `StickerType`
- `HeadShape`
- `Mouth`
- `Eyes`
- `Hair`
- `Body`

以下内容目前仍然只是设计目标，不应直接写入 `personalities.json` 作为生效配置：
- Interests: Creativity + Operating
- Starting attributes: Creativity +9, Operating +3, Excavation -2, Construction -1, Strength/Combat -2
- Positive trait concept: Resonant Beat
- Negative trait concept: Soundstage Sensitive
- Printing pod specific flavor text beyond the standard personality description/localization

## 当前角色设定

- Name: Hatsune Miku / Miku
- Theme: 打印舱似乎产出了一名带有持续声学异常的复制人，并对重复劳动表现出异常稳定的节奏适应性
- 当前已落地内容：自定义名称、描述、压力反应、愉悦反应与视觉附件

## 安装说明

### 手动安装

1. 确认已安装并启用 `Dupery`
2. 打开 ONI 本地 Mod 目录
3. 将仓库中的 `mod_folder/` 整个复制到本地 Mod 目录
4. 如有需要，可将目标目录重命名为更易识别的文件夹名，但不要修改目录内文件结构
5. 启动游戏，在 Mod 列表中启用本 Mod 与 `Dupery`

### macOS 本地安装路径

macOS 下 ONI Local mods 常见路径为：

```bash
~/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Local
```

如果你希望本地开发时直接引用仓库目录而不是复制文件，可使用：

```bash
./scripts/install_local_macos.sh
```

如需卸载对应软链接，可使用：

```bash
./scripts/uninstall_local_macos.sh
```

如需覆盖默认安装路径，可在执行前设置环境变量：

```bash
ONI_LOCAL_MODS_DIR="/your/custom/Local" ./scripts/install_local_macos.sh
```

## 测试步骤

建议每次改动后至少完成以下手动测试：

1. 运行结构检查脚本

```bash
./scripts/check_mod_structure.sh
```

2. 通过手动复制或 `install_local_macos.sh` 将 `mod_folder/` 安装到 ONI Local mods
3. 启动 ONI，并确认 `Dupery` 与本 Mod 已启用
4. 进入新档或可访问打印舱的存档
5. 打开打印舱候选列表，确认可出现 `Hatsune Miku`
6. 检查角色名称、描述、压力反应、愉悦反应和视觉附件是否按预期加载
7. 切换游戏语言时，确认至少已有的 `strings/*.po` 文件不会导致缺失报错

## 仓库脚本

- `scripts/install_local_macos.sh`：将 `mod_folder/` 软链接到 macOS ONI Local mods
- `scripts/uninstall_local_macos.sh`：移除对应软链接
- `scripts/check_mod_structure.sh`：检查基础 Mod 结构是否齐全

## 动画源文件说明

当前附件动画源文件位于 `anim_source/`。如果你需要重新构建相关动画，可在仓库根目录放置 `kanimal-SE` 可执行文件后运行：

```bash
./kanimal-cli.exe kanim anim_source/printcassidy_accessories.scml --output ./anim_files/ --interpolate
```

以上命令依赖 [kanimal-SE](https://github.com/skairunner/kanimal-SE)。

## 约束说明

- 不要在当前阶段修改动画、贴图等资源文件，除非明确进入资源制作流程
- 不要把 `Dupery` 未支持的字段写入 `personalities.json`
- 不要改动 `personalities.json` 的 schema 边界来模拟未实现机制

更多版本记录、路线图与授权/声明请参见：
- [CHANGELOG.md](/Volumes/Sector0/AppData/RiderProjects/oni-miku-duplicant/CHANGELOG.md)
- [ROADMAP.md](/Volumes/Sector0/AppData/RiderProjects/oni-miku-duplicant/ROADMAP.md)
- [NOTICE.md](/Volumes/Sector0/AppData/RiderProjects/oni-miku-duplicant/NOTICE.md)
