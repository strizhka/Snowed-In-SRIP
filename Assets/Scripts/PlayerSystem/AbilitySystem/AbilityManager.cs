using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace PlayerSystem.AbilitySystem
{
    public class AbilityManager
    {
        private List<BaseAbility> _abilities = new();
        
        [Inject]
        public void Construct(List<BaseAbility> abilities)
        {
            _abilities = abilities;
        }
        
        public void EnableAbility(Ability ability)
        {
            BaseAbility baseAbility = GetAbility(ability);
            baseAbility.Enable();
        }
        
        public void DisableAbility(Ability ability)
        {
            BaseAbility baseAbility = GetAbility(ability);
            baseAbility.Disable();
        }
        
        public void UpdateAbilities()
        {
            foreach (var ability in _abilities)
            {
                ability.Execute();
            }
        }
        
        public void DrawGizmos()
        {
            foreach (var ability in _abilities)
            {
                ability.DrawGizmos();
            }
        }
        
        private BaseAbility GetAbility(Ability ability)
        {
            return _abilities.FirstOrDefault(baseAbility => baseAbility.Ability == ability);
        }
    }
}