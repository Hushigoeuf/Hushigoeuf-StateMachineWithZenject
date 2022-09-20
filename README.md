# Hushigoeuf-StateMachineWithZenject
 
<img src="https://github.com/Hushigoeuf/Hushigoeuf-StateMachineWithZenject/blob/main/Common/StateMachineDiagram.png" width="640px">

Пример реализации машины состояний (StateMachine) с использованием Zenject на примере простой игры.

Особенности:
- Каждое состоение является отдельным классом;
- Каждое состояние связано с действиями (actions) и переходами (transitions);
- Состояния могут ссылаться на одинаковые actions и decisions;
- Машина состояний сама проталкивает Update, FixedUpdate и LateUpdate;
- Можно привязать частоту обновления метода Update;
- Есть встроенный MonoDebugger, который отображает данные машины состояний (исп. Odin Inspector).

## Пример использования
Для начала надо определить машину состояний. В примере ниже выделяется отдельный класс для машины состояний игрока. PlayerFacade это может быть MonoBehaviour, который вешается на игровой объект игрока - этот объект является владельцем.

```csharp
public class PlayerStateMachine : SMStateMachineWithZenject
{
    [Inject]
    public PlayerStateMachine(PlayerStartingState initialState, PlayerFacade owner) : base(initialState, owner)
    {
    }
}
```

Каждое состояние представляется в виде класса. Ниже описано 2 состояния игрока: стартовое состояние и состояние падения. Классы состояний могут содержать своеобразную логику при необходимости, но в основном в них определяются actions, transitions-decisions.

```csharp
public class PlayerStartingState : SMState
{
    [Inject] private readonly PlayerFallingState _fallingState;
    [Inject] private readonly PlayerStartDecision _startDecision;

    protected override void SetTransitions(SMTransitionList transitionList)
    {
        transitionList.Subscribe(_startDecision, _fallingState);
    }
}

public class PlayerFallingState : SMState
{
    [Inject] private readonly PlayerFallingAction _fallingAction;

    protected override void SetActions(SMActionList actionList)
    {
        actionList.Subscribe(_fallingAction);
    }
}
```

Ниже представлен класс действия, которое смещает персонажа вниз. Это действие работает только в том случае если целевое состояние активно. Этот класс можно свободно подписывать в разных состояниях - actions не привязаны к состояниям.

```csharp
public class PlayerFallingAction : SMAction
{
    [Inject] private readonly PlayerFacade _player;

    private Vector3 _playerPosition;

    protected override void OnActionUpdate()
    {
        _playerPosition = _player.Position;
        _playerPosition.y -= 100 * Time.deltaTime;
        _player.Position = _playerPosition;
    }
}
```

Decisions выполняют единственную функцию - устанавливают true/false для Decided. Это свойство используется машиной состояний для понимания - надо ли совершить переход от одного состояния к другому. Это автоматизирует переходы между состояниями и даже вносит правила, но не запрещает принудительно изменить состояние.

```csharp
public class PlayerStartDecision : SMDecision
{
    protected override void OnDecisionLateUpdate() => Decided = Input.GetKeyDown(KeyCode.Space);
}
```

## MonoDebugger
Каждая машина состояний автоматически создает свой MonoBehaviour во время игры, где в инспекторе можно управлять текущим состоянием и его компонентами, а так же управлять частотой обновления метода Update если есть такая возможность.

<img src="https://github.com/Hushigoeuf/Hushigoeuf-StateMachineWithZenject/blob/main/Common/MonoDebugger.png" width="320px">

## Пример использования в рамках небольшой игры
Здесь представлен пример использования машины состояний на примере простейшей игры, где надо управлять случайной фигурой и правильно соединить ее с похожей фигурой, при этом уклоняясь от врагов. В игре нет ничего такого, это простейшее демо с использование машины состояний и Zenject (Binding, Factories, Pools).

<img src="https://github.com/Hushigoeuf/Hushigoeuf-StateMachineWithZenject/blob/main/Common/Demonstration.gif" width="640px">

P.S. Из репозитория по понятной причине удалены сторонние решения (Zenject, Odin Inspector), графика, в остальном - все на месте.
