# StandardWeek
 <details>
<summary>지난 주차들 문제</summary>
<div markdown="1">

## Q1
**[요구사항 1]**

**분석 문제 :** 분석한 내용을 직접 작성하고, 강의의 코드를 다시 한번 작성하며 복습해봅시다.

- Equipment와 EquipTool 기능의 구조와 핵심 로직을 분석해보세요.
  - Equipment에서는 장착 아이템의 장착, 해제, 공격 시 모션의 기능을 담당한다
    - EquipNew()에선 장착상태를 초기화 한 뒤, ItemData에서 인벤토리에서 장착아이템 장착을 누르면 equipPrefab을 받아와서 Instantiate한다.
    - Uneqip()에선 현재장착아이템curEqip을 Destroy한 다음 curEquip을 null상태로 만든다.
    - OnAttackInput()에선 장착아이템이 존재할 때, 다른 UI활성화가 되지않아서 canlook상태일때, 마우스 입력을 받고있는 상태에서 공격을 가능하게 한다.
  - EquipToll에서는 Equip을 상속받아 공격 시 스태미너 관리, 공격 판정기능을 관리한다.
    - OnAttackInput()에서는 useStamina값에 따라 공격이 나갈 수 있는지를 판단한 뒤 공격 빈도에 따라 공격을 가능하게 한다.
    - OnCanAttack()은 공격이 가능하게끔 초기화하는 로직이다.
    - OnHit()은 조준점을 기준으로 공격 거리길이만큼의 레이캐스트를 쏴서 대상이 자원이라면 자원채취를 가능하게 끔 하는 로직이다
- Resource 기능의 구조와 핵심 로직을 분석해보세요.
    - Gater()는 해당 자원을 채취할 때, capacy(자원 내구도)를 1을 깎고 자원아이템을 드롭한다. capacy가0이하가 되면 더이상 채취할 수 없다.
 
**[요구사항 2]**

**확장 문제 :** 강의 ****내용을 바탕으로 새로운 기능을 추가 구현해봅시다.

- 새로운 자원을 만들고 새로운 자원채취 보상 아이템을 설정해보세요.
- 두 개의 능력치를 사용하는 새로운 무기를 만들고 구현해보세요.

**[요구사항 3]**

**개선 문제** : 기존의 코드를 개선해봅시다. (리팩터링)

---

## Q2
**[요구사항 1]**

**분석 문제** : 분석한 내용을 직접 작성하고, 강의의 코드를 다시 한번 작성하며 복습해봅시다.

- AI 네비게이션 시스템에서 가장 핵심이 되는 개념에 대해 복습해보세요.
  - 가장 핵심이 되는 개념은 NavMesh(Navigation Mesh)입니다. NavMesh는 게임 오브젝트가 이동할 수 있는 경로를 정의하고 AI캐릭터들이 자연스럽고
    효율적으로 경로를 탐색하게 하는 시스템입니다.
    NaveMesh는 AI가 이동할 수 있는 지형을 나타내는 3D 메쉬로, 이 메쉬를 바탕으로 AI가 장애물이나 다양한 지형 요소를 피하면서 목적지로 이동할 수 있게합니다.
  - NavMeshAgent는 AI오브젝트에 부착하는 컴포넌트로, AI오브젝트가 NavMesh 위를 이동하도록 제어하는 컴포넌트입니다. 이 컴포넌트를 통해 AI가 NavMesh의 경로를
    따라 이동하면서, 장애물을 회피하고, 목표지점으로 이동합니다. 이동속도, 회전속도, 가속 등을 조절해 캐릭터의 움직임을 더욱 자연스럽게 만들 수 있습니다.
  - NavMesh Obstacles는 NavMesh위에 존재하는 장애물로, AI가 피해야 하는 요소를 설정하는데 사용됩니다. 이를 통해 AI는 장애물로 인식하고 그 주위를 우회합니다.
  - NavMesh Bake는 NavMesh를 설정하고 빌드하는 과정입니다. 씬에 있는 이동 가능한 영역을 구분하고, AI가 갈 수 있는 길을 네비게이션 메쉬로 정의하는 작업을 의미합니다.
    Bake과정에서 세부적인 옵션을 설정할 수 있습니다(ex. 이동할 수 있는 최대 경서, 이동할 수 없는 작은 틈새설정, 경로탐색 정밀도 등)

- NPC 기능의 구조와 핵심 로직을 분석해보세요.
  - NPC.cs의 기능은 적 NPC의 현재상태 설정을 정의, 이동경로와 범위 설정, 공격상태 설정 및 공격상태 해제 설정, 플레이어 탐색 설정(어그로설정), 사망상태 설정 등이 있습니다.
  - SetState()에서 현재상태 설정을 정의합니다. 현재 기본, 이동, 공격 상태등이 정의되어 있습니다. 동시에 각 상태별 애니메이션의 속도도 초기화합니다.
  - PassiveUpdate()에서는 NPC의 현재상태, 플레이어의 거리에 따라 상태 변화를 다르게 정의합니다.
  - WanderToNewLocation()에서는 상태가 이동상태로 변할때 새로운 이동경로를 탐색하는 로직입니다
  - GetWonderLocation()은 이동상태로 변할때 목표좌표를 얻는 로직입니다.
  - AttackingUpdate()는 플레이어캐릭터와의 좌표거리별로 어떻게 행동하는지 정의하는 로직입니다.
  - IsPlayerInFieldOfView()는 플레이어캐릭터가 적NPC의 탐지범위내에 있는지 확인하고 bool값을 초기화하는 로직입니다.
  - TakePhysicalDamage()는 적NPC가 공격받을 때 데미지 처리 및 데미지 효과를 처리하는 로직입니다.
  - Die()로직은 적NPC가 죽을 때 드롭아이템을 생성하고 적NPC오브젝트를 파괴하는 로직입니다.
  - DamageFlash()는 코루틴으로 적NPC가 데미지를 받을때 해당 오브젝트의 컬러가 변하는 로직입니다.

**[요구사항 2]**

**확장 문제** : 강의 내용을 바탕으로 새로운 기능을 추가 구현해봅시다.

- AI 네비게이션에 대해 학습한 내용을 복습하며 펫 기능을 만들어보세요.
 - 스태미너를 채워주는 펫을 만들었습니다. 범위안으로 플레이어를 인식하면 따라다닙니다.
  
- AI 네비게이션 기능을 바탕으로 원거리 공격 몬스터를 만들어보세요.
(ex. 기존 몬스터보다 추적 범위를 넓히고 원거리에서 무기를 던짐)
 - 원거리 공격 몬스터를 만들었습니다. 
</br>
</br>
### Q3. 숙련 7강, 8강, 17강

**[요구사항 1]**

**분석 문제** : 분석한 내용을 직접 작성하고, 강의의 코드를 다시 한번 작성하며 복습해봅시다.

- 보간에 대해 학습하고 **선형보간(Lerp)**과 **구면선형보간(Slerp)**에 대해 학습해보세요.

  - 보간은 왜 하는 걸까?
  보간은 게임 개발이나 그래픽 프로그래밍에서 두 점 사이를 자연스럽게 연결하는 방법으로, 시각적 효과와 사용 경험을 향상시키기 위해 사용됩니다. 보간을 사용하는 이유는 다음과 같습니다:

1. 부드러운 움직임과 전환 효과
게임 속 객체가 순간이동하거나 급격하게 방향을 바꾸면 부자연스럽게 보입니다. 보간을 통해 움직임을 단계적으로 부드럽게 만들어, 사용자가 자연스러운 경험을 할 수 있도록 합니다.
예를 들어, 캐릭터가 특정 위치로 이동할 때 중간 지점을 여러 번 계산하여 이동하면 이동이 갑작스럽지 않고 서서히 이동하는 효과를 냅니다.
2. 자연스러운 회전
회전도 갑작스럽게 이루어지면 어색하게 보일 수 있습니다. 보간을 통해 회전 각도를 단계적으로 변화시키면, 천천히 한 방향으로 회전하는 느낌을 줄 수 있습니다. 이를 통해 현실적인 물리와 동작을 구현할 수 있으며, 캐릭터나 카메라가 자연스럽게 회전할 수 있습니다.
3. 색상 변화 및 애니메이션 효과
보간은 색상 변화나 다양한 애니메이션에도 사용됩니다. 예를 들어, 버튼을 클릭할 때 색상이 천천히 변하게 하거나, 캐릭터가 데미지를 받을 때 색이 빨간색으로 변했다가 다시 원래 색으로 돌아오는 효과를 주는 것입니다. 이는 시각적으로 더 부드럽고 명확한 피드백을 제공합니다.
4. 게임의 몰입도와 현실감을 향상
게임 내의 움직임, 전환, 효과가 갑작스럽게 이루어지면 사용자에게 어색함을 줄 수 있습니다. 보간을 통해 변화 과정을 부드럽게 연결하면 몰입감을 유지하고 현실감 있는 게임 경험을 제공합니다. 현실 세계에서는 거의 모든 움직임이 시간에 따라 자연스럽게 변화하기 때문에, 이를 모방하는 것이 게임의 질을 높이는 데 매우 중요합니다.
5. 프레임별 계산을 단순화
게임은 각 프레임마다 화면을 갱신하는데, 보간을 사용하면 프레임마다 새 위치나 색상을 정확하게 계산할 수 있어 코드가 더 단순해지고 관리가 쉬워집니다. 보간으로 중간값을 자동으로 계산해주기 때문에, 개발자가 직접 프레임별로 조정하지 않아도 일정한 속도로 변화하는 효과를 구현할 수 있습니다.

  - 선형보간
    - 선형보간은 두 점을 직선으로 연결하여 그 사이의 값을 계산하는 방법입니다. `t`값에 따라 두 점 사이의 위치를 계산합니다.
    - Lerp(a,b,t) = (1-t)*a + t*b
    - 여기서 a는 시작점, b는 끝점 혹은 도착점, t는 0과 1사이의 값(0이 시작점, 1이 도착점)
    - 선형보간의 장점과 한계
      - 장점: 계산이 간단하고 성능이 뛰어나며 직선 상의 보간에 적합합니다.
      - 한계: 회전과 같은 복잡한 보간에는 적합하지 않습니다. 특히, 회전 시 직선 보간으로 인해 결과가 이상하게 보일 수 있습니다.

  - 구면선형보간
    - 구면선형보간은 두 벡터가 이루는 각을 구면(구의 표면)을 따라 보간하는 방법입니다. 보통 회전과 같은 각도를 부드럽게 보간할 때 사용됩니다.
    - 구면선형보간의 장점과 한계
      -장점: 각도를 자연스럽게 보간할 수 있습니다. 회전할 때 발생할 수 있는 왜곡을 방지하며, 카메라나 캐릭터가 특정 방향으로 회전할 때 매우 유용합니다.
      -한계: 계산량이 선형보간보다 많아, 성능에 민감한 곳에서는 주의해야 합니다.

  - 정리 및 사용 시 주의사항
    - Lerp: 두 점 사이를 직선으로 보간하며, 주로 위치 이동이나 단순한 속도 조절에 적합합니다.
    - Slerp: 회전과 같은 각도를 자연스럽게 보간할 때 적합하며, Quaternion을 이용해 회전할 때 많이 사용됩니다.

  - 자료형 사용 예시
    |자료형|	Lerp 적용 예시|	Slerp 적용 예시|
    |:---|:---|:---|
    |Vector3|오브젝트가 직선으로 이동할 때|구면 상에서 자연스럽게 곡선을 따라 이동할 때|
    |Quaternion	|회전을 직선 보간으로 점진적으로 변경할 때	|카메라나 캐릭터의 자연스러운 회전이 필요할 때|
    |Color	|UI 버튼이 색상 변화를 할 때	|지원되지 않음|
    |Float	|단일 값의 변화가 점진적일 때 (투명도, 속도 등)	|지원되지 않음|


- 근사값(`Mathf.Approximately`)을 사용하는 이유에 대해 학습해보세요.
1. 부동 소수점 연산의 정확도 문제
컴퓨터는 소수를 정확히 표현하지 못하는 경우가 많습니다. 예를 들어, 0.1과 같은 소수를 이진법으로 완벽히 나타낼 수 없기 때문에 약간의 오차가 발생합니다. 이로 인해 단순히 == 연산자로 값을 비교하려 하면, 기대한 것과 다른 결과가 나올 수 있습니다.
Mathf.Approximately는 이런 오차를 감안하여, 두 값이 거의 같은지를 확인해 주므로 안정적으로 값 비교를 할 수 있게 합니다.
2. 부드러운 움직임이나 애니메이션 종료 조건에서 사용
게임에서 위치나 회전이 특정 값에 도달했는지 확인할 때 근사 비교가 필요합니다. 예를 들어, 플레이어가 목표 위치에 거의 도달했을 때 이동을 멈추게 하려면, Mathf.Approximately로 확인하면 더 부드러운 결과를 얻을 수 있습니다.
transform.position == targetPosition처럼 비교할 경우, 미세한 부동 소수점 차이로 인해 목표 위치에 정확히 도달하지 못해 멈추지 않거나 이상하게 작동할 수 있습니다. 반면 Mathf.Approximately를 사용하면 이 오차를 무시하고 거의 도달한 경우를 “근사값”으로 인식해 멈추게 할 수 있습니다.
3. 물리 계산과 동작 시 정확성 보장
물리 연산에서 속도나 가속도가 거의 0에 가까울 때 정지 상태로 간주하고 싶을 때, Mathf.Approximately를 사용하면 미세한 소수점 차이를 신경 쓰지 않고 안정적인 정지 상태를 구현할 수 있습니다.
예를 들어, rigidbody.velocity.magnitude == 0으로 비교하려고 하면, 부동 소수점 연산의 미세한 오차로 인해 0이 아닌 아주 작은 값이 남아 무한히 움직이는 상태가 될 수 있습니다. Mathf.Approximately(rigidbody.velocity.magnitude, 0)를 사용하면 근사값으로 처리해 이 문제를 해결할 수 있습니다.
4. UI 및 값의 변화를 다룰 때 활용
UI나 애니메이션에서 부드러운 변화를 주고 싶을 때 Mathf.Approximately는 특히 유용합니다. 예를 들어, 슬라이더 값이 특정 수치에 도달했는지 확인할 때 오차로 인해 값이 정확히 맞지 않아 무한루프에 빠지는 등의 문제가 발생할 수 있습니다.
이 경우 Mathf.Approximately를 사용하면 소수점 차이를 무시하고 슬라이더가 거의 도달했을 때 원하는 동작을 수행하도록 할 수 있습니다.

</br>
</br>
**[요구사항 2]**

**확장 문제** : 강의 내용을 바탕으로 새로운 기능을 추가 구현해봅시다.

- 몬스터가 공격할 때 효과음을 추가해보세요.
  - 추가했는데 애니메이션이랑 사운드가 맞질않습니다.
- 새로운 종류의 조명을 추가해 게임효과를 더해보세요.

  



</div>
</details>

