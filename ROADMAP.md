# Roadmap

本路线图用于描述 `ONI Miku Duplicant Mod` 在开发初期的阶段目标。当前以中文为主，后续再逐步补多语言说明。

## v0.1.x 内部测试阶段

目标：
- 稳定本地安装流程
- 固化最小可测目录结构
- 保持 `Dupery` 兼容边界清晰
- 让测试者可以验证角色是否正常出现在打印舱候选中

范围内：
- 文档补全
- 本地安装/卸载脚本
- 基础结构检查脚本
- 当前角色配置与本地化数据的整理

暂不处理：
- 新动画制作
- 新贴图制作
- `Dupery` 底层逻辑扩展
- 超出 `PersonalityOutline` schema 的配置字段

## v0.2.x 可玩性增强阶段

目标方向：
- 评估是否能在不破坏兼容性的前提下补更多文本表现
- 梳理更多本地化内容
- 提升测试流程的一致性与可复现性

候选工作：
- 增加更完整的安装说明截图或示例
- 补充更多语言文档
- 增加更细的脚本报错与提示

## v0.3.x 内容扩展评估阶段

目标方向：
- 评估角色主题是否需要更多可选文本与表现层扩展
- 在确认兼容边界前，不承诺落地属性、兴趣或额外机制

仅作为设计储备，不视为当前承诺：
- Interests: Creativity + Operating
- Starting attributes: Creativity +9, Operating +3, Excavation -2, Construction -1, Strength/Combat -2
- Positive trait concept: Resonant Beat
- Negative trait concept: Soundstage Sensitive

## 长期原则

- 代码与文档可持续整理
- 资源资产与代码边界明确分离
- 不以破坏 schema 兼容性的方式硬塞未支持功能
