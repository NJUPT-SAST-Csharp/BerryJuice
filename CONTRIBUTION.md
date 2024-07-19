# SAST C# 组 - Summer of Code 2024 参与指南

## Foreword

欢迎各位参加SAST C# 组的SOC😋🎉！这篇指南旨在帮助各位理解这个项目的目标，以及目前规划的架构，以便于发现并确定自己能够担当的部分。

在最初的立项会上，我们决定以制作一个记账软件为本次SOC的主题，并且（费了一番功夫）取名为 **BerryJuice**。

## BerryJuice🧃项目简介

这是一款记账软件，目前规划至少提供**记账**、**预算计划**、**资产计算**的功能

今年年初的WOC中，我们采用C/S架构，基于WPF进行桌面端开发和ASP.NET进行后端WebAPI的开发，顺利完成了SAST Wiki（虽然这玩意太过简陋了😢）。这次SOC很显然我们有了在技术选择上更大的自由，因此我打算尝试一些新东西，选择ASP.NET Core Blazor作为本次SOC使用的技术栈，使用C#同时完成前端和后端开发，并且使用ORM框架EF Core。

对于后端部分，我有考虑去实现Modular Monolith + DDD，如果各位感兴趣并且愿意负责后端部分可以尝试。

## 项目构成

解决方案下各个项目的关系如下：

- BerryJuice
	- Frontend
		- `BerryJuice.Blazor`: Blazor网页前端，跑在服务器上的部分
		- `BerryJuice.Blazor.Client`: Blazor网页前端，会被编译成WASM并且跑在浏览器里，以提高组件的响应速度
	- Backend
		- `BerryJuice.WebAPI`: 提供WebAPI的项目，也就是Controller层所在的位置
		- `*.Application`: 熟悉的Application层，承载业务逻辑，基于事件总线，使用`Query`和`Command`
		- `*.Domain`: 领域模型
		- `*.Infrastructure`: 后端的一些基础设施，比如事件总线，.NET主机的配置，EF Core配置
	- Shared
		- 从🦊的`SAST Image`里狠狠`Ctrl+C`&`Ctrl+V`下来的一些东西，感觉不如过段时间打个nuget包方便分发（
	- `BerryJuice`: 整个解决方案唯一的可启动项目，这样可以在一个可执行文件里同时跑Blazor服务和WebAPI服务，减小开发调试与部署的烦恼，不需要连着开几个小黑框挂后台或者用更高级的Aspire，并且或许能提升性能。
 - 
### 一些其它注解

- 必须使用事件总线完成后端Application层的开发。可以使用MediatR
- ORM框架可以使用EF Core
- WebAPI层可以不用做，因为Blazor项目里我计划直接使用Application里面的Query & Command，绕开WebAPI，减轻开发难度并减小开销。
- Blazor可以混合CSR (Client Side Rendering，客户端侧渲染)和SSR (Server Side Rendering，服务器侧渲染)。为了和传统的C\S架构划清界限，我考虑仅在SSR组件中与后端进行交互，不过混合CSR组件可以让我不写一行JavaScirpt（
- 如果想要实现DDD，或许可以参考👇下面的功能来设计聚合

## 项目功能

核心具有三大功能（领域？）

- 记账功能
	- 每一笔账需要包含：收入/支出金额，支付方式，标签，注释，时间，……
		- 增删查改
			- 考虑到账单项目可能会非常多，为了网络传输开销考虑，需要实现按指定时间范围进行查询
	- 标签
		- 增删查改
- 预算规划
	- 预算是一个具有一定周期和一个金额，能统计这个周期内所有（或包含指定标签的）账单的开销总和并与设定的预算金额进行比较的东西。可以同时存在多个预算（比如同时存在年预算，月预算，天预算），并且它们会计算各自周期内全部的流水。
		- 同样的，增删查改都不能少
	- 子预算。是**在某一个预算中**，单独设置的一个金额。将这部分金额分配给某个特定标签的账目，该标签的账目会优先使用对应子预算的部分，如果用完了才会使用一笔预算中剩余的部分，不得使用其它不含该标签的子预算；而没有对应子预算的账目只能使用一笔预算中剩余部分。
		- 增删查改
- 资产计算
	- 资产账户。比如支付宝，微信，银行卡，饭卡等，每一个资产账户都有自己的剩余金额，可以根据账单进行计算，用户可以手动校准剩余金额。
		- 增删查改
		- 校准时将与账单统计的差值作为金额自动生成一笔账目。

## 如何参与？

速速打开你的Rider / Visual Studio / VS Code，Clone这个仓库：[NJUPT-SAST-Csharp/BerryJuice 的 Git 仓库](https://github.com/NJUPT-SAST-Csharp/BerryJuice.git)

然后再在任意一款现代浏览器中打开[NJUPT-SAST-Csharp/BerryJuice 的 GitHub 页面](https://github.com/NJUPT-SAST-Csharp/BerryJuice)，先点右上角的Watch，随后点开上方的Project，即可一览当前项目的进度😋

Project中分为Overview Frontend Backend这三个主板块，用于跟踪总体进度，尽量不要动它。我没有对Frontend Backend主板块的条目做细化，这件事交给想要参与某一领域的你来进行，你可以在All板块里对应板块增加item，并且**设置Domain**（可以设置为任何你觉得合适的名称，这样Frontend Backend这两个个主板块里就不会显示了😋之后我们再来协调统一的名称）。比如基础设施搭建这一Domain，我可以细化为EF Core配置、MediatR配置等等，然后你可以把这个item分配给自己，并且相当方便的直接在仓库里创建issue，码完代码提交pr一气呵成。
