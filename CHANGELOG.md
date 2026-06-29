# Changelog

本项目当前采用手工维护的变更记录，先以中文为主。

## v0.1.0

发布日期：2026-06-30

定位：内部测试版，开发初期整理发布

新增：
- 补充 `README.md`，包含安装说明、依赖说明、macOS 本地安装路径与测试步骤
- 新增 `CHANGELOG.md`
- 新增 `ROADMAP.md`
- 新增 `NOTICE.md`
- 新增 `scripts/install_local_macos.sh`
- 新增 `scripts/uninstall_local_macos.sh`
- 新增 `scripts/check_mod_structure.sh`

当前内容：
- 提供 `mod_folder/` 作为本地安装目录
- 包含 `personalities.json`、多语言 `strings/*.po` 与当前已导出的附件动画资源
- 明确当前运行依赖 `Dupery`

限制：
- 不包含 `Dupery` 底层实现
- 未实现的设计目标不会写入 `personalities.json` 生效字段
- 本次整理不涉及动画、贴图与底层资源改造
