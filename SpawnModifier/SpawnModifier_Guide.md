# 怪物刷新时间快速调整工具 (SpawnModifier) 操作文档

此工具用于快速读取并修改 Mir2 服务端的二进制数据库 `Server.MirDB` 中的怪物刷新时间。支持对指定的地图 ID 范围内的所有怪物刷新点进行时间微调（增加/减少），并在修改前自动备份原数据库，确保可以随时安全地进行还原。

---

## 📂 工具存放位置

编译生成的工具位于：
`d:\happy\my-mir\Crystal_Run\Server\SpawnModifier.exe` (以及依赖的 `SpawnModifier.dll` 模块)。

> [!IMPORTANT]
> **请务必在该工具所在的目录下（即含有 `Server.MirDB` 数据库文件的目录中）运行此工具。**

---

## 🚀 基础使用方法

本工具支持两种运行方式：**交互模式** 和 **命令行参数模式**。

### 1. 交互模式（双击直接运行）
直接双击运行 `SpawnModifier.exe`。它会在控制台中逐步提示您输入必要的修改参数：
1. **起始地图 Index**：例如输入 `85`。
2. **结束地图 Index**：例如输入 `97`。
3. **刷新时间调整分钟数**：
   - 增加时间：输入正数。例如输入 `45`（所有刷新时间增加 45 分钟）。
   - 减少时间：输入负数。例如输入 `-45`（所有刷新时间减少 45 分钟）。
4. 输入完成后，工具会打印修改日志，完成**自动备份**，并覆盖保存原数据库。

---

### 2. 命令行参数模式 (自动化脚本/快捷方式)
您可以通过 Windows 命令行 (CMD / PowerShell) 传入参数来快速执行修改，适合做成批处理脚本。

#### 命令行格式
```bash
.\SpawnModifier.exe --start <起始地图Index> --end <结束地图Index> --adjust <调整分钟数>
```
*或简写为：*
```bash
.\SpawnModifier.exe -s <起始地图Index> -e <结束地图Index> -a <调整分钟数>
```

#### 使用示例
1. **将地图 85 至 97 怪物刷新时间统一增加 45 分钟：**
   ```powershell
   .\SpawnModifier.exe -s 85 -e 97 -a 45
   ```
2. **将地图 85 至 97 怪物刷新时间统一减少 45 分钟（即还原）：**
   ```powershell
   .\SpawnModifier.exe -s 85 -e 97 -a -45
   ```

---

## 🔄 备份与回滚 (还原数据库)

### 1. 自动备份
每次运行本工具进行修改时，工具在保存修改前，都会在同级目录下自动创建一个备份文件，命名规则为：
`Server.MirDB.bak_adjust_yyyyMMdd_HHmmss`
例如：`Server.MirDB.bak_adjust_20260527_122619`

### 2. 如何人工回滚还原数据库
如果某次修改效果不满意，您可以：
1. 关闭正在运行的游戏服务端（M2Server）。
2. 在 `Crystal_Run/Server/` 目录下，删除当前的 `Server.MirDB`。
3. 将刚才自动生成的备份文件（例如 `Server.MirDB.bak_adjust_20260527_122619`）重命名还原为 `Server.MirDB`。
4. 重新开启服务端即可。

---

## 🛠️ 快捷脚本推荐

为了方便反复使用，您可以直接在 `Crystal_Run/Server/` 目录下创建两个批处理 (`.bat`) 快捷键脚本：

### 一键增加 45 分钟 脚本 (`怪物刷新时间_增加45分钟.bat`)
```bat
@echo off
cd /d %~dp0
SpawnModifier.exe -s 85 -e 97 -a 45
pause
```

### 一键还原(减少) 45 分钟 脚本 (`怪物刷新时间_一键还原.bat`)
```bat
@echo off
cd /d %~dp0
SpawnModifier.exe -s 85 -e 97 -a -45
pause
```
*(将上述代码分别存为相应的 `.bat` 文件后，直接放置在 `Crystal_Run/Server/` 目录下即可一键运行。)*
